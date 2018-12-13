using System.Runtime.Serialization;

namespace IrpsApi.Api.Models.Fcm
{
    [DataContract]
    public class AndroidPushMessageModel
    {
        [DataMember(Name = "notification", IsRequired = true)]
        public NotificationModel Notification
        {
            get;
            set;
        }

        [DataMember(Name = "to_registration_id", IsRequired = true)]
        public string ToRegistrationId
        {
            get;
            set;
        }

        [DataMember(Name = "data")]
        public object Data
        {
            get;
            set;
        }
    }
}