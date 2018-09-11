using System;
using IrpsApi.Framework.Accounts;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Accounts
{
    [Table("Account", "Accounts")]
    public class Account : GeneratedQueryRecord, IAccount
    {
        public string UserCode
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string PasswordHash
        {
            get;
            set;
        }

        public string TypeId
        {
            get;
            set;
        }

        public string EntityTypeId
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Mobile
        {
            get;
            set;
        }

        public string EmailVerificationStatusId
        {
            get;
            set;
        }

        public string MobileVerificationStatusId
        {
            get;
            set;
        }

        public DateTime CreationDateTime
        {
            get;
            set;
        }

        public string EmailVerificationToken
        {
            get;
            set;
        }

        public DateTime? EmailVerificationTokenExpirationDate
        {
            get;
            set;
        }

        public string MobileVerificationToken
        {
            get;
            set;
        }

        public DateTime? MobileVerificationTokenExpirationDate
        {
            get;
            set;
        }

        public string StateId
        {
            get;
            set;
        }

        public string StateNotes
        {
            get;
            set;
        }

        public string Roles
        {
            get;
            set;
        }

        public string Permissions
        {
            get;
            set;
        }

        public string VerificationStatusId
        {
            get;
            set;
        }

        public string ReferrerAccountId
        {
            get;
            set;
        }
    }
}