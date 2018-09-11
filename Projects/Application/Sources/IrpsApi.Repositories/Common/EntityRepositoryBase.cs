using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework;
using Mabna.Data;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Common
{
    public abstract class EntityRepositoryBase<TEntity> : RepositoryBase, IEntityRepository<TEntity> where TEntity : IEntity
    {
        protected readonly IConnectionString ConnectionString;

        protected abstract string GetByIdQuery
        {
            get;
        }

        protected EntityRepositoryBase(IConnectionString connectionString)
        {
            ConnectionString = connectionString;
        }

        public virtual TEntity Get(string id)
        {
            var entity = CreateEntity();
            entity.Id = id;

            return (TEntity)(IEntity)entity;
        }

        internal abstract EntityBase CreateEntity();

        protected TEntity ReadEntity(IDataRecord record)
        {
            var entity = CreateEntity();
            entity.Load(record);

            return (TEntity)(IEntity)entity;
        }

        public virtual async Task<TEntity> GetAsync(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var command = new DataCommand(ConnectionString.ConnectionString))
            {
                command.CommandText = GetByIdQuery;
                command.AddParameter("@Id", id);

                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    if (await reader.ReadAsync(cancellationToken))
                        return ReadEntity(reader);
                }
            }

            return default(TEntity);
        }

        internal override Task<DbDataReader> GetRecordAsync(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            var command = new DataCommand(ConnectionString.ConnectionString);

            command.CommandText = GetByIdQuery;
            command.AddParameter("@Id", id);

            return command.ExecuteReaderAsync(cancellationToken);
        }
    }
}
