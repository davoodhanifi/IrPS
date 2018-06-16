using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework.Accounting;
using Microsoft.Extensions.Configuration;

namespace Noandishan.IrpsApi.Repositories.Accounting
{
    public class TransactionRepository : EntityRepositoryBase<ITransaction>, ITransactionRepository
    {
        public TransactionRepository(IConfiguration configuration) : base(configuration)
        {
        }
        
        public async Task<IEnumerable<ITransaction>> GetAllByUserCodeAsync(string userCode, CancellationToken cancellationToken)
        {
            var command = GetCommand();
            command.CommandText = "SELECT * From [Accounting].[Transaction] WHERE [FromUserCode] = @userCode OR [ToUserCode] = @userCode ORDER BY [DateTime] DESC";

            command.AddParameter("@userCode", userCode);

            var entities = FetchEntities(command);

            return entities;
        }

        public async Task<ITransaction> CreateAsync(ITransaction transaction, CancellationToken cancellationToken)
        {
            transaction.DateTime = DateTime.Now;

            var command = GetCommand();
            command.CommandText = "INSERT INTO [Accounting].[Transaction] ([FromUserCode], [ToUserCode], [Amount], [DateTime], [TransactionType],[Notes]) OUTPUT Inserted.Id VALUES(@fromUserCode, @toUserCode, @amount, @dateTime, @transactionType, @notes)";

            command.AddParameter("@fromUserCode", transaction.FromUserCode);
            command.AddParameter("@toUserCode", transaction.ToUserCode);
            command.AddParameter("@amount", transaction.Amount);
            command.AddParameter("@dateTime", transaction.DateTime);
            command.AddParameter("@transactionType", (int)transaction.TransactionType);
            command.AddParameter("@notes", transaction.Notes);

            transaction.Id = await command.ExecuteScalarAsync<int>(cancellationToken);

            return transaction;
        }

        protected override ITransaction SetEntity(IDataReader reader)
        {
            return new Transaction
            {
                Id = reader.ReadInt32("Id"),
                FromUserCode = reader.ReadString("FromUserCode"),
                ToUserCode = reader.ReadString("ToUserCode"),
                Amount = reader.ReadDecimal("Amount"),
                DateTime = reader["DateTime"] is DateTime ? (DateTime)reader["DateTime"] : new DateTime(),
                TransactionType = (TransactionType)reader.ReadByte("TransactionType"),
                Notes = reader.ReadString("Notes")
            };
        }
    }
}
