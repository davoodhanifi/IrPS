using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using IrpsApi.Framework.OnlinePayment;
using IrpsApi.Framework.OnlinePayment.Repositories;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.OnlinePayment
{
    public class OnlinePaymentStateRepository : GeneratedQueryEditableRecordRepository<IOnlinePaymentState, OnlinePaymentState>, IOnlinePaymentStateRepository
    {
        public OnlinePaymentStateRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<IOnlinePaymentState>> GetAllOnlinePaymentStatesAsync(CancellationToken cancellationToken = default)
        {

            using (var connection = GetConnection())
            {
                const string query = @"SELECT   [Id]
                                               ,[Title]
                                               ,[TitleEn]
                                       FROM  [OnlinePayment].[OnlinePaymentState]";

                return await connection.QueryAsync<OnlinePaymentState>(new CommandDefinition(query, cancellationToken: cancellationToken));
            }
        }
    }
}