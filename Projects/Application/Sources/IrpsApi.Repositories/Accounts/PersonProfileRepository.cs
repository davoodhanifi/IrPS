using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework;
using IrpsApi.Framework.Accounts;
using IrpsApi.Framework.Accounts.Repositories;
using Noandishan.IrpsApi.Repositories.Common;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.Accounts
{
    public class PersonProfileRepository : GeneratedQueryEditableRecordRepository<IPersonProfile, PersonProfile>, IPersonProfileRepository
    {
        public PersonProfileRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        public async Task<IPersonProfile> GetAsync(IAccount account, CancellationToken cancellationToken = default)
        {
            return (await GetAsync(new IFilterParameter[]
            {
                new FilterParameter("AccountId", account.Id),
            }, cancellationToken: cancellationToken)).FirstOrDefault();
        }
    }
}