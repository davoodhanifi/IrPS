using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework.Accounting;
using Microsoft.Extensions.Configuration;

namespace Noandishan.IrpsApi.Repositories.Accounting
{
    public class BalanceRepository : EntityRepositoryBase<IBalance>, IBalanceRepository
    {
        public BalanceRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IBalance> GetByUserCodeAsync(string userCode, CancellationToken cancellationToken)
        {
            var command = GetCommand();
            command.CommandText = "SELECT * From [Accounting].[Balance] WHERE [UserCode] = @userCode AND [IsActive] = 1";

            command.AddParameter("@userCode", userCode);

            using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                if (await reader.ReadAsync(cancellationToken))
                    return SetEntity(reader);

            return null;
        }

        public async Task<IBalance> CreateAsync(IBalance balance, CancellationToken cancellationToken)
        {
            DeleteAsync(balance.UserCode, cancellationToken);

            balance.DateTime = DateTime.Now;

            var command = GetCommand();
            command.CommandText = "INSERT INTO [Accounting].[Balance] ([UserCode], [DateTime], [Balance], [IsActive], [Notes]) OUTPUT Inserted.Id VALUES (@userCode, @dateTime, @balance, @isActive, @notes)";

            command.AddParameter("@userCode", balance.UserCode);
            command.AddParameter("@dateTime", balance.DateTime);
            command.AddParameter("@balance", balance.CurrentBalance);
            command.AddParameter("@isActive", balance.IsActive);
            command.AddParameter("@notes", balance.Notes);

            balance.Id = await command.ExecuteScalarAsync<int>(cancellationToken);

            return balance;
        }

        public async void DeleteAsync(string userCode, CancellationToken cancellationToken)
        {
            var command = GetCommand();
            command.CommandText = "UPDATE [Accounting].[Balance] SET [IsActive] = 0 WHERE [UserCode] = @userCode";

            command.AddParameter("@userCode", userCode);

            await command.ExecuteNonQueryAsync(cancellationToken);
        }

        protected override IBalance SetEntity(IDataReader reader)
        {
            return new Balance
            {
                Id = reader.ReadInt32("Id"),
                UserCode = reader.ReadString("UserCode"),
                DateTime = reader["DateTime"] is DateTime ? (DateTime)reader["DateTime"] : new DateTime(),
                CurrentBalance = reader.ReadDecimal("Balance"),
                IsActive = reader.ReadBoolean("IsActive"),
                Notes = reader.ReadString("Notes")
            };
        }
    }
}
