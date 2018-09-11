using System.Runtime.Serialization;

namespace IrpsApi.Api.Models.Common
{
    [DataContract(Name = "meta")]
    public class RecordMetadataModel
    {
        [DataMember(Name = "version")]
        public long Version
        {
            get;
            set;
        }

        [DataMember(Name = "state")]
        public RecordState State
        {
            get;
            set;
        }

        [DataMember(Name = "insert_date_time")]
        public string InsertDateTime
        {
            get;
            set;
        }

        [DataMember(Name = "update_date_time")]
        public string UpdateDateTime
        {
            get;
            set;
        }

        [DataMember(Name = "delete_date_time")]
        public string DeleteDateTime
        {
            get;
            set;
        }
    }
}
