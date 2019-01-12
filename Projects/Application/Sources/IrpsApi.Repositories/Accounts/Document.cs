using System;
using IrpsApi.Framework.Accounts;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Accounts
{
    [Table("Document", "Accounts")]
    public class Document : GeneratedQueryRecord, IDocument
    {
        public string AccountId
        {
            get;
            set;
        }

        public DateTime DateTime
        {
            get;
            set;
        }

        public string TypeId
        {
            get;
            set;
        }

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

        public string MimeType
        {
            get;
            set;
        }

        public byte[] Data
        {
            get;
            set;
        }

        public string Note
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }
    }
}