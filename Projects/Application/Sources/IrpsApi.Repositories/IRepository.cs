using IrpsApi.Framework;

namespace Noandishan.IrpsApi.Repositories
{
    public interface IRepository
    {
        bool Load(IEntity entity);

        IEntity Create();
    }
}

