using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.User
{
    public interface IUserRepository : IEntityRepository<IUser>
    {
        Task<IUser> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancelationToken);

        Task<IUser> GetAsync(int id, CancellationToken cancellationToken);

        Task<IUser> CreateAsync(IUser user, CancellationToken cancellationToken);
    }
}
