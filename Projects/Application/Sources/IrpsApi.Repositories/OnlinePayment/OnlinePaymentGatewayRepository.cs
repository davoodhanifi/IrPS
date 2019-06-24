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
    public class OnlinePaymentGatewayRepository : GeneratedQueryEditableRecordRepository<IOnlinePaymentGateway, OnlinePaymentGateway>, IOnlinePaymentGatewayRepository
    {
        public OnlinePaymentGatewayRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<IOnlinePaymentGateway>> GetAllOnlinePaymentGatewaysAsync(CancellationToken cancellationToken = default)
        {

            using (var connection = GetConnection())
            {
                const string query = @"SELECT   [Id]
                                               ,[Title]
                                               ,[TitleEn]
                                       FROM  [OnlinePayment].[OnlinePaymentGateway]";

                return await connection.QueryAsync<OnlinePaymentGateway>(new CommandDefinition(query, cancellationToken: cancellationToken));
            }
        }
    }
}