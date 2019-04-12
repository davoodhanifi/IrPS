using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using IrpsApi.Framework;
using IrpsApi.Framework.Accounts;
using IrpsApi.Framework.Accounts.Repositories;
using Noandishan.IrpsApi.Repositories.Common;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Accounts
{
    public class DocumentRepository : GeneratedQueryEditableRecordRepository<IDocument, Document>, IDocumentRepository
    {
        public DocumentRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        public async Task<IDocument> GetByDocumentTypeAsync(string accountId, string typeId, CancellationToken cancellationToken)
        {
            return (await GetAsync(new IFilterParameter[]
            {
                new FilterParameter("AccountId", accountId),
                new FilterParameter("TypeId", typeId)
            }, cancellationToken: cancellationToken)).OrderByDescending(item => item.DateTime).FirstOrDefault();
        }
    }
}