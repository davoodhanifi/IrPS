using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IrpsApi.Api.Models.OnlinePayment
{
    [DataContract]
    public class OnlinePaymentInitResultModel
    {
        [DataMember(Name = "payment_url")]
        public string PaymentUrl
        {
            get;
            set;
        }

        [DataMember(Name = "gateway_url")]
        public string GatewayUrl
        {
            get;
            set;
        }

        [DataMember(Name = "online_payment")]
        public OnlinePaymentModel OnlinePayment
        {
            get;
            set;
        }

        [DataMember(Name = "parameters")]
        public IDictionary<string, string> Parameters
        {
            get;
            set;
        }
    }
}