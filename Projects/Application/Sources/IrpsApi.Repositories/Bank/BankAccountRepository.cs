using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework;
using IrpsApi.Framework.Bank;
using IrpsApi.Framework.Bank.Repositories;
using Noandishan.IrpsApi.Repositories.Common;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Bank
{
    public class BankAccountRepository : GeneratedQueryEditableRecordRepository<IBankAccount, BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        public async Task<IBankAccount> GetBankAccountAsync(string accountId, CancellationToken cancellationToken)
        {
            return (await GetAsync(new IFilterParameter[]
            {
                new FilterParameter("AccountId", accountId)
            }, cancellationToken: cancellationToken)).OrderByDescending(item => item.Id).FirstOrDefault();
        }
    }
}
