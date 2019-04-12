using System.Runtime.Serialization;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.Operation
{
    [DataContract]
    public class RequestTypeModel : RecordModel
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