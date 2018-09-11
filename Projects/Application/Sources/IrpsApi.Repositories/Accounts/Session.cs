using System;
using System.Collections.Generic;
using IrpsApi.Framework.Accounts;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Accounts
{
    [Table("Session", "Accounts")]
    public class Session : GeneratedQueryRecord, ISession
    {
        public string AccessToken
        {
            get;
            set;
        }

        public string AccountId
        {
            get;
            set;
        }

        [ColumnAlias("Permissions")]
        public string InternalPermissions
        {
            get => Permissions != null ? string.Join(",", Permissions) : null;
            set => Permissions = value?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        [IgnoreColumn]
        public IEnumerable<string> Permissions
        {
            get;
            set;
        }

        public DateTime CreationDateTime
        {
            get;
            set;
        }

        public DateTime? InvalidateDateTime
        {
            get;
            set;
        }

        public string StateId
        {
            get;
            set;
        }

        public DateTime LastAccessDateTime
        {
            get;
            set;
        }

        public string Ip
        {
            get;
            set;
        }

        public string UserAgent
        {
            get;
            set;
        }

        public string TypeId
        {
            get;
            set;
        }

        public string Mobile
        {
            get;
            set;
        }

        public string MobileToken
        {
            get;
            set;
        }

        public DateTime? MobileTokenGenerationDateTime
        {
            get;
            set;
        }

        public DateTime? MobileTokenExpirationDateTime
        {
            get;
            set;
        }
    }
}