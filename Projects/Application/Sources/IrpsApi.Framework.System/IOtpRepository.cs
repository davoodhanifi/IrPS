using System;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.System
{
    public interface IOtpRepository
    {
       Task<IOtp> GetAsync(int id, CancellationToken cancellationToken);

       Task<IOtp> CreateNewOtpAsync(string phoneNumber, string deviceId, CancellationToken cancellationToken);
    }
}
