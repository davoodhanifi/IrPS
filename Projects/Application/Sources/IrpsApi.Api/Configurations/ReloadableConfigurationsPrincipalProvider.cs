using System;
using System.Linq;
using IrpsApi.Api.Security;
using Mabna.WebApi.AspNetCore.Security;
using Microsoft.Extensions.Options;

namespace IrpsApi.Api.Configurations
{
    public class ReloadableConfigurationsPrincipalProvider : ConfigurationsPrincipalProvider
    {
        private readonly ISessionPrincipalProvider _sessionPrincipalProvider;
        private readonly IOptionsMonitor<ApiSessions> _sessionsMonitor;

        public ReloadableConfigurationsPrincipalProvider(ISessionPrincipalProvider sessionPrincipalProvider, IOptionsMonitor<ApiSessions> sessionsMonitor) : base(sessionPrincipalProvider)
        {
            _sessionPrincipalProvider = sessionPrincipalProvider;
            _sessionsMonitor = sessionsMonitor;
        }

        public override Mabna.WebApi.AspNetCore.Security.Configuration.Session FindSession(string token)
        {
            return _sessionsMonitor.CurrentValue.Sessions.FirstOrDefault(q => string.Equals(q.AccessToken, token, StringComparison.Ordinal));
        }
    }
}