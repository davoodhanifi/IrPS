using System.Runtime.Serialization;

namespace IrpsApi.Api.Models.Operation
{
    [DataContract]
    public class RequestParameterModel
    {
        [DataMember(Name = "key")]
        public string Key
        {
            get;
            set;
        }

        [DataMember(Name = "value")]
        public object Value
        {
            get;
            set;
        }

        [DataMember(Name = "type")]
        public string Type
        {
            get;
            set;
        }
    }
}
