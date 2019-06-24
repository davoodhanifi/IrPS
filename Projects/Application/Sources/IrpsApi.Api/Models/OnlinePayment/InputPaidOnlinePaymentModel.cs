using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.OnlinePayment
{
    [DataContract]
    public class InputPaidOnlinePaymentModel : RecordModel
    {
        [DataMember(Name = "online_payment_id")]
        [Required]
        public string OnlinePaymentId
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

        [DataMember(Name = "paid_amount")]
        public decimal? PaidAmount
        {
            get;
            set;
        }

        [DataMember(Name = "payment_date_time")]
        public string PaymentDateTime
        {
            get;
            set;
        }

        [DataMember(Name = "parameters")]
        public IEnumerable<OnlinePaymentParameterModel> OnlinePaymentParameters
        {
            get;
            set;
        }
    }
}