using System;
using IrpsApi.Framework.Accounts;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Accounts
{
    [Table("PersonProfile", "Accounts")]
    public class PersonProfile : GeneratedQueryRecord, IPersonProfile
    {
        public string AccountId
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string FatherName
        {
            get;
            set;
        }

        public string NationalCode
        {
            get;
            set;
        }

        public string IdentificationNumber
        {
            get;
            set;
        }

        public string IdentificationSerial
        {
            get;
            set;
        }

        public string IdentificationPlaceOfIssue
        {
            get;
            set;
        }

        public DateTime? Birthdate
        {
            get;
            set;
        }

        public string GenderTypeId
        {
            get;
            set;
        }
    }
}
