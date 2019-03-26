using IrpsApi.Framework.Accounts;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Bank.Repositories
{
    public interface IBankAccountRepository : IEditableEntityRepository<IBankAccount>
    {
        Task<IBankAccount> GetBankAccountAsync(IAccount account, CancellationToken cancellationToken = default);
    }
}