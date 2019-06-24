using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.OnlinePayment.Repositories
{
    public interface IOnlinePaymentRepository : IEditableEntityRepository<IOnlinePayment>
    {
        IOnlinePaymentParameter CreateParameter();

        Task<IOnlinePayment> GetOnlinePaymentByUniqueIdAsync(Guid uniqueId, CancellationToken cancellationToken);

        Task<IEnumerable<IOnlinePayment>> GetOnlinePaymentsAsync(string accountId, CancellationToken cancellationToken);

        Task<IOnlinePaymentParameter> GetOnlinePaymentParameterAsync(string id, CancellationToken cancellationToken);

        Task<IOnlinePaymentParameter> SaveOnlinePaymentParameter(IOnlinePaymentParameter onlinePaymentParameter, CancellationToken cancellationToken);
    }
}