using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Api.ExpandOptionsHelpers;
using IrpsApi.Api.Models.OnlinePayment;
using IrpsApi.Framework.Accounts.Repositories;
using IrpsApi.Framework.OnlinePayment;
using IrpsApi.Framework.OnlinePayment.Repositories;
using IrpsApi.Framework.System;
using IrpsApi.Framework.System.Repositories;
using Mabna.WebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace IrpsApi.Api.Controllers.OnlinePayment
{
    public class OnlinePaymentController : RequiresAuthenticationApiControllerBase
    {
        private readonly IOnlinePaymentRepository _onlinePaymentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IOnlinePaymentStateRepository _onlinePaymentStateRepository;
        private readonly IOnlinePaymentGatewayRepository _onlinePaymentGatewayRepository;
        private readonly IOnlinePaymentService _onlinePaymentService;
        private readonly IPersonProfileRepository _personProfileRepository;
        private readonly ILogRepository _logRepository;
        private readonly IOptionsMonitor<SamanGatewaySettings> _samanGateWaySettings;

        public OnlinePaymentController(IOnlinePaymentRepository onlinePaymentRepository, IAccountRepository accountRepository, IOnlinePaymentStateRepository onlinePaymentStateRepository, IOnlinePaymentGatewayRepository onlinePaymentGatewayRepository, IOnlinePaymentParameterRepository onlinePaymentParameterRepository, IOnlinePaymentService onlinePaymentService, IPersonProfileRepository personProfileRepository, ILogRepository logRepository, IOptionsMonitor<SamanGatewaySettings> samanGateWaySettings)
        {
            _onlinePaymentRepository = onlinePaymentRepository;
            _accountRepository = accountRepository;
            _onlinePaymentStateRepository = onlinePaymentStateRepository;
            _onlinePaymentGatewayRepository = onlinePaymentGatewayRepository;
            _onlinePaymentService = onlinePaymentService;
            _personProfileRepository = personProfileRepository;
            _logRepository = logRepository;
            _samanGateWaySettings = samanGateWaySettings;

            ExpandEngines.Add("parameters", _onlinePaymentRepository.GetOnlinePaymentParameterAsync);
            ExpandEngines.Add("state", _onlinePaymentStateRepository.GetAsync);
            ExpandEngines.Add("gateway", _onlinePaymentGatewayRepository.GetAsync);
            ExpandEngines.Add("account", _accountRepository.GetAsync);
            ExpandEngines.Add("online_payment", _onlinePaymentRepository.GetAsync);
        }

        /// <summary>
        /// Get user online payments.
        /// </summary>
        /// <response code="404">invalid_account_id</response>  
        /// <response code="403">forbidden</response>
        [HttpGet]
        [Route("onlinepayment/{account_id}/onlinepayments")]
        [SwaggerResponse(200, type: typeof(IEnumerable<OnlinePaymentModel>))]
        [SwaggerResponse(404)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<IEnumerable<OnlinePaymentModel>>> GetAllOnlinePaymentsAsync([FromRoute(Name = "account_id")] string accountId, [FromQuery(Name = "_expand")] ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (accountId != Session.AccountId)
                return Forbid();

            var account = await _accountRepository.GetAsync(accountId, cancellationToken);
            if (account == null)
                return NotFound("invalid_account_id");

            var onlinePayments = await _onlinePaymentRepository.GetOnlinePaymentsAsync(accountId, cancellationToken);
            var result = new List<OnlinePaymentModel>();
            foreach (var onlinePayment in onlinePayments)
                result.Add(await onlinePayment.ToOnlinePaymentModelAsync(GetExpandOptions(expandOptions), cancellationToken));

            return Ok(result);
        }

        /// <summary>
        /// Get online payment by online payment id.
        /// </summary>
        /// <response code="404">invalid_online_payment_id</response>  
        [HttpGet]
        [Route("onlinepayment/onlinepayments/{online_payment_id}")]
        [SwaggerResponse(200, type: typeof(OnlinePaymentModel))]
        [SwaggerResponse(404)]
        public async Task<ActionResult<OnlinePaymentModel>> GetOnlinePaymentAsync([FromRoute(Name = "online_payment_id")]string onlinePaymentId, [FromQuery(Name = "_expand")]ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var onlinePayment = await _onlinePaymentRepository.GetAsync(onlinePaymentId, cancellationToken);
            if (onlinePayment == null)
                return NotFound("invalid_online_payment_id");

            return Ok(await onlinePayment.ToOnlinePaymentModelAsync(GetExpandOptions(expandOptions), cancellationToken));
        }

        /// <summary>
        /// Get online payment states.
        /// </summary>
        [HttpGet]
        [Route("onlinepayment/onlinepaymentstates")]
        public async Task<ActionResult<IEnumerable<OnlinePaymentStateModel>>> GetOnlinePaymentStatesAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var onlinePaymentStates = await _onlinePaymentStateRepository.GetAllOnlinePaymentStatesAsync(cancellationToken);
            return Ok(onlinePaymentStates.Select(item => item.ToOnlinePaymentStateModel()).ToList());
        }

        /// <summary>
        /// Get online payment gateways.
        /// </summary>
        [HttpGet]
        [Route("onlinepayment/onlinepaymentgateways")]
        public async Task<ActionResult<IEnumerable<OnlinePaymentGatewayModel>>> GetOnlinePaymentGatewaysAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var onlinePaymentGateways = await _onlinePaymentGatewayRepository.GetAllOnlinePaymentGatewaysAsync(cancellationToken);
            return Ok(onlinePaymentGateways.Select(item => item.ToOnlinePaymentGatewayModel()).ToList());
        }

        /// <summary>
        /// Add online payment.
        /// </summary>
        /// <response code="422">invlaid_account_id, missing_getway</response>  
        /// <response code="403">forbidden</response>
        [HttpPost]
        [Route("onlinepayment/onlinepayments/add")]
        [SwaggerResponse(200, type: typeof(OnlinePaymentInitResultModel))]
        [SwaggerResponse(422)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<OnlinePaymentInitResultModel>> CreateOnlinePaymentAsync([FromBody] InputOnlinePaymentModel onlinePaymentModel, [FromQuery(Name = "_expand")] ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (onlinePaymentModel.Account.Id != Session.AccountId)
                return Forbid();

            var account = await _accountRepository.GetAsync(onlinePaymentModel.Account.Id, cancellationToken);
            if (account == null)
                return UnprocessableEntity("invlaid_account_id", "there is no account by provided id.");

            if (onlinePaymentModel.Gateway == null)
                return UnprocessableEntity(new UnprocessableEntityException("missing_getway", "Getway Not Defined!"));

            var additionalData1 = string.Empty;
            var additionalData2 = string.Empty;

            //if (account.EntityTypeId == AccountEntityTypeIds.Person)
            //{
            //    var personProfile = await _personProfileRepository.GetAsync(account, cancellationToken);
            //    additionalData1 = personProfile.NationalCode;
            //    additionalData2 = $"{personProfile.FirstName} {personProfile.LastName}";
            //}

            var initResult = await _onlinePaymentService.InitOnlinePayment(account.Id, onlinePaymentModel.Gateway.Id, onlinePaymentModel.Amount, onlinePaymentModel.CallbacUrl, new[] { additionalData1, additionalData2 }, cancellationToken);

            if (initResult.IsSuccess)
                return Ok(await initResult.ToOnlinePaymentInitResultModel(GetExpandOptions(expandOptions), cancellationToken));

            return StatusCode((int)HttpStatusCode.BadGateway, new ApiResultModel<object>
            {
                Error = new ErrorModel
                {
                    Code = initResult.ErrorCode,
                    Message = initResult.ErrorMessage
                }
            });
        }

        /// <summary>
        /// Redirect to callback.
        /// </summary>
        /// <response code="422">invlaid_account_id</response>  
        /// <response code="403">forbidden</response>
        [HttpPost]
        [Route("onlinepayment/onlinepayments/{uid}/result")]
        [SwaggerResponse(200, type: typeof(RedirectResult))]
        [SwaggerResponse(422)]
        [AllowAnonymous]
        public async Task<IActionResult> Callback([FromRoute(Name = "uid")]string uid, [FromForm]string state, [FromForm]string stateCode, [FromForm]string refNum, [FromForm]string resNum, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("uid", uid);
            parameters.Add("State", state);
            parameters.Add("StateCode", stateCode);
            parameters.Add("RefNum", refNum);
            parameters.Add("ResNum", resNum);

            if (!Guid.TryParse(uid, out var uniqueId))
            return UnprocessableEntity(new UnprocessableEntityException("invalid_uid", "UID Is Not Valid!"));

            var onlinePayment = await _onlinePaymentRepository.GetOnlinePaymentByUniqueIdAsync(uniqueId, cancellationToken);
            if (onlinePayment == null)
                return UnprocessableEntity(new UnprocessableEntityException("invalid_uid", "UID Is Not Valid!"));
            
            _logRepository.InsertLog("Irps.API", LogLevelIds.Information, null, "Onlinepayment.Onlinepayments.ProcessCallback", "code", string.Join(",", parameters.Select(item => item.Value)));

            var result = await _onlinePaymentService.ProcessCallbackAsync(onlinePayment, parameters, cancellationToken);

            _logRepository.InsertLog("Irps.API", LogLevelIds.Information, null, "Onlinepayment.Onlinepayments.ProcessCallback", "IsSuccess", result.IsSuccess, "ErrorCode", result.ErrorCode, "ErrorMessage", result.ErrorMessage);

            return await Redirect(result, onlinePayment, cancellationToken);
        }

        public async Task<IActionResult> Redirect(IOnlinePaymentCallbackProcessResult result, IOnlinePayment onlinePayment, CancellationToken cancellationToken)
        {
            var url = $"{_samanGateWaySettings.CurrentValue.BaseUrl}/finance/sep/callback/{onlinePayment.UniqueId}?isSuccess={result.IsSuccess}";

            try
            {
                if (!result.IsSuccess)
                {
                    url += $"&errorCode={result.ErrorCode}";
                    //url += $"&errorMessage={result.ErrorMessage}";
                }
                else
                {
                    //if (!string.IsNullOrWhiteSpace(result.Message))
                    //    url += $"&message={result.Message}";
                }

                _logRepository.InsertLog("Irps.API", LogLevelIds.Information, null, "Onlinepayment.Onlinepayments.Redirect", "IsSuccess", result.IsSuccess, "ErrorCode", result.ErrorCode, "ErrorMessage", result.ErrorMessage, "url", url);
            }
            catch (Exception e)
            {
                _logRepository.InsertLog("Irps.API", LogLevelIds.Error, null, "Onlinepayment.Onlinepayments.Redirect", "IsSuccess", result.IsSuccess, "ErrorCode", result.ErrorCode, "ErrorMessage", result.ErrorMessage, "url", url, "Exception", e.ToString());
            }

            return await Task.Run(() => Redirect(url), cancellationToken);
        }
    }
}
