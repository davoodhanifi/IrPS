using System;

namespace IrpsApi.Framework.Accounts
{
    public interface IPersonProfile : IRecord
    {
        string AccountId
        {
            get;
            set;
        }

        string FirstName
        {
            get;
            set;
        }

        string LastName
        {
            get;
            set;
        }

        string FatherName
        {
            get;
            set;
        }

        string NationalCode
        {
            get;
            set;
        }

        string IdentificationNumber
        {
            get;
            set;
        }

        string IdentificationSerial
        {
            get;
            set;
        }

        string IdentificationPlaceOfIssue
        {
            get;
            set;
        }

        DateTime? Birthdate
        {
            get;
            set;
        }

        string GenderTypeId
        {
            get;
            set;
        }
    }
}