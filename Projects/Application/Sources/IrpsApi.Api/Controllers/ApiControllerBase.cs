using System.Net;
using IrpsApi.Api.ExpandOptionsHelpers;
using Mabna.WebApi.AspNetCore;
using Mabna.WebApi.AspNetCore.Security;
using Mabna.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace IrpsApi.Api.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {
        private string _remoteIpAddress;

        protected string RemoteIpAddress
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_remoteIpAddress))
                    _remoteIpAddress = string.Join(",", SecurityHelper.GetXForwardedForHeader(Request));

                return _remoteIpAddress;
            }
        }

        public virtual ActionResult Error(HttpStatusCode statusCode, string errorCode, string errorMessage = null)
        {
            return Error((int)statusCode, errorCode, errorMessage);
        }

        public virtual ActionResult Error(int statusCode, string errorCode, string errorMessage = null)
        {
            return new NegotiatedContentResult(statusCode, new ApiResultModel<object>
            {
                Error = new ErrorModel
                {
                    Code = errorCode,
                    Message = errorMessage
                }
            });
        }

        public IExpandOptionCollection GetExpandOptions(ExpandOptions expandOptions)
        {
            return expandOptions?.GetExpandOptions(ExpandEngines, ValidExpandOptions);
        }
    }
}
