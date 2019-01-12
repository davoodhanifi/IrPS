using System.Threading;
using System.Threading.Tasks;

namespace IrpsApi.Framework.Accounts.Repositories
{
    public interface IDocumentRepository : IEditableEntityRepository<IDocument>
    {
        Task<IDocument> GetByDocumentTypeAsync(string accountId, string typeId, CancellationToken cancellationToken = default);
    }
}