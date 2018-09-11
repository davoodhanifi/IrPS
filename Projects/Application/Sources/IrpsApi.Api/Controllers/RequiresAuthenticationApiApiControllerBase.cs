using IrpsApi.Api.Security;
using IrpsApi.Framework.Accounts;

namespace IrpsApi.Api.Controllers
{
    public class RequiresAuthenticationApiControllerBase : ApiControllerBase
    {
        internal ISession Session => Request.HttpContext.User?.ToPrincipal().Session;

        /*private readonly ISessionRepository _sessionRepository;

        internal Principal Principal => Request.GetOwinContext().Request.User?.ToPrincipal();

        public RequiresAuthenticationApiControllerBase(ISessionManager sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public override async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            var session = controllerContext.Request.GetOwinContext().Request.User?.ToPrincipal()?.Session;
            if (session?.Id != null)
            {
                session.LastAccessDateTime = DateTime.Now;
                session.Ip = string.Join(",", SecurityHelper.GetXForwardedForHeader(controllerContext.Request));
                session.UserAgent = Helper.GetXForwardedUserAgent(controllerContext.Request);
                await _sessionRepository.SaveAsync(session, cancellationToken);
            }

            var account = controllerContext.Request.GetOwinContext().Request.User?.ToPrincipal()?.Session?.Account;
            if (account != null && account.StateId != AccountStateIds.Active)
                return controllerContext.Request.CreateResponse(HttpStatusCode.Forbidden, new ApiResultModel<object>
                {
                    Error = new ErrorModel
                    {
                        Code = "inactive_account",
                        Message = "account_state is not set to active"
                    }
                });

            return await base.ExecuteAsync(controllerContext, cancellationToken);
        }*/
    }
}
