using System;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Accounting
{
    public interface IBalanceRepository : IEntityRepository<IBalance>
    {
        Task<IBalance> GetByUserCodeAsync(string userCode, CancellationToken cancellationToken);
    }
}
