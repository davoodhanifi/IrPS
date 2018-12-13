using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Accounts.Repositories
{
    public interface IPersonProfileRepository : IEditableEntityRepository<IPersonProfile>
    {
        Task<IPersonProfile> GetAsync(IAccount account, CancellationToken cancellationToken = default);
    }
}