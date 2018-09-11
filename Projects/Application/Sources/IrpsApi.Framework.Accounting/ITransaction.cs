using System;

namespace IrpsApi.Framework.Accounting
{
    public interface ITransaction : IRecord
    {
        string FromAccountId
        {
            get;
            set;
        }

        string ToAccountId
        {
            get;
            set;
        }

        decimal Amount
        {
            get;
            set;
        }

        DateTime DateTime
        {
            get;
            set;
        }

        string Description
        {
            get;
            set;
        }

        string TypeId
        {
            get;
            set;
        }

        string OnlinePaymentId
        {
            get;
            set;
        }
    }
}
