using System.Runtime.Serialization;
using IrpsApi.Api.Models.Fcm;

namespace IrpsApi.Api.Models.FcmModels
{
    [DataContract]
    public class FcmModel
    {
        [DataMember(Name = "notification")]
        public NotificationModel Notification
        {
            get;
            set;
        }

        [DataMember(Name = "registration_ids")]
        public string[] RegistrationIds
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