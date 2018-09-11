using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using IrpsApi.Framework.Accounts;
using Mabna.WebApi.Common.Security;
using Microsoft.AspNetCore.Http;

namespace IrpsApi.Api.Security
{
    internal static class Helper
    {
        public static Principal ToPrincipal(this IPrincipal principal)
        {
            if (principal is Principal apiPrincipal)
                return apiPrincipal;

            if (principal is UserPrincipal userPrincipal)
            {
                return new Principal(principal.Identity, new Session
                {
                    Permissions = userPrincipal.Permissions,
                    TypeId = SessionTypeIds.Permanent
                }, userPrincipal.Permissions?.ToArray());
            }

            return new Principal(principal.Identity, new Session { StateId = SessionStateIds.Open }, new string[0]);
        }

        public static string GetXForwardedUserAgent(IHeaderDictionary headers)
        {
            var userAgents = new List<string>();
            if (headers.TryGetValue("X-Forwarded-UserAgent", out var xForwardedUserAgents))
            {
                userAgents.AddRange(xForwardedUserAgents.SelectMany(t => t.Split(new[]
                        {
                            ','
                        }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(q => q.Trim()))
                    .ToList());
            }

            if (headers.TryGetValue("User-Agent", out var useragent))
                userAgents.Add(useragent.ToString());
            if (userAgents.Count > 0)
                return string.Join(",", userAgents);

            return null;
        }
    }
}