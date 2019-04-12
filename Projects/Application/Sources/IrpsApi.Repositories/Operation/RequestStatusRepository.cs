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
    public class RequestStatusRepository : GeneratedQueryEditableRecordRepository<IRequestStatus, RequestStatus>, IRequestStatusRepository
    {
        public RequestStatusRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<IRequestStatus>> GetAllRequestStatusesAsync(CancellationToken cancellationToken = default)
        {
            using (var connection = GetConnection())
            {
                const string query = @"SELECT   [Id]
                                               ,[Title]
                                               ,[TitleEn]
                                       FROM  [Operation].[RequestStatus]";

                return await connection.QueryAsync<RequestStatus>(new CommandDefinition(query, cancellationToken: cancellationToken));
            }
        }
    }
}
