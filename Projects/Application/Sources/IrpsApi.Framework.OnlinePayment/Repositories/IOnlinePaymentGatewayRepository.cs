using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.OnlinePayment.Repositories
{
    public interface IOnlinePaymentGatewayRepository : IEditableEntityRepository<IOnlinePaymentGateway>
    {
        Task<IEnumerable<IOnlinePaymentGateway>> GetAllOnlinePaymentGatewaysAsync(CancellationToken cancellationToken = default);
    }
}