using System;
using IrpsApi.Framework.Accounting;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Accounting
{
    [Table("TransactionType", "Accounting")]
    public class TransactionType : GeneratedQueryRecord, ITransactionType
    {
        public string Title
        {
            get;
            set;
        }

        public string TitleEn
        {
            get;
            set;
        }
    }
}
