using IrpsApi.Framework;
using System.Threading;
using System.Threading.Tasks;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Common
{
    public abstract class EditableEntityRepositoryBase<TEntity> : QueryableEntityRepositoryBase<TEntity>, IEditableEntityRepository<TEntity> where TEntity : IEntity
    {
        protected EditableEntityRepositoryBase(string baseTableName, IConnectionString connectionString) : base(baseTableName, connectionString)
        {
        }

        public TEntity Create()
        {
            var entity = CreateEntity();

            return (TEntity)(IEntity)entity;
        }

        public abstract Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken());

        public abstract Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken());

        public abstract Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken());

        public Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(entity.Id))
                return InsertAsync(entity, cancellationToken);
            else
                return UpdateAsync(entity, cancellationToken);
        }
    }
}