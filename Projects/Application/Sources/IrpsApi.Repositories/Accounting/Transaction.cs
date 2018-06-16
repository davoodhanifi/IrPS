using System;
using IrpsApi.Framework.Accounting;

namespace Noandishan.IrpsApi.Repositories.Accounting
{
    public class Transaction : ITransaction
    {
        public int Id
        {
            get;
            set;
        }

        public string FromUserCode
        {
            get;
            set;
        }

        public string ToUserCode
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

        public TransactionType TransactionType
        {
            get;
            set;
        }

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
