using System;

namespace IrpsApi.Framework.Accounting
{
    public interface IBalance : IRecord
    {
        string AccountId
        {
            get;
            set;
        }

        DateTime DateTime
        {
            get;
            set;
        }

        decimal CurrentBalance
        {
            get;
            set;
        }

        bool IsActive
        {
            get;
            set;
        }

        string Description
        {
            get;
            set;
        }
    }
}
