using System;

namespace IrpsApi.Framework.Accounting
{
    public interface ITransaction : IEntity
    {
        string FromUserCode
        {
            get;
            set;
        }

        string ToUserCode
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

        TransactionType TransactionType
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
