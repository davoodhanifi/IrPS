using System;
using IrpsApi.Framework.Accounts;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Accounts
{
    [Table("PushTarget", "Accounts")]
    public class PushTarget : GeneratedQueryRecord, IPushTarget
    {
        public string AccountId
        {
            get;
            set;
        }

        public string Token
        {
            get;
            set;
        }

        public string TypeId
        {
            get;
            set;
        }

        public string PlatformUniqueId
        {
            get;
            set;
        }

        public string PlatformName
        {
            get;
            set;
        }

        public string PlatformVersion
        {
            get;
            set;
        }

        public DateTime RegistrationDateTime
        {
            get;
            set;
        }

        public string StatusId
        {
            get;
            set;
        }
    }
}