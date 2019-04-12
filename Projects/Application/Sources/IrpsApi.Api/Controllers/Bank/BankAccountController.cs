using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Api.ExpandOptionsHelpers;
using IrpsApi.Api.Models.Bank;
using IrpsApi.Framework.Accounts.Repositories;
using IrpsApi.Framework.Bank.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IrpsApi.Api.Controllers.Bank
{
    public class BankAccountController : RequiresAuthenticationApiControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBankAccountRepository _bankAccountRepository;

        public BankAccountController(IAccountRepository accountRepository, IBankAccountRepository bankAccountRepository)
        {
            _accountRepository = accountRepository;
            _bankAccountRepository = bankAccountRepository;

            ExpandEngines.Add("account", _accountRepository.GetAsync);
        }

        /// <summary>
        /// Get user bank account by account id.
        /// </summary>
        /// <response code="404">invalid_account_id</response>  
        /// <response code="403">forbidden</response>
        [HttpGet]
        [Route("bank/bankaccounts/{account_id}")]
        [SwaggerResponse(200, type : typeof(BankAccountModel))]
        [SwaggerResponse(404)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<BankAccountModel>> GetUserBankAccountAsync([FromRoute(Name = "account_id")]string accountId, [FromQuery(Name = "_expand")]ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (accountId != Session.AccountId)
                return Forbid();

            var account = await _accountRepository.GetAsync(accountId, cancellationToken);
            if (account == null)
                return NotFound("invalid_account_id");

            var bankAccount = await _bankAccountRepository.GetBankAccountAsync(accountId, cancellationToken);
            if (bankAccount == null)
                return NotFound();

            return Ok(await bankAccount.ToBankAccountModelAsync(GetExpandOptions(expandOptions), cancellationToken));
        }

        /// <summary>
        /// Put user bank account.
        /// </summary>
        /// <response code="404">invalid_account_id</response>  
        /// <response code="403">forbidden</response>
        [HttpPut]
        [Route("bank/bankaccounts/{account_id}")]
        [SwaggerResponse(200, type: typeof(BankAccountModel))]
        [SwaggerResponse(404)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<BankAccountModel>> PutUserBankAccountAsync([FromRoute(Name = "account_id")]string accountId, [FromBody]InputBankAccountModel model, [FromQuery(Name = "_expand")]ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (accountId != Session.AccountId)
                return Forbid();

            var account = await _accountRepository.GetAsync(accountId, cancellationToken);
            if (account == null)
                return NotFound("invalid_account_id");

            var existingBankAccount = await _bankAccountRepository.GetBankAccountAsync(accountId, cancellationToken);

            var bankAccount = model.ToBankAccount();
            bankAccount.AccountId = accountId;
            if (existingBankAccount != null)
                bankAccount.Id = existingBankAccount.Id;

            bankAccount = await _bankAccountRepository.SaveAsync(bankAccount, cancellationToken);
            return Ok(await bankAccount.ToBankAccountModelAsync(GetExpandOptions(expandOptions), cancellationToken));
        }
    }
}