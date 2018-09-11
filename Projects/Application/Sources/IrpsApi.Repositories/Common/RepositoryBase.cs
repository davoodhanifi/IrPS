using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Noandishan.IrpsApi.Repositories.Common
{
    public abstract class RepositoryBase
    {
        internal abstract Task<DbDataReader> GetRecordAsync(string id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
