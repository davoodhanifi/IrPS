using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using IrpsApi.Framework;
using IrpsApi.Framework.Operation;
using IrpsApi.Framework.Operation.Repositories;
using Noandishan.IrpsApi.Repositories.Common;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Operation
{
    public class RequestRepository : GeneratedQueryEditableRecordRepository<IRequest, Request>, IRequestRepository
    {
        public RequestRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        public IRequestParameter CreateParameter(string key, object value, string type)
        {
            return SerializeHelper.CreateRequestParameter(key, type, value);
        }

        public async Task<IEnumerable<IRequest>> GetAllRequestsAsync(string accountId, CancellationToken cancellationToken)
        {
            using (var connection = GetConnection())
            {
                const string query = @"SELECT  [Id]
                                              ,[AccountId]
                                              ,[TypeId]
                                              ,[StatusId]
                                              ,[DateTime]
                                              ,[Parameters]
                                              ,[Comments]
                                       FROM  [Operation].[Request]
                                       WHERE [AccountId] = @AccountId
                                       ORDER BY [DateTime] DESC";

                return await connection.QueryAsync<Request>(new CommandDefinition(query, new { AccountId = accountId }, cancellationToken: cancellationToken));
            }
        }

        public async Task<IRequest> GetRequestAsync(string accountId, int requestId, CancellationToken cancellationToken = default)
        {
            return (await GetAsync(new IFilterParameter[]
            {
                new FilterParameter("AccountId", accountId),
                new FilterParameter("Id", requestId)
            }, cancellationToken: cancellationToken)).FirstOrDefault();
        }

        public async Task<IRequest> GetRequestAsync(string accountId, string typeId, DateTime dateTime, CancellationToken cancellationToken = default)
        {
            using (var connection = GetConnection())
            {
                const string query = @"SELECT  [Id]
                                              ,[AccountId]
                                              ,[TypeId]
                                              ,[StatusId]
                                              ,[DateTime]
                                              ,[Parameters]
                                              ,[Comments]
                                       FROM  [Operation].[Request]
                                       WHERE [AccountId] = @AccountId AND
                                             [TypeId] = @TypeId AND
                                             Cast([DateTime] AS Date) = @DateTime
                                       ORDER BY [DateTime] DESC";

                return await connection.QueryFirstOrDefaultAsync<Request>(new CommandDefinition(query, new { AccountId = accountId, TypeId = typeId, DateTime = dateTime.Date }, cancellationToken: cancellationToken));
            }
        }
    }
}
