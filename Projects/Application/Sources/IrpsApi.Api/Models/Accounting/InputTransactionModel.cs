using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using IrpsApi.Api.Models.Accounts;

namespace IrpsApi.Api.Models.Accounting
{
    [DataContract]
    public class InputTransactionModel
    {
        [DataMember(Name = "from_account")]
        [Required]
        public AccountModel FromAccount
        {
            get;
            set;
        }

        [DataMember(Name = "to_account")]
        [Required]
        public AccountModel ToAccount
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

        [DataMember(Name = "type")]
        [Required]
        public TransactionTypeModel Type
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

        [DataMember(Name = "online_payment_id")]
        public string OnlinePaymentId
        {
            get;
            set;
        }
    }
}