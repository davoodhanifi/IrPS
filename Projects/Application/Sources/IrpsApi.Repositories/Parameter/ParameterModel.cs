using System;
using System.Runtime.Serialization;

namespace Noandishan.IrpsApi.Repositories.Parameter
{
    [DataContract]
    internal class ParameterModel
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
