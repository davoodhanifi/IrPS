using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using IrpsApi.Framework.Accounts;
using IrpsApi.Framework.Bank;
using IrpsApi.Framework.Bank.Repositories;
using Noandishan.IrpsApi.Repositories.Bank;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Accounting
{
    public class BankAccountRepository : GeneratedQueryEditableRecordRepository<IBankAccount, BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        public async Task<IBankAccount> GetBankAccountAsync(IAccount account, CancellationToken cancellationToken)
        {
            using (var connection = GetConnection())
            {
                const string query = @"SELECT [Id],
                                              [AccountId],
                                              [BankId],
                                              [Number],
                                              [Iban],
                                              [BranchName],
                                              [BranchNameEn],
                                              [BranchCode],
                                              [TypeId],
                                              [DailyPayment]
                                       FROM  [Bank].[BankAccount]
                                       WHERE [AccountId] = @AccountId 
                                       ORDER BY [Id] DESC";

                return await connection.QueryFirstOrDefaultAsync<BankAccount>(new CommandDefinition(query, new { AccountId = account.Id}, cancellationToken: cancellationToken));
            }
        }
    }
}
