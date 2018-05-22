using System;

namespace IrpsApi.Framework.Accounting
{
    public interface IBalance : IEntity
    {
        string UserCode
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

        string Notes
        {
            get;
            set;
        }
    }
}
