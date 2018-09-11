using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework.Accounts;
using IrpsApi.Framework.Accounts.Repositories;
using Mabna.WebApi.Common.Security;

namespace IrpsApi.Api.Security
{
    public class SessionPrincipalProvider : ISessionPrincipalProvider
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IAccountRepository _accountRepository;

        public SessionPrincipalProvider(ISessionRepository sessionRepository, IAccountRepository accountRepository)
        {
            _sessionRepository = sessionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<ClaimsPrincipal> GetPrincipalAsync(string token, CancellationToken cancellationToken = new CancellationToken())
        {
            var session = await _sessionRepository.GetByTokenAsync(token, cancellationToken);
            if (session == null || session.StateId != SessionStateIds.Open)
                return null;

            var account = await _accountRepository.GetAsync(session.AccountId, cancellationToken);

            return new Principal(new UserIdentity(account?.Id), session);
        }
    }
}