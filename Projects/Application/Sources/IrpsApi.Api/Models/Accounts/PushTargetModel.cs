using System;
using System.Runtime.Serialization;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.Accounts
{
    [DataContract(Name = "push_target")]
    public class PushTargetModel : RecordModel
    {
        [DataMember(Name = "account")]
        public AccountModel Account
        {
            get;
            set;
        }

        [DataMember(Name = "token")]
        public string Token
        {
            get;
            set;
        }

        [DataMember(Name = "type")]
        public PushTargetTypeModel Type
        {
            get;
            set;
        }

        [DataMember(Name = "platform_unique_id")]
        public string PlatformUniqueId
        {
            get;
            set;
        }

        [DataMember(Name = "platform_name")]
        public string PlatformName
        {
            get;
            set;
        }

        [DataMember(Name = "platform_version")]
        public string PlatformVersion
        {
            get;
            set;
        }

        [DataMember(Name = "registration_date_time")]
        public string RegistrationDateTime
        {
            get;
            set;
        }

        [DataMember(Name = "status")]
        public PushTargetStatusModel Status
        {
            get;
            set;
        }
    }
}