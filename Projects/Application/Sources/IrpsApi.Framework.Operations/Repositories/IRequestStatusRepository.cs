using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Operation.Repositories
{
    public interface IRequestStatusRepository : IEditableEntityRepository<IRequestStatus>
    {
        Task<IEnumerable<IRequestStatus>> GetAllRequestStatusesAsync(CancellationToken cancellationToken = default);
    }
}