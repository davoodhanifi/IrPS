using System;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework.OnlinePayment.Repositories;
using Mabna.WebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IrpsApi.Api.Controllers.Finance
{
    public class FinanceController : Controller
    {
        private readonly IOnlinePaymentRepository _onlinePaymentRepository;

        public FinanceController(IOnlinePaymentRepository onlinePaymentRepository)
        {
            _onlinePaymentRepository = onlinePaymentRepository;
        }

        /// <summary>
        /// Redirect to payment gateway.
        /// </summary>
        /// <response code="422">invlaid_account_id</response>  
        [HttpGet]
        [Route("finance/payment/{token}")]
        [AllowAnonymous]
        public async Task<ActionResult> RedirectToPaymentGateway([FromRoute(Name = "token")]string uuid, CancellationToken cancellationToken = default)
        {
            if (!Guid.TryParse(uuid, out var uniqueId))
                return UnprocessableEntity(new UnprocessableEntityException("invalid_token", "token Is Not Valid!"));

            var onlinePayment = await _onlinePaymentRepository.GetOnlinePaymentByUniqueIdAsync(uniqueId, cancellationToken);
            if (onlinePayment == null)
                return UnprocessableEntity(new UnprocessableEntityException("invalid_token", "token Is Not Valid!"));

            return View(onlinePayment);
        }

        /// <summary>
        /// Payment result.
        /// </summary>
        [HttpGet]
        [Route("finance/sep/callback/{id}")]
        [AllowAnonymous]
        public ActionResult PaymentResult([FromRoute(Name = "id")]string id, bool isSuccess, string errorCode = null, string errorMessage = null)
        {
            if (isSuccess)
                ViewBag.Success = "واریز با موفقیت انجام شد.";
            else
            {
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = "خطا در عملیات واریز پول";

                ViewBag.Error = errorMessage;
            }

            return View();
        }
    }
}
