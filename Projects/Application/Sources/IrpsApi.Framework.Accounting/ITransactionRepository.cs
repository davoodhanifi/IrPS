using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Accounting
{
    public interface ITransactionRepository : IEntityRepository<ITransaction>
    {
        Task<IEnumerable<ITransaction>> GetAllByUserCodeAsync(string userCode, CancellationToken cancellationToken);

        Task<ITransaction> CreateAsync(ITransaction transaction, CancellationToken cancellationToken);

    }
}
