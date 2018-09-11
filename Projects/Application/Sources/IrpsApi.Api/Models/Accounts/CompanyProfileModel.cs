using System.Runtime.Serialization;

namespace IrpsApi.Api.Models.Accounts
{
    [DataContract(Name = "profile")]
    internal class CompanyProfileModel : ProfileModel
    {
        [DataMember(Name = "account")]
        public AccountModel Account
        {
            get;
            set;
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }

        [DataMember(Name = "name_en")]
        public string NameEn
        {
            get;
            set;
        }

        [DataMember(Name = "registration_code")]
        public string RegistrationCode
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

        [DataMember(Name = "economic_code")]
        public string EconomicCode
        {
            get;
            set;
        }

        [DataMember(Name = "registration_date")]
        public string RegistrationDate
        {
            get;
            set;
        }

        [DataMember(Name = "attorney")]
        public AccountModel Attorney
        {
            get;
            set;
        }

        [DataMember(Name = "power_of_attorney_end_date")]
        public string PowerOfAttorneyEndDate
        {
            get;
            set;
        }

        [DataMember(Name = "power_of_attorney_number")]
        public string PowerOfAttorneyNumber
        {
            get;
            set;
        }

        [DataMember(Name = "registration_place")]
        public string RegistrationPlace
        {
            get;
            set;
        }

        [DataMember(Name = "business_description")]
        public string BusinessDescription
        {
            get;
            set;
        }

        [DataMember(Name = "official_paper_number")]
        public string OfficialPaperNumber
        {
            get;
            set;
        }

        [DataMember(Name = "official_paper_date")]
        public string OfficialPaperDate
        {
            get;
            set;
        }

        [DataMember(Name = "web_site")]
        public string WebSite
        {
            get;
            set;
        }

        [DataMember(Name = "trading_agent")]
        public string TradingAgent
        {
            get;
            set;
        }

        [DataMember(Name = "fida_code")]
        public string FidaCode
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
    }
}
