using System.Linq;
using IrpsApi.Framework.OnlinePayment;

namespace IrpsApi.Services.OnlinePayment
{
    public class OnlinePaymentCallbackProcessResult : ResultBase, IOnlinePaymentCallbackProcessResult
    {
        public IOnlinePayment OnlinePayment
        {
            get;
            set;
        }

        public static OnlinePaymentCallbackProcessResult Failed(IOnlinePayment onlinePayment = null, string errorCode = null, string errorMessage = null)
        {
            return new OnlinePaymentCallbackProcessResult
            {
                IsSuccess = false,
                OnlinePayment = onlinePayment,
                RedirectUrl = onlinePayment?.OnlinePaymentParameters.FirstOrDefault(q=>q.Key == "callback_url")?.Value?.ToString(),
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            };
        }

        public static IOnlinePaymentCallbackProcessResult Success(IOnlinePayment onlinePayment, string message)
        {
            return new OnlinePaymentCallbackProcessResult
            {
                IsSuccess = true,
                OnlinePayment = onlinePayment,
                RedirectUrl = onlinePayment.OnlinePaymentParameters.FirstOrDefault(q=>q.Key == "callback_url")?.Value?.ToString(),
                Message = message
            };
        }

        public string RedirectUrl
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
    }
}