using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework.OnlinePayment;

namespace IrpsApi.Services.OnlinePayment
{
    public interface IOnlinePaymentClient
    {
        Task<IDictionary<string, string>> InitPaymentAsync(long amount, string tracingId, string[] additionalData, string redirectUrl, CancellationToken cancellationToken);

        CallbackParametersProcessResult ProcessCallbackParameters(IDictionary<string, string> parameters);

        string GatewayUrl
        {
            get;
        }

        Task<VerifyResult> VerifyTransaction(IOnlinePayment onlinePayment, string paygateTransactionId, CancellationToken cancellationToken);
    }
}