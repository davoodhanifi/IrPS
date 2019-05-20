using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Accounts.Repositories
{
    public interface IAccountRepository : IEditableEntityRepository<IAccount>
    {
        Task<IAccount> FindAccount(string username, string userCode, string mobile, string email, CancellationToken cancellationToken = default);

        Task<IAccount> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

        Task<IAccount> GetByUserCodeAsync(string userCode, CancellationToken cancellationToken = default);

        Task<IAccount> GetByMobileAsync(string mobile, CancellationToken cancellationToken = default);

        Task<IEnumerable<string>> GetAllUserCodesAsync(CancellationToken cancellationToken = default);
    }
}