using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Accounting.Repositories
{
    public interface IBalanceRepository : IEditableEntityRepository<IBalance>
    {
        Task<IBalance> GetByAccountIdAsync(string accountId, CancellationToken cancellationToken = default);
    }
}
