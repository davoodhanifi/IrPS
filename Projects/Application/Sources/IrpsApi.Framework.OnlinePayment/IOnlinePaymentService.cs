using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.OnlinePayment
{
    public interface IOnlinePaymentService
    {
        Task<IOnlinePaymentCallbackProcessResult> ProcessCallbackAsync(IOnlinePayment onlinePaymentUniqueId, IDictionary<string, string> parameters, CancellationToken cancellationToken);

        Task<IOnlinePaymentInitResult> InitOnlinePayment(string accountId, string gatewayId, decimal amount, string callbackUrl, string[] additionalData, CancellationToken cancellationToken);
    }
}