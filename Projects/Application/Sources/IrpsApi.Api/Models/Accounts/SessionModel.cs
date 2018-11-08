using System.Collections.Generic;
using System.Runtime.Serialization;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.Accounts
{
    [DataContract(Name = "session")]
    public class SessionModel: RecordModel
    {
        // Todo : Remove this prop and send with sms.
        [DataMember(Name = "mobile_token")]
        public string MobileToken
        {
            get;
            set;
        }

        [DataMember(Name = "access_token")]
        public string AccessToken
        {
            get;
            set;
        }

        [DataMember(Name = "account")]
        public AccountModel Account
        {
            get;
            set;
        }

        [DataMember(Name = "permissions")]
        public IEnumerable<string> Permissions
        {
            get;
            set;
        }

        [DataMember(Name = "creation_date_time")]
        public string CreationDateTime
        {
            get;
            set;
        }

        [DataMember(Name = "sliding_expiration_date_time")]
        public string SlidingExpirationDateTime
        {
            get;
            set;
        }

        [DataMember(Name = "absolute_expiration_date_time")]
        public string AbsoluteExpirationDateTime
        {
            get;
            set;
        }

        [DataMember(Name = "user_agent")]
        public string UserAgent
        {
            get;
            set;
        }

        [DataMember(Name = "state")]
        public SessionStateModel State
        {
            get;
            set;
        }
    }
}