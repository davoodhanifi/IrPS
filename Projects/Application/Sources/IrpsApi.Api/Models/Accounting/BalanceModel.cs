using System;
using System.Runtime.Serialization;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.Accounting
{
    [DataContract(Name = "balance")]
    public class BalanceModel : RecordModel
    {
        [DataMember(Name = "account")]
        public AccountModel Account
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

        [DataMember(Name = "current_balance")]
        public decimal CurrentBalance
        {
            get;
            set;
        }

        [DataMember(Name = "is_active")]
        public bool IsActive
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
    }
}
