using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Api.ExpandOptionsHelpers;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Framework.Accounts.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IrpsApi.Api.Controllers.Accounts
{
    public class PersonProfileController : RequiresAuthenticationApiControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPersonProfileRepository _personProfileRepository;

        public PersonProfileController(IAccountRepository accountRepository, IPersonProfileRepository personProfileRepository)
        {
            _accountRepository = accountRepository;
            _personProfileRepository = personProfileRepository;
        }

        /// <summary>
        /// Get person profile.
        /// </summary>
        /// <response code="404">invalid_account_id</response>  
        /// <response code="403">forbidden</response>
        [HttpGet]
        [Route("accounts/{account_id}/profile/person")]
        [SwaggerResponse(200, type : typeof(PersonProfileModel))]
        [SwaggerResponse(404)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<PersonProfileModel>> GetPersonProfileAsync([FromRoute(Name = "account_id")]string accountId, [FromQuery(Name = "_expand")]ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (accountId != Session.AccountId)
                return Forbid();

            var account = await _accountRepository.GetAsync(accountId, cancellationToken);
            if (account == null)
                return NotFound("invalid_account_id");

            var profile = await _personProfileRepository.GetAsync(account, cancellationToken);
            if (profile == null)
                return NotFound();

            return Ok(await profile.ToPersonProfileModelAsync(GetExpandOptions(expandOptions), cancellationToken));
        }

        /// <summary>
        /// Put person profile.
        /// </summary>
        /// <response code="404">invalid_account_id</response>  
        /// <response code="403">forbidden</response>
        [HttpPut]
        [Route("accounts/{account_id}/profile/person")]
        [SwaggerResponse(200, type : typeof(PersonProfileModel))]
        [SwaggerResponse(404)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<PersonProfileModel>> PutPersonProfileAsync([FromRoute(Name = "account_id")]string accountId, [FromBody]InputProfileModel model, [FromQuery(Name = "_expand")]ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (accountId != Session.AccountId)
                return Forbid();

            var account = await _accountRepository.GetAsync(accountId, cancellationToken);
            if (account == null)
                return NotFound("invalid_account_id");

            var existingProfile = await _personProfileRepository.GetAsync(account, cancellationToken);

            var profile = model.ToPersonProfile();
            profile.AccountId = accountId;
            if (existingProfile != null)
                profile.Id = existingProfile.Id;

            profile = await _personProfileRepository.SaveAsync(profile, cancellationToken);
            return Ok(await profile.ToPersonProfileModelAsync(GetExpandOptions(expandOptions), cancellationToken));
        }
    }
}