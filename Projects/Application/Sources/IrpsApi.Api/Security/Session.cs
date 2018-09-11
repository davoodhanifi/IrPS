using System;
using System.Collections.Generic;
using IrpsApi.Framework;
using IrpsApi.Framework.Accounts;

namespace IrpsApi.Api.Security
{
    internal class Session : ISession
    {
        public string Id
        {
            get;
            set;
        }

        public long RecordVersion
        {
            get;
            set;
        }

        public RecordState RecordState
        {
            get;
            set;
        }

        public DateTime? RecordInsertDateTime
        {
            get;
            set;
        }

        public DateTime? RecordUpdateDateTime
        {
            get;
            set;
        }

        public DateTime? RecordDeleteDateTime
        {
            get;
            set;
        }

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
