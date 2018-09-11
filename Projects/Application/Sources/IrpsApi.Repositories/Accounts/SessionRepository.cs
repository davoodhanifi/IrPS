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
    public class SessionRepository : GeneratedQueryEditableRecordRepository<ISession, Session>, ISessionRepository
    {
        public SessionRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        internal override EntityBase CreateEntity()
        {
            return new Session();
        }

        public async Task<ISession> GetLastNotVerifiedSessionByMobile(string mobile, CancellationToken cancellationToken)
        {
            var lastSession = (await GetAsync(new IFilterParameter[]
            {
                new FilterParameter("Mobile", mobile),
                new FilterParameter("StateId", SessionStateIds.NotVerified),
            }, new ISortOption[]{new SortOption("RecordVersion", SortOrder.Descending) }, new PaginationOption(1), cancellationToken)).FirstOrDefault();

            return lastSession;
        }

        public async Task<ISession> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return (await GetAsync(new IFilterParameter[]
            {
                new FilterParameter("AccessToken", token)
            }, cancellationToken: cancellationToken)).FirstOrDefault();
        }
    }
}