using System;
using IrpsApi.Framework.Accounting;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Accounting
{
    [Table("Transaction", "Accounting")]
    public class Transaction : GeneratedQueryRecord, ITransaction
    {
        public string FromAccountId
        {
            get;
            set;
        }

        public string ToAccountId
        {
            get;
            set;
        }

        public decimal Amount
        {
            get;
            set;
        }

        public DateTime DateTime
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string TypeId
        {
            get;
            set;
        }

        public string OnlinePaymentId
        {
            get;
            set;
        }
    }
}
