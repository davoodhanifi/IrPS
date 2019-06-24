using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.OnlinePayment.Repositories
{
    public interface IOnlinePaymentStateRepository : IEditableEntityRepository<IOnlinePaymentState>
    {
        Task<IEnumerable<IOnlinePaymentState>> GetAllOnlinePaymentStatesAsync(CancellationToken cancellationToken = default);
    }
}