using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Accounts.Repositories
{
    public interface IPushTargetRepository : IEditableEntityRepository<IPushTarget>
    {
        Task<IEnumerable<IPushTarget>> GetAllByTokenAndAccountIdAsync(string token, string accountId, CancellationToken cancellationToken = default);

        Task<IEnumerable<IPushTarget>> GetAllByAccountIdAsync(string accountId, CancellationToken cancellationToken = default);

        Task<IEnumerable<IPushTarget>> UpdateStatusByTokenAsync(string token, string statusId, CancellationToken cancellationToken = default);
    }
}