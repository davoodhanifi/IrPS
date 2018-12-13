using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using IrpsApi.Framework.Accounts;
using IrpsApi.Framework.Accounts.Repositories;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Accounts
{
    public class PushTargetRepository : GeneratedQueryEditableRecordRepository<IPushTarget, PushTarget>, IPushTargetRepository
    {
        public PushTargetRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<IPushTarget>> GetAllByTokenAndAccountIdAsync(string token, string accountId, CancellationToken cancellationToken)
        {
            using (var connection = GetConnection())
            {
                const string query = @"SELECT [Id],
                                              [AccountId],
                                              [Token],
                                              [TypeId],
                                              [PlatformUniqueId],
                                              [PlatformName],
                                              [PlatformVersion],
                                              [RegistrationDateTime],
                                              [StatusId]
                                       FROM  [Accounts].[PushTarget]
                                       WHERE [AccountId] = @AccountId AND
                                             [Token] = @Token AND
                                             [StatusId] = 1
                                       ORDER BY [RegistrationDateTime] DESC";

                return await connection.QueryAsync<PushTarget>(new CommandDefinition(query, new { AccountId = accountId, Token = token}, cancellationToken: cancellationToken));
            }
        }

        public async Task<IEnumerable<IPushTarget>> GetAllByAccountIdAsync(string accountId, CancellationToken cancellationToken = default)
        {
            using (var connection = GetConnection())
            {
                const string query = @"SELECT [Id],
                                              [AccountId],
                                              [Token],
                                              [TypeId],
                                              [PlatformUniqueId],
                                              [PlatformName],
                                              [PlatformVersion],
                                              [RegistrationDateTime],
                                              [StatusId]
                                       FROM  [Accounts].[PushTarget]
                                       WHERE [AccountId] = @AccountId AND
                                             [StatusId] = 1
                                       ORDER BY [RegistrationDateTime] DESC";

                return await connection.QueryAsync<PushTarget>(new CommandDefinition(query, new { AccountId = accountId }, cancellationToken: cancellationToken));
            }
        }

        public async Task<IEnumerable<IPushTarget>> UpdateStatusByTokenAsync(string token, string statusId, CancellationToken cancellationToken)
        {
            using (var connection = GetConnection())
            {
                const string query = @"UPDATE [Accounts].[PushTarget]
                                       SET [StatusId] = @StatusId
                                       WHERE [Token] = @Token";

                return await connection.QueryAsync<PushTarget>(new CommandDefinition(query, new { StatusId = statusId, Token = token }, cancellationToken: cancellationToken));
            }
        }
    }
}