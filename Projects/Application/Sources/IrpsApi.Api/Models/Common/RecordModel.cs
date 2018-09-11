using System.Runtime.Serialization;

namespace IrpsApi.Api.Models.Common
{
    [DataContract(Name = "record")]
    public class RecordModel : EntityModel
    {
        [DataMember(Name = "meta")]
        public RecordMetadataModel Meta
        {
            get;
            set;
        }
    }
}
