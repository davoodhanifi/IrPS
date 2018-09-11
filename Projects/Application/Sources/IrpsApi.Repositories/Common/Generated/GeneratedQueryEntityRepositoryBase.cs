using IrpsApi.Framework;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Common.Generated
{
    public class GeneratedQueryEntityRepositoryBase<TEntity, TDb> : EntityRepositoryBase<TEntity>
        where TEntity : IEntity
        where TDb : EntityBase, new()
    {
        public GeneratedQueryEntityRepositoryBase(string tableName, string schemaName, IConnectionString connectionString) : base(connectionString)
        {
            GetByIdQuery = SqlGenerator.SelectByIdQuery<TDb>(tableName, schemaName);
        }

        protected override string GetByIdQuery
        {
            get;
        }

        internal override EntityBase CreateEntity()
        {
            return new TDb();
        }
    }
}