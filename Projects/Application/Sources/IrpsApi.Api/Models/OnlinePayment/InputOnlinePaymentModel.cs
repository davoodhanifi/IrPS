using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.OnlinePayment
{
    [DataContract]
    public class InputOnlinePaymentModel : RecordModel
    {
        [DataMember(Name = "account")]
        [Required]
        public AccountModel Account
        {
            get;
            set;
        }

        [DataMember(Name = "gateway")]
        [Required]
        public OnlinePaymentGatewayModel Gateway
        {
            get;
            set;
        }

        [DataMember(Name = "amount")]
        [Required]
        public decimal Amount
        {
            get;
            set;
        }

        [DataMember(Name = "callback_url")]
        [Required]
        public string CallbacUrl
        {
            get;
            set;
        }
    }
}