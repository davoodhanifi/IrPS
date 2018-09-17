
using System.Runtime.Serialization;

namespace IrpsApi.Api.Models.Accounts
{
    [DataContract(Name = "profile")]
    public class LimitedPersonProfileModel : ProfileModel
    {
        [DataMember(Name = "account")]
        public AccountModel Account
        {
            get;
            set;
        }

        [DataMember(Name = "first_name")]
        public string FirstName
        {
            get;
            set;
        }

        [DataMember(Name = "last_name")]
        public string LastName
        {
            get;
            set;
        }
    }
}