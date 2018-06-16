using System;
using System.Runtime.Serialization;
using IrpsApi.Framework.Accounting;

namespace IrpsApi.Api.ViewModels.Accounting
{
    [DataContract(Name = "transaction")]
    public class TransactionModel : ITransaction
    {
        [DataMember(Name = "id")]
        public int Id
        {
            get;
            set;
        }

        [DataMember(Name = "from_user_code")]
        public string FromUserCode
        {
            get;
            set;
        }

        [DataMember(Name = "to_user_code")]
        public string ToUserCode
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
        public DateTime DateTime
        {
            get;
            set;
        }

        [DataMember(Name = "transaction_type")]
        public TransactionType TransactionType
        {
            get;
            set;
        }

        [DataMember(Name = "notes")]
        public string Notes
        {
            get;
            set;
        }

        public void EnsureLoaded()
        {
            throw new NotImplementedException();
        }
    }
}
