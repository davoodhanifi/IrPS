using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Accounts.Repositories
{
    public interface IAccountRepository : IEditableEntityRepository<IAccount>
    {
        Task<IAccount> FindAccount(string username, string userCode, string mobile, string email, CancellationToken cancellationToken);

        Task<IAccount> GetByUsernameAsync(string username, CancellationToken cancellationToken);

        Task<IAccount> GetByUserCodeAsync(string userCode, CancellationToken cancellationToken);

        Task<IAccount> GetByMobileAsync(string mobile, CancellationToken cancellationToken);
    }
}