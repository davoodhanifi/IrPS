using System;
using IrpsApi.Framework.Accounting;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Accounting
{
    [Table("Balance", "Accounting")]
    public class Balance : GeneratedQueryRecord, IBalance
    {
        public string AccountId
        {
            get;
            set;
        }

        public DateTime DateTime
        {
            get;
            set;
        }

        public decimal CurrentBalance
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
    }
}
