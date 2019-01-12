using System.Runtime.Serialization;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.Accounts
{
    [DataContract(Name = "document_type")]
    public class DocumentTypeModel : RecordModel
    {
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
    }
}
