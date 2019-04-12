using System.Collections.Generic;
using System.Runtime.Serialization;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.Operation
{
    [DataContract]
    public class RequestModel : RecordModel
    {
        [DataMember(Name = "account")]
        public AccountModel Account
        {
            get;
            set;
        }

        [DataMember(Name = "type")]
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
