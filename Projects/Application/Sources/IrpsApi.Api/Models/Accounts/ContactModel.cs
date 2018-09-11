using System.Runtime.Serialization;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.Accounts
{
    [DataContract]
    public class ContactModel : RecordModel
    {
        [DataMember(Name = "account")]
        public AccountModel Account
        {
            get;
            set;
        }

        [DataMember(Name = "tel")]
        public string Tel
        {
            get;
            set;
        }

        [DataMember(Name = "fax")]
        public string Fax
        {
            get;
            set;
        }

        [DataMember(Name = "country")]
        public CountryModel Country
        {
            get;
            set;
        }

        [DataMember(Name = "province")]
        public ProvinceModel Province
        {
            get;
            set;
        }

        [DataMember(Name = "city")]
        public string City
        {
            get;
            set;
        }

        [DataMember(Name = "address")]
        public string Address
        {
            get;
            set;
        }

        [DataMember(Name = "english_address")]
        public string AddressEn
        {
            get;
            set;
        }

        [DataMember(Name = "postal_code")]
        public string PostalCode
        {
            get;
            set;
        }

        [DataMember(Name = "type")]
        public ContactTypeModel Type
        {
            get;
            set;
        }
    }
}