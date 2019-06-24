using System.Collections.Generic;

namespace IrpsApi.Framework.OnlinePayment
{
    public interface IOnlinePaymentInitResult : IResultBase
    {
        IDictionary<string, string> Parameters
        {
            get;
            set;
        }

        string GatewayUrl
        {
            get;
            set;
        }

        string PaymentUrl
        {
            get;
            set;
        }

        IOnlinePayment OnlinePayment
        {
            get;
            set;
        }
    }
}