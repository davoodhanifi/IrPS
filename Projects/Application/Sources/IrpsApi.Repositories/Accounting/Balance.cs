using System;
using IrpsApi.Framework.Accounting;

namespace Noandishan.IrpsApi.Repositories.Accounting
{
    public class Balance : IBalance
    {
        public int Id
        {
            get;
            set;
        }

        public string UserCode
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
