using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Api.ExpandOptionsHelpers;
using IrpsApi.Api.Models.Accounting;
using IrpsApi.Framework.Accounting;
using IrpsApi.Framework.Accounting.Repositories;
using IrpsApi.Framework.Accounts.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IrpsApi.Api.Controllers.Accounting
{
    public class BalanceController : RequiresAuthenticationApiControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBalanceRepository _balanceRepository;

        public BalanceController(IAccountRepository accountRepository, IBalanceRepository balanceRepository)
        {
            _accountRepository = accountRepository;
            _balanceRepository = balanceRepository;
        }

        /// <summary>
        /// Get last user balance.
        /// </summary>
        /// <response code="404">invalid_account_id</response>  
        /// <response code="403">forbidden</response>   
        [HttpGet]
        [Route("accounting/{account_id}/balance")]
        [SwaggerResponse(200, type : typeof(BalanceModel))]
        [SwaggerResponse(404)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<BalanceModel>> GetBalanceAsync([FromRoute(Name = "account_id")] string accountId, [FromQuery(Name = "_expand")] ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (accountId != Session.AccountId)
                return Forbid();

            var account = await _accountRepository.GetAsync(accountId, cancellationToken);
            if (account == null)
                return NotFound("invalid_account_id");

            var balance = await _balanceRepository.GetByAccountIdAsync(accountId, cancellationToken);

            return Ok(await balance.ToBalanceModelAsync(GetExpandOptions(expandOptions), cancellationToken));
        }
    }
}