using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using IrpsApi.Framework.Accounts;
using IrpsApi.Framework.Accounts.Repositories;
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
            using (var connection = GetConnection())
            {
                const string query = @"SELECT  [Id]
                                              ,[AccountId]
                                              ,[DateTime]
                                              ,[TypeId]
                                              ,[Title]
                                              ,[TitleEn]
                                              ,[MimeType]
                                              ,[Data]
                                              ,[Note]
                                              ,[FileName]
                                       FROM  [Accounts].[Document]
                                       WHERE [AccountId] = @AccountId AND
                                             [TypeId] = @TypeId 
                                       ORDER BY [DateTime] DESC";

                return await connection.QueryFirstOrDefaultAsync<Document>(new CommandDefinition(query, new { AccountId = accountId, TypeId = typeId}, cancellationToken: cancellationToken));
            }
        }
    }
}