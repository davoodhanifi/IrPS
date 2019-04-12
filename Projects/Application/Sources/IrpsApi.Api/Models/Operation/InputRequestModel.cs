using System.Collections.Generic;
using System.Runtime.Serialization;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Api.ValidationHelpers;

namespace IrpsApi.Api.Models.Operation
{
    [DataContract]
    public class InputRequestModel
    {
        [DataMember(Name = "account")]
        [Required]
        public AccountModel Account
        {
            get;
            set;
        }

        [DataMember(Name = "type")]
        [Required]
        public RequestTypeModel Type
        {
            get;
            set;
        }

        [DataMember(Name = "date_time")]
        public string DateTime
        {
            get;
            set;
        }

        [DataMember(Name = "status")]
        public RequestStatusModel Status
        {
            get;
            set;
        }

        [DataMember(Name = "comments")]
        public string Comments
        {
            get;
            set;
        }

        [DataMember(Name = "parameters")]
        public IEnumerable<RequestParameterModel> Parameters
        {
            get;
            set;
        }
    }
}
