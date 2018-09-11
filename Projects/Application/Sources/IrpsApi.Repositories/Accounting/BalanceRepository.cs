using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework;
using IrpsApi.Framework.Accounting;
using IrpsApi.Framework.Accounting.Repositories;
using Noandishan.IrpsApi.Repositories.Common;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Accounting
{
    public class BalanceRepository : GeneratedQueryEditableRecordRepository<IBalance, Balance>, IBalanceRepository
    {
        public BalanceRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        public async Task<IBalance> GetByAccountIdAsync(string accountId, CancellationToken cancellationToken)
        {
            return (await GetAsync(new IFilterParameter[]
            {
                new FilterParameter("AccountId", accountId),
                new FilterParameter("IsActive", true)
            }, cancellationToken: cancellationToken)).OrderByDescending(item => item.Id).FirstOrDefault();
        }
    }
}
