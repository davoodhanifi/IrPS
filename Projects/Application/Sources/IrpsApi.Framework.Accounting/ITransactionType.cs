using System;

namespace IrpsApi.Framework.Accounting
{
    public interface ITransactionType : IRecord
    {
        string Title
        {
            get;
        }

        string TitleEn
        {
            get;
        }
    }
}
