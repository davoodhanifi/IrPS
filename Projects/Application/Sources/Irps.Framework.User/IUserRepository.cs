using IrpsApi.Framework;

namespace Irps.Framework.User
{
    public interface IUserRepository : IEntityRepository<IUser>
    {
        IUser GetByUsername(string username);
    }
}
