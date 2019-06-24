using System.Collections.Generic;
using System.Runtime.Serialization;
using IrpsApi.Api.Models.Accounting;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.OnlinePayment
{
    [DataContract]
    public class OnlinePaymentModel : RecordModel
    {
        [DataMember(Name = "account")]
        public AccountModel Account
        {
            get;
            set;
        }

        [DataMember(Name = "gateway")]
        public OnlinePaymentGatewayModel Gateway
        {
            get;
            set;
        }

        [DataMember(Name = "reference_no")]
        public string ReferenceNo
        {
            get;
            set;
        }

        [DataMember(Name = "amount")]
        public decimal? Amount
        {
            get;
            set;
        }

        [DataMember(Name = "transaction")]
        public TransactionModel Transaction
        {
            get;
            set;
        }

        [DataMember(Name = "state")]
        public OnlinePaymentStateModel State
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

        [DataMember(Name = "creation_date_time")]
        public string CreationDateTime
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