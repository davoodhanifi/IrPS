using IrpsApi.Framework;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Common.Generated
{
    public class GeneratedQueryQueryableRecordRepositoryBase<TEntity, TDb> : GeneratedQueryQueryableEntityRepositoryBase<TEntity, TDb> where TDb : GeneratedQueryRecord, new()
        where TEntity : IRecord
    {
        protected override string GenerateGetByIdQuery()
        {
            return SqlGenerator.SelectRecordByIdQuery(TableInfo);
        }

        public GeneratedQueryQueryableRecordRepositoryBase(IConnectionString connectionString) : base(connectionString)
        {
        }
    }
}
