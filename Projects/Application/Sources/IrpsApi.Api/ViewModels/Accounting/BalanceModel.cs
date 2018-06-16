using System;
using System.Runtime.Serialization;
using IrpsApi.Framework.Accounting;

namespace IrpsApi.Api.ViewModels.Accounting
{
    [DataContract(Name = "balance")]
    public class BalanceModel : IBalance
    {
        [DataMember(Name = "id")]
        public int Id
        {
            get;
            set;
        }

        [DataMember(Name = "user_code")]
        public string UserCode
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
