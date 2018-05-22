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
