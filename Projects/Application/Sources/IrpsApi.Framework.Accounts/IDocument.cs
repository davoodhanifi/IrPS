
using System;

namespace IrpsApi.Framework.Accounts
{
    public interface IDocument : IRecord
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

        string TypeId
        {
            get;
            set;
        }

        string Title
        {
            get;
            set;
        }

        string TitleEn
        {
            get;
            set;
        }

        string MimeType
        {
            get;
            set;
        }

        byte[] Data
        {
            get;
            set;
        }

        string Note
        {
            get;
            set;
        }

        string FileName
        {
            get;
            set;
        }
    }
}
