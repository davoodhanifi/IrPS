using System.Runtime.Serialization;
using IrpsApi.Api.ValidationHelpers;

namespace IrpsApi.Api.Models.Accounts
{
    [DataContract]
    public class InputPushTargetModel
    {
        [DataMember(Name = "account")]
        [Required]
        public AccountModel Account
        {
            get;
            set;
        }

        [DataMember(Name = "token")]
        [Required]
        public string Token
        {
            get;
            set;
        }

        [DataMember(Name = "type")]
        [Required]
        public PushTargetTypeModel Type
        {
            get;
            set;
        }

        [DataMember(Name = "platform_unique_id")]
        [Required]
        public string PlatformUniqueId
        {
            get;
            set;
        }

        [DataMember(Name = "platform_name")]
        [Required]
        public string PlatformName
        {
            get;
            set;
        }

        [DataMember(Name = "platform_version")]
        [Required]
        public string PlatformVersion
        {
            get;
            set;
        }

        [DataMember(Name = "status")]
        [Required]
        public PushTargetStatusModel Status
        {
            get;
            set;
        }
    }
}