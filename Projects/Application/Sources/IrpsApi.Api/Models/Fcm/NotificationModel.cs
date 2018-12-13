using System.Runtime.Serialization;

namespace IrpsApi.Api.Models.Fcm
{
    [DataContract]
    public class NotificationModel
    {
        [DataMember(Name = "title")]
        public string Title
        {
            get;
            set;
        }

        [DataMember(Name = "body", IsRequired = true)]
        public string Body
        {
            get;
            set;
        }

        [DataMember(Name = "icon")]
        public string Icon
        {
            get;
            set;
        }

        [DataMember(Name = "sound")]
        public string Sound
        {
            get;
            set;
        }
    }
}