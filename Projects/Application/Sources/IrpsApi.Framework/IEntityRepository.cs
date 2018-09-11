using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework
{
    public interface IEntityRepository<TEntity> where TEntity : IEntity
    {
        TEntity Get(string id);

        Task<TEntity> GetAsync(string id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
