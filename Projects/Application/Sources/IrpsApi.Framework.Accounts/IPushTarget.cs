using System;

namespace IrpsApi.Framework.Accounts
{
    public interface IPushTarget : IRecord
    {
        string AccountId
        {
            get;
            set;
        }

        string Token
        {
            get;
            set;
        }

        string TypeId
        {
            get;
            set;
        }

        string PlatformUniqueId
        {
            get;
            set;
        }

        string PlatformName
        {
            get;
            set;
        }

        string PlatformVersion
        {
            get;
            set;
        }

        DateTime RegistrationDateTime
        {
            get;
            set;
        }

        string StatusId
        {
            get;
            set;
        }
    }
}