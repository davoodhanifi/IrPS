using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Accounts.Repositories
{
    public interface ISessionRepository : IEditableEntityRepository<ISession>
    {
        Task<ISession> GetLastNotVerifiedSessionByMobile(string mobile, CancellationToken cancellationToken);

        Task<ISession> GetByTokenAsync(string token, CancellationToken cancellationToken);
    }
}