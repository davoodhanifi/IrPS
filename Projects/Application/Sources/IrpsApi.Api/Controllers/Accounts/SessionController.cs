using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Api.ExpandOptionsHelpers;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Api.Security;
using IrpsApi.Api.Services;
using IrpsApi.Api.ValidationHelpers;
using IrpsApi.Framework.Accounts;
using IrpsApi.Framework.Accounts.Repositories;
using Mabna.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IrpsApi.Api.Controllers.Accounts
{
    public class SessionController : RequiresAuthenticationApiControllerBase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ISmsService _smsService;
        private static readonly Regex MobileValidator = new Regex(@"^\d+$");

        public SessionController(ISessionRepository sessionRepository, IAccountRepository accountRepository, ISmsService smsService)
        {
            _sessionRepository = sessionRepository;
            _accountRepository = accountRepository;
            _smsService = smsService;
        }

        /// <summary>
        /// Mobile login request.
        /// </summary>
        /// <response code="422">invalid_mobile</response>  
        /// <response code="429">retry_after</response>
        [HttpPost]
        [Route("accounts/sessions/mobileloginrequest")]
        [SwaggerResponse(200, type : typeof(SessionModel))]
        [SwaggerResponse(422)]
        [SwaggerResponse(429)]
        public async Task<ActionResult<SessionModel>> MobileLoginRequestAsync([FromForm][Required]string mobile, [FromQuery(Name = "_expand")]ExpandOptions expandOptions, CancellationToken cancellationToken)
        {
            if (!MobileValidator.IsMatch(mobile))
                throw new UnprocessableEntityException("invalid_mobile", "value of mobile is not a valid mobile number", "mobile");

#if !DEBUG
            var existingSession = await _sessionRepository.GetLastNotVerifiedSessionByMobile(mobile, cancellationToken);
            if (existingSession != null && existingSession.CreationDateTime.AddMinutes(5) > DateTime.Now)
            {
                Response.Headers.Add("Retry-After", existingSession.CreationDateTime.AddMinutes(5).ToString("R"));
                return Error(429, "retry_after");
            }
#endif

            var session = _sessionRepository.Create();
            session.AccessToken = Guid.NewGuid().ToString("N");
            session.Mobile = mobile;
            session.CreationDateTime = DateTime.Now;
            session.StateId = SessionStateIds.NotVerified;
            session.Ip = RemoteIpAddress;
            session.UserAgent = Helper.GetXForwardedUserAgent(Request.Headers);
            session.LastAccessDateTime = DateTime.Now;
            session.TypeId = SessionTypeIds.Normal;
            session.MobileToken = MobileVerificationHelper.GenerateToken();
            session.MobileTokenGenerationDateTime = DateTime.Now;
            session.MobileTokenExpirationDateTime = DateTime.Now.AddMinutes(5);
            session = await _sessionRepository.SaveAsync(session, cancellationToken);
            
            //_smsService.SendVerificationSms(session.Mobile, session.MobileToken);

            return Ok(await session.ToSessionModelAsync(GetExpandOptions(expandOptions), cancellationToken));
        }

        /// <summary>
        /// Mobile login.
        /// </summary>
        /// <response code="422">invlaid_session_id, invalid_token</response>  
        /// <response code="404">invlaid_session_id</response>  
        [HttpPost]
        [Route("accounts/sessions/{session_id}/mobilelogin")]
        [SwaggerResponse(200, type : typeof(SessionModel))]
        [SwaggerResponse(404)]
        [SwaggerResponse(422)]
        public async Task<ActionResult<SessionModel>> MobileLoginAsync([FromRoute(Name = "session_id")]string sessionId, [FromForm][Required] string token, [FromQuery(Name = "_expand")]ExpandOptions expandOptions, CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.GetAsync(sessionId, cancellationToken);
            if (session == null)
                return NotFound("invlaid_session_id");

            if (session.MobileTokenExpirationDateTime < DateTime.Now || !session.MobileToken.Equals(token, StringComparison.OrdinalIgnoreCase) || session.StateId != SessionStateIds.NotVerified)
                throw new UnprocessableEntityException("invalid_token");

            var account = await _accountRepository.GetByMobileAsync(session.Mobile, cancellationToken);
            if (account == null)
            {
                account = _accountRepository.Create();
                account.Mobile = session.Mobile;
                account.MobileVerificationStatusId = VerificationStatusIds.Verified;
                // Todo: Get unique usercode in account table.
                account.UserCode = new Random().Next(0, 99999999).ToString("D8");
                account.CreationDateTime = DateTime.Now;
                account.TypeId = AccountTypeIds.NormalUser;
                account.EmailVerificationStatusId = VerificationStatusIds.NotVerified;
                account.StateId = AccountStateIds.Active;
                account.VerificationStatusId = VerificationStatusIds.NotVerified;
                account = await _accountRepository.SaveAsync(account, cancellationToken);
            }

            session.StateId = SessionStateIds.Open;
            session.AccountId = account.Id;
            session = await _sessionRepository.SaveAsync(session, cancellationToken);

            return Ok(await session.ToSessionModelAsync(GetExpandOptions(expandOptions), cancellationToken));
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <response code="422">missing_username, missing_password, invalid_credential</response>
        /// <response code="403">inactive_account, password_change_required</response>
        [HttpPost]
        [Route("accounts/sessions")]
        [SwaggerResponse(200, type : typeof(SessionModel))]
        [SwaggerResponse(422)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<SessionModel>> LoginAsync([FromForm]string mobile, [FromForm]string username, [FromForm]string password, [FromQuery(Name = "_expand")]ExpandOptions expandOptions, CancellationToken cancellationToken)
        {
            if (Session.AccountId != null)
                return Unauthorized();

            var account = await _accountRepository.FindAccount(username, username, mobile, username, cancellationToken);

            if (account == null)
                throw new UnprocessableEntityException("invalid_credential", "Username or password is not valid.");

            if (!PasswordHash.ValidatePassword(password, account.PasswordHash))
                throw new UnprocessableEntityException("invalid_credential", "Username or password is not valid.");

            if (account.StateId != AccountStateIds.Active)
                return Error(HttpStatusCode.Forbidden, "inactive_account", "account_state is not set to active");

            var session = _sessionRepository.Create();
            session.AccessToken = Guid.NewGuid().ToString("N");
            session.AccountId = account.Id;
            session.CreationDateTime = DateTime.Now;
            session.StateId = SessionStateIds.Open;
            session.Ip = RemoteIpAddress;
            session.UserAgent = Helper.GetXForwardedUserAgent(Request.Headers);
            session.LastAccessDateTime = DateTime.Now;
            session.TypeId = SessionTypeIds.Normal;

            session = await _sessionRepository.SaveAsync(session, cancellationToken);

            return Ok(await session.ToSessionModelAsync(GetExpandOptions(expandOptions), cancellationToken));
        }

        /// <summary>
        /// Mobile logout.
        /// </summary>
        /// <response code="404">invlaid_session_id</response>  
        /// <response code="403">inactive_account</response>  
        [HttpDelete]
        [Route("accounts/sessions/{session_id}/mobilelogout")]
        [SwaggerResponse(200, type: typeof(SessionModel))]
        [SwaggerResponse(404)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<SessionModel>> MobileLogoutAsync([FromRoute(Name = "session_id")]string sessionId, [FromQuery(Name = "_expand")]ExpandOptions expandOptions, CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.GetAsync(sessionId, cancellationToken);
            if (session == null)
                return NotFound("invlaid_session_id");

            if (session.AccountId == null)
                return Error(HttpStatusCode.Forbidden, "inactive_account", "Forbidden: Closing session of client is not allowed.");

            session.StateId = SessionStateIds.Closed;
            session.InvalidateDateTime = DateTime.Now;
            session = await _sessionRepository.SaveAsync(session, cancellationToken);

            return Ok(await session.ToSessionModelAsync(GetExpandOptions(expandOptions), cancellationToken));
        }

    }
}
