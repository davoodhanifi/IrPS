using System;

namespace IrpsApi.Framework.Accounts
{
    public interface IAccount : IRecord
    {
        string TypeId
        {
            get;
            set;
        }

        string EntityTypeId
        {
            get;
            set;
        }

        string UserCode
        {
            get;
            set;
        }

        string Username
        {
            get;
            set;
        }

        string PasswordHash
        {
            get;
            set;
        }

        string Email
        {
            get;
            set;
        }

        string Mobile
        {
            get;
            set;
        }

        DateTime CreationDateTime
        {
            get;
            set;
        }

        string EmailVerificationStatusId
        {
            get;
            set;
        }

        string MobileVerificationStatusId
        {
            get;
            set;
        }

        string EmailVerificationToken
        {
            get;
            set;
        }

        DateTime? EmailVerificationTokenExpirationDate
        {
            get;
            set;
        }

        string MobileVerificationToken
        {
            get;
            set;
        }

        DateTime? MobileVerificationTokenExpirationDate
        {
            get;
            set;
        }

        string StateId
        {
            get;
            set;
        }

        string StateNotes
        {
            get;
            set;
        }

        string Roles
        {
            get;
            set;
        }

        string Permissions
        {
            get;
            set;
        }
        
        string VerificationStatusId
        {
            get;
            set;
        }

        string ReferrerAccountId
        {
            get;
            set;
        }
    }
}
