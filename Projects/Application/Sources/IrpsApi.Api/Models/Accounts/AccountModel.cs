using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using IrpsApi.Api.Models.Common;

namespace IrpsApi.Api.Models.Accounts
{
    [DataContract]
    public class AccountModel : RecordModel
    {
        [DataMember(Name = "user_code")]
        public string UserCode
        {
            get;
            set;
        }

        [DataMember(Name = "username")]
        public string Username
        {
            get;
            set;
        }

        [DataMember(Name = "type")]
        public AccountTypeModel Type
        {
            get;
            set;
        }

        [DataMember(Name = "entity_type")]
        public AccountEntityTypeModel EntityType
        {
            get;
            set;
        }

        [DataMember(Name = "email")]
        public string Email
        {
            get;
            set;
        }

        [DataMember(Name = "mobile")]
        public string Mobile
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

        [DataMember(Name = "branch")]
        public EntityModel Branch
        {
            get;
            set;
        }

        [DataMember(Name = "email_verification_status")]
        public VerificationStatusModel EmailVerificationStatus
        {
            get;
            set;
        }

        [DataMember(Name = "mobile_verification_status")]
        public VerificationStatusModel MobileVerificationStatus
        {
            get;
            set;
        }

        [DataMember(Name = "state")]
        public AccountStateModel State
        {
            get;
            set;
        }

        [DataMember(Name = "state_notes")]
        public string StateNotes
        {
            get;
            set;
        }

        [DataMember(Name = "roles")]
        public string Roles
        {
            get;
            set;
        }

        [DataMember(Name = "permissions")]
        public string Permissions
        {
            get;
            set;
        }

        [DataMember(Name = "verification_status")]
        public VerificationStatusModel VerificationStatus
        {
            get;
            set;
        }

        [DataMember(Name = "referrer_account")]
        public AccountModel ReferrerAccount
        {
            get;
            set;
        }

        [DataMember(Name = "allowed_ips")]
        public string AllowedIPs
        {
            get;
            set;
        }

        [DataMember(Name = "contacts")]
        public IEnumerable<ContactModel> Contacts
        {
            get;
            set;
        }
    }
}