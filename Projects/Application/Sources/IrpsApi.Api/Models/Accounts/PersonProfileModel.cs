using System.Runtime.Serialization;

namespace IrpsApi.Api.Models.Accounts
{
    [DataContract(Name = "profile")]
    public class PersonProfileModel : ProfileModel
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

        [DataMember(Name = "father_name")]
        public string FatherName
        {
            get;
            set;
        }

        [DataMember(Name = "national_code")]
        public string NationalCode
        {
            get;
            set;
        }

        [DataMember(Name = "identification_number")]
        public string IdentificationNumber
        {
            get;
            set;
        }

        [DataMember(Name = "identification_serial")]
        public string IdentificationSerial
        {
            get;
            set;
        }

        [DataMember(Name = "identification_place_of_issue")]
        public string IdentificationPlaceOfIssue
        {
            get;
            set;
        }

        [DataMember(Name = "birth_date")]
        public string Birthdate
        {
            get;
            set;
        }

        [DataMember(Name = "gender_type")]
        public GenderTypeModel GenderType
        {
            get;
            set;
        }
    }
}