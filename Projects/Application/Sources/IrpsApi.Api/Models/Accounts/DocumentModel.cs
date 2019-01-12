using System;
using System.Runtime.Serialization;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.Accounts
{
    [DataContract]
    public class DocumentModel : RecordModel
    {
        [DataMember(Name = "account")]
        public AccountModel Account
        {
            get;
            set;
        }

        [DataMember(Name = "date_time")]
        public string DateTime
        {
            get;
            set;
        }

        [DataMember(Name = "type")]
        public DocumentTypeModel Type
        {
            get;
            set;
        }

        [DataMember(Name = "title")]
        public string Title
        {
            get;
            set;
        }

        [DataMember(Name = "english_title")]
        public string TitleEn
        {
            get;
            set;
        }

        [DataMember(Name = "mime_type")]
        public string MimeType
        {
            get;
            set;
        }

        [DataMember(Name = "data")]
        public byte[] Data
        {
            get;
            set;
        }

     
        [DataMember(Name = "note")]
        public string Note
        {
            get;
            set;
        }

        [DataMember(Name = "file_name")]
        public string FileName
        {
            get;
            set;
        }
    }
}