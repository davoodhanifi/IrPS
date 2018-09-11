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
    public class AccountRepository : GeneratedQueryEditableRecordRepository<IAccount, Account>, IAccountRepository
    {
        public AccountRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }

        public async Task<IAccount> FindAccount(string username, string userCode, string mobile, string email, CancellationToken cancellationToken = default)
        {
            using (var connection = GetConnection())
            {
                const string query = @"SELECT * 
                                       FROM  Accounts.Account 
                                       WHERE RecordState < 2
                                             AND (Username = @Username OR
                                                  UserCode = @UserCode OR 
                                                  Email = @Email OR 
                                                  Mobile = @Mobile)";

                return await connection.QueryFirstOrDefaultAsync<Account>(new CommandDefinition(query, new { Username = username, UserCode = userCode, Mobile = mobile, Email = email }, cancellationToken: cancellationToken));
            }
        }

        public async Task<IAccount> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return (await GetAsync(new IFilterParameter[]
            {
                new FilterParameter("Username", username),
            }, cancellationToken: cancellationToken)).FirstOrDefault();
        }

        public async Task<IAccount> GetByUserCodeAsync(string userCode, CancellationToken cancellationToken)
        {
            return (await GetAsync(new IFilterParameter[]
            {
                new FilterParameter("UserCode", userCode),
            }, cancellationToken: cancellationToken)).FirstOrDefault();
        }

        public async Task<IAccount> GetByMobileAsync(string mobile, CancellationToken cancellationToken)
        {
            return (await GetAsync(new IFilterParameter[]
            {
                new FilterParameter("Mobile", mobile),
            }, cancellationToken: cancellationToken)).FirstOrDefault();
        }
    }
}