using System.Runtime.Serialization;

namespace IrpsApi.Api.Models.Fcm
{
    [DataContract]
    public class MessageModel<T>
    {
        [DataMember(Name = "data", IsRequired = true)]
        public T Data
        {
            get;
            set;
        }

        [DataMember(Name = "profile", IsRequired = true)]
        public string Profile
        {
            get;
            set;
        }
    }
}