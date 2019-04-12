using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Operation.Repositories
{
    public interface IRequestRepository : IEditableEntityRepository<IRequest>
    {
        IRequestParameter CreateParameter(string key, object value, string type);

        Task<IEnumerable<IRequest>> GetAllRequestsAsync(string accountId, CancellationToken cancellationToken = default);

        Task<IRequest> GetRequestAsync(string accountId, int requestId, CancellationToken cancellationToken = default);

        Task<IRequest> GetRequestAsync(string accountId, string typeId, DateTime dateTime, CancellationToken cancellationToken = default);

    }
}
