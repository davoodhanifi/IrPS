using System;

namespace IrpsApi.Framework.Accounts
{
    public interface IDocumentType : IRecord
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
