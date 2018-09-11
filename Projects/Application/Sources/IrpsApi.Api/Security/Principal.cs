using System.Security.Principal;
using IrpsApi.Framework.Accounts;

namespace IrpsApi.Api.Security
{
    internal class Principal : GenericPrincipal
    {
        public ISession Session
        {
            get;
        }

        public string[] Permissions
        {
            get;
        }

        public Principal(IIdentity identity, ISession session) : base(identity, null)
        {
            Session = session;
        }

        public Principal(IIdentity identity, ISession session, string[] permissions) : base(identity, permissions)
        {
            Session = session;
            Permissions = permissions;
        }
    }
}
