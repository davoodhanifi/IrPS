using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Common
{
    public abstract class EditableRecordRepositoryBase<TRecord> : RecordRepositoryBase<TRecord>, IEditableEntityRepository<TRecord> where TRecord : IRecord
    {
        protected EditableRecordRepositoryBase(string baseTableName, IConnectionString connectionString) : base(baseTableName, connectionString)
        {
        }

        public TRecord Create()
        {
            var entity = CreateEntity();

            return (TRecord)(IRecord)entity;
        }

        public abstract Task<TRecord> InsertAsync(TRecord entity, CancellationToken cancellationToken = new CancellationToken());

        public abstract Task<TRecord> UpdateAsync(TRecord entity, CancellationToken cancellationToken = new CancellationToken());

        public abstract Task DeleteAsync(TRecord entity, CancellationToken cancellationToken = new CancellationToken());

        public Task<TRecord> SaveAsync(TRecord entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(entity.Id))
                return InsertAsync(entity, cancellationToken);
            else
                return UpdateAsync(entity, cancellationToken);
        }
    }
}