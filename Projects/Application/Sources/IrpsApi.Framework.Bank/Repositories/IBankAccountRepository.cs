using IrpsApi.Framework.Accounts;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Bank.Repositories
{
    public interface IBankAccountRepository : IEditableEntityRepository<IBankAccount>
    {
        Task<IBankAccount> GetBankAccountAsync(string accountId, CancellationToken cancellationToken = default);
    }
}