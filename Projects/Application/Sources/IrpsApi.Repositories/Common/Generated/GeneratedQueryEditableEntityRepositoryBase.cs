using System.Threading;
using System.Threading.Tasks;
using Dapper;
using IrpsApi.Framework;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Common.Generated
{
    public class GeneratedQueryEditableEntityRepositoryBase<TEntity, TDb> : GeneratedQueryQueryableEntityRepositoryBase<TEntity, TDb>, IEditableEntityRepository<TEntity> where TEntity : IEntity
        where TDb : EntityBase, new()
    {
        protected string InsertQuery;
        protected string UpdateQuery;
        protected string DeleteQuery;

        public GeneratedQueryEditableEntityRepositoryBase(IConnectionString connectionString) : base(connectionString)
        {
            GenerateQueries();
        }

        protected void GenerateQueries()
        {
            InsertQuery = GenerateInsertQuery(TableInfo);
            UpdateQuery = GenerateUpdateQuery(TableInfo);
            DeleteQuery = GenerateDeleteQuery(TableInfo);
        }

        protected override string GenerateGetByIdQuery()
        {
            return SqlGenerator.SelectRecordByIdQuery(TableInfo);
        }

        protected virtual string GenerateInsertQuery(TableInformation tableInformation)
        {
            return SqlGenerator.InsertQuery(tableInformation);
        }

        protected virtual string GenerateUpdateQuery(TableInformation tableInformation)
        {
            return SqlGenerator.UpdateQuery(tableInformation);
        }

        protected virtual string GenerateDeleteQuery(TableInformation tableInformation)
        {
            return SqlGenerator.DeleteQuery(tableInformation);
        }

        internal override EntityBase CreateEntity()
        {
            return new TDb();
        }

        public virtual TEntity Create()
        {
            return (TEntity)(IEntity)CreateEntity();
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var connection = GetConnection())
            {
                entity.Id = (await connection.ExecuteScalarAsync<int>(new CommandDefinition(InsertQuery, entity, cancellationToken: cancellationToken))).ToString();
            }

            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var connection = GetConnection())
            {
                await connection.ExecuteAsync(new CommandDefinition(UpdateQuery, entity, cancellationToken: cancellationToken));
            }

            return entity;
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var connection = GetConnection())
            {
                await connection.ExecuteAsync(new CommandDefinition(DeleteQuery, new { entity.Id }, cancellationToken: cancellationToken));
            }
        }

        public Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(entity.Id))
                return InsertAsync(entity, cancellationToken);
            else
                return UpdateAsync(entity, cancellationToken);
        }
    }
}