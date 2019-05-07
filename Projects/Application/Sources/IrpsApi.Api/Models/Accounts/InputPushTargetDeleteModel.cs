using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

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