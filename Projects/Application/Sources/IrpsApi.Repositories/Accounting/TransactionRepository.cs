using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using IrpsApi.Framework.Accounting;
using IrpsApi.Framework.Accounting.Repositories;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Accounting
{
    public class TransactionRepository : GeneratedQueryEditableRecordRepository<ITransaction, Transaction>, ITransactionRepository
    {
        public TransactionRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<ITransaction>> GetAllTransactionsAsync(string accountId, CancellationToken cancellationToken)
        {
            using (var connection = GetConnection())
            {
                const string query = @"SELECT [Id],
                                              [FromAccountId],
                                              [ToAccountId],
                                              [Amount],
                                              [DateTime],
                                              [Description],
                                              [TypeId],
                                              [OnlinePaymentId]
                                       FROM [Accounting].[Transaction] 
                                       WHERE [FromAccountId] = @AccountId OR
                                             [ToAccountId] = @AccountId 
                                       ORDER BY [DateTime] DESC";

                return await connection.QueryAsync<Transaction>(new CommandDefinition(query, new { AccountId = accountId}, cancellationToken: cancellationToken));
            }
        }

        public async Task<ITransaction> GetTransactionAsync(string accountId, int transactionId, CancellationToken cancellationToken)
        {
            using (var connection = GetConnection())
            {
                const string query = @"SELECT [Id],
                                              [FromAccountId],
                                              [ToAccountId],
                                              [Amount],
                                              [DateTime],
                                              [Description],
                                              [TypeId],
                                              [OnlinePaymentId]
                                       FROM [Accounting].[Transaction] 
                                       WHERE [Id] = @TransactionId AND 
                                             ([FromAccountId] = @AccountId OR
                                             [ToAccountId] = @AccountId)";

                return await connection.QueryFirstOrDefaultAsync<Transaction>(new CommandDefinition(query, new { AccountId = accountId, TransactionId = transactionId}, cancellationToken: cancellationToken));
            }
        }
    }
}
