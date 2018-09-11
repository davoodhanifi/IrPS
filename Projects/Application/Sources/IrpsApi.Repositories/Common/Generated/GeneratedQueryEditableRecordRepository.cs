using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using IrpsApi.Framework;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Common.Generated
{
    public class GeneratedQueryEditableRecordRepository<TEntity, TDb> : GeneratedQueryEditableEntityRepositoryBase<TEntity, TDb> 
        where TDb : EntityBase, new()
        where TEntity : IRecord
    {
        public GeneratedQueryEditableRecordRepository(IConnectionString connectionString) : base(connectionString)
        {
        }

        public async Task<TEntity> ExecuteUpdateInsertAsync(string query, TEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var connection = GetConnection())
            {
                using (var reader = await connection.ExecuteReaderAsync(new CommandDefinition(query, entity, cancellationToken: cancellationToken)))
                {
                    var dbDataReader = reader as DbDataReader;
                    if (dbDataReader != null)
                        await ((DbDataReader)reader).ReadAsync(cancellationToken);
                    else
                        reader.Read();

                    entity.Id = ((int)reader["Id"]).ToString();
                    var recordInfo = reader.GetRecordInfo();
                    entity.RecordVersion = recordInfo.Version;
                    entity.RecordState = recordInfo.State;
                    entity.RecordInsertDateTime = recordInfo.InsertDateTime;
                    entity.RecordUpdateDateTime = recordInfo.UpdateDateTime;
                    entity.RecordDeleteDateTime = recordInfo.DeleteDateTime;

                    return entity;
                }
            }
        }

        public override Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            return ExecuteUpdateInsertAsync(InsertQuery, entity, cancellationToken);
        }

        public override Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            return ExecuteUpdateInsertAsync(UpdateQuery, entity, cancellationToken);
        }

        public override List<IFilterParameter> GetExtraFilters(IEnumerable<IFilterParameter> filterParameters)
        {
            var filters = base.GetExtraFilters(filterParameters);

            if ((filterParameters == null || !filterParameters.Any(f => f.FieldName == "RecordState" || f.FieldName == "RecordVersion")) && (filters == null || !filters.Any(f => f.FieldName == "RecordState" || f.FieldName == "RecordVersion")))
                filters.Add(new FilterParameter("RecordState", FilterOperatorType.NotEqual, RecordState.Deleted));

            return filters;
        }
    }
}