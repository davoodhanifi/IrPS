using System.Runtime.Serialization;
using IrpsApi.Api.ValidationHelpers;

namespace IrpsApi.Api.Models.Accounts
{
    [DataContract]
    public class InputDocumentModel
    {
        [DataMember(Name = "account")]
        [Required]
        public AccountModel Account
        {
            get;
            set;
        }

        [DataMember(Name = "type")]
        [Required]
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
        [Required]
        public string MimeType
        {
            get;
            set;
        }

        [DataMember(Name = "data")]
        [Required]
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