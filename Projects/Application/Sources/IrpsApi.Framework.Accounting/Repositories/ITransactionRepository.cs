using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Accounting.Repositories
{
    public interface ITransactionRepository : IEditableEntityRepository<ITransaction>
    {
        Task<IEnumerable<ITransaction>> GetAllTransactionsAsync(string accountId, CancellationToken cancellationToken);

        Task<ITransaction> GetTransactionAsync(string accountId, int transactionId, CancellationToken cancellationToken);
    }
}
