using System;
using System.Runtime.Serialization;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.Accounting
{
    [DataContract(Name = "transaction")]
    public class TransactionModel : RecordModel
    {
        [DataMember(Name = "from_account")]
        public AccountModel FromAccount
        {
            get;
            set;
        }

        [DataMember(Name = "to_account")]
        public AccountModel ToAccount
        {
            get;
            set;
        }

        [DataMember(Name = "amount")]
        public decimal Amount
        {
            get;
            set;
        }

        [DataMember(Name = "date_time")]
        public string DateTime
        {
            get;
            set;
        }

        [DataMember(Name = "description")]
        public string Description
        {
            get;
            set;
        }

        [DataMember(Name = "type")]
        public TransactionTypeModel Type
        {
            get;
            set;
        }

        [DataMember(Name = "online_payment_id")]
        public int OnlinePaymentId
        {
            get;
            set;
        }
    }
}
