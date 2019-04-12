using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Operation.Repositories
{
    public interface IRequestTypeRepository : IEditableEntityRepository<IRequestType>
    {
        Task<IEnumerable<IRequestType>> GetAllRequestTypesAsync(CancellationToken cancellationToken = default);
    }
}