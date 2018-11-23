using IrpsApi.Framework.Accounting;
using IrpsApi.Framework.Accounting.Repositories;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Accounting
{
    public class TransactionTypeRepository : GeneratedQueryEditableRecordRepository<ITransactionType, TransactionType>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }
    }
}
