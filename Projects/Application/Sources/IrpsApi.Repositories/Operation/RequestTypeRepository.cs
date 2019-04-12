using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using IrpsApi.Framework.Operation;
using IrpsApi.Framework.Operation.Repositories;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Operation
{
    public class RequestTypeRepository : GeneratedQueryEditableRecordRepository<IRequestType, RequestType>, IRequestTypeRepository
    {
        public RequestTypeRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<IRequestType>> GetAllRequestTypesAsync(CancellationToken cancellationToken = default)
        {
            using (var connection = GetConnection())
            {
                const string query = @"SELECT   [Id]
                                               ,[Title]
                                               ,[TitleEn]
                                       FROM  [Operation].[RequestType]";

                return await connection.QueryAsync<RequestType>(new CommandDefinition(query, cancellationToken: cancellationToken));
            }
        }
    }
}
