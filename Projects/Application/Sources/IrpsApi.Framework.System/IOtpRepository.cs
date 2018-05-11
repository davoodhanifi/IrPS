using System;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.System
{
    public interface IOtpRepository : IEntityRepository<IOtp>
    {
       Task<IOtp> GetAsync(int id, CancellationToken cancellationToken);

       Task<IOtp> CreateAsync(string phoneNumber, CancellationToken cancellationToken);

       Task<IOtp> CheckAsync(string phoneNumber, string password, CancellationToken cancellationToken);

       void DeleteAsync(CancellationToken cancellationToken);
    }
}
