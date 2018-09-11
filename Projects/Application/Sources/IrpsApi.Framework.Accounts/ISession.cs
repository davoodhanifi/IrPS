using System;
using System.Collections.Generic;

namespace IrpsApi.Framework.Accounts
{
    public interface ISession : IRecord
    {
        string AccessToken
        {
            get;
            set;
        }

        string AccountId
        {
            get;
            set;
        }

        IEnumerable<string> Permissions
        {
            get;
            set;
        }

        DateTime CreationDateTime
        {
            get;
            set;
        }

        DateTime? InvalidateDateTime
        {
            get;
            set;
        }

        string StateId
        {
            get;
            set;
        }

        DateTime LastAccessDateTime
        {
            get;
            set;
        }

        string Ip
        {
            get;
            set;
        }

        string UserAgent
        {
            get;
            set;
        }

        string TypeId
        {
            get;
            set;
        }

        string Mobile
        {
            get;
            set;
        }

        string MobileToken
        {
            get;
            set;
        }

        DateTime? MobileTokenGenerationDateTime
        {
            get;
            set;
        }

        DateTime? MobileTokenExpirationDateTime
        {
            get;
            set;
        }
    }
}