using System.Runtime.Serialization;

namespace Noandishan.IrpsApi.Repositories.System
{
    [DataContract(Name = "log_parameter")]
    public class LogParameterModel
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
