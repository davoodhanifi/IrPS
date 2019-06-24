using System.Collections.Generic;

namespace IrpsApi.Services.OnlinePayment
{
    public class CallbackParametersProcessResult : ResultBase
    {
        public string OnlinePaymentId
        {
            get;
            set;
        }

        public string TransactionId
        {
            get;
            set;
        }

        public IDictionary<string, string> AdditionalParameters
        {
            get;
            set;
        }

        public string MessageText
        {
            get;
            set;
        }
    }
}