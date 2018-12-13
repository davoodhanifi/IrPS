using System.Runtime.Serialization;
using IrpsApi.Api.ValidationHelpers;

namespace IrpsApi.Api.Models.Accounts
{
    [DataContract]
    public class InputPushTargetDeleteModel
    {
        [DataMember(Name = "token")]
        [Required]
        public string Token
        {
            get;
            set;
        }
    }
}