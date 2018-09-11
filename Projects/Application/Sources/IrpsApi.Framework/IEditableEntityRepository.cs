using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework
{
    public interface IEditableEntityRepository<TEntity> : IQueryableEntityRepository<TEntity> where TEntity : IEntity
    {
        TEntity Create();

        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}
