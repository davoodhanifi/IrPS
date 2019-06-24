using System.Collections.Generic;
using IrpsApi.Framework.OnlinePayment;

namespace IrpsApi.Services.OnlinePayment
{
    public class OnlinePaymentInitResult : ResultBase, IOnlinePaymentInitResult
    {
        public IDictionary<string, string> Parameters
        {
            get;
            set;
        }

        public string GatewayUrl
        {
            get;
            set;
        }

        public string PaymentUrl
        {
            get;
            set;
        }

        public IOnlinePayment OnlinePayment
        {
            get;
            set;
        }
    }
}