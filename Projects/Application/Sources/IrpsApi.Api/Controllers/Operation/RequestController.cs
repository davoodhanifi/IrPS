using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Api.ExpandOptionsHelpers;
using IrpsApi.Api.Models.Operation;
using IrpsApi.Framework.Accounting.Repositories;
using IrpsApi.Framework.Accounts.Repositories;
using IrpsApi.Framework.Bank.Repositories;
using IrpsApi.Framework.Operation;
using IrpsApi.Framework.Operation.Repositories;
using Mabna.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IrpsApi.Api.Controllers.Operation
{
    public class RequestController : RequiresAuthenticationApiControllerBase
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IBankAccountRepository _bankAccountRepository;

        public RequestController(IRequestRepository requestRepository, IAccountRepository accountRepository, IRequestTypeRepository requestTypeRepository, IRequestStatusRepository requestStatusRepository, IPersonProfileRepository personProfileRepository, IBalanceRepository balanceRepository, IBankAccountRepository bankAccountRepository)
        {
            _requestRepository = requestRepository;
            _accountRepository = accountRepository;
            _balanceRepository = balanceRepository;
            _bankAccountRepository = bankAccountRepository;

            ExpandEngines.Add("account", accountRepository.GetAsync);
            ExpandEngines.Add("type", requestTypeRepository.GetAsync);
            ExpandEngines.Add("status", requestStatusRepository.GetAsync);
        }

        /// <summary>
        /// Get user requests.
        /// </summary>
        /// <response code="404">invalid_account_id</response>  
        /// <response code="403">forbidden</response>
        [HttpGet]
        [Route("operation/{account_id}/requests")]
        [SwaggerResponse(200, type: typeof(IEnumerable<RequestModel>))]
        [SwaggerResponse(404)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<IEnumerable<RequestModel>>> GetAllRequestsAsync([FromRoute(Name = "account_id")] string accountId, [FromQuery(Name = "_expand")] ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (accountId != Session.AccountId)
                return Forbid();

            var account = await _accountRepository.GetAsync(accountId, cancellationToken);
            if (account == null)
                return NotFound("invalid_account_id");

            var requests = await _requestRepository.GetAllRequestsAsync(accountId, cancellationToken);
            var result = new List<RequestModel>();
            foreach (var request in requests)
                result.Add(await request.ToRequestModelAsync(GetExpandOptions(expandOptions), cancellationToken));

            return Ok(result);
        }

        /// <summary>
        /// Get user request.
        /// </summary>
        /// <response code="404">invalid_account_id, invalid_request_id</response>  
        /// <response code="403">forbidden</response>
        [HttpGet]
        [Route("operation/{account_id}/requests/{request_id}")]
        [SwaggerResponse(200, type: typeof(RequestModel))]
        [SwaggerResponse(404)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<RequestModel>> GetRequestAsync([FromRoute(Name = "account_id")] string accountId, [FromRoute(Name = "request_id")] int requestId, [FromQuery(Name = "_expand")] ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (accountId != Session.AccountId)
                return Forbid();

            var account = await _accountRepository.GetAsync(accountId, cancellationToken);
            if (account == null)
                return NotFound("invalid_account_id");

            var request = await _requestRepository.GetRequestAsync(accountId, requestId, cancellationToken);

            if (request == null)
                return NotFound("invalid_request_id");

            return Ok(await request.ToRequestModelAsync(GetExpandOptions(expandOptions), cancellationToken));
        }

        /// <summary>
        /// Add user request.
        /// </summary>
        /// <response code="422">missing_request_type, missing_bank_account, active_daily_payment, duplicated_payment_request, not_enough_credit</response>  
        /// <response code="403">forbidden</response>
        [HttpPost]
        [Route("operation/requests/add")]
        [SwaggerResponse(200, type: typeof(RequestModel))]
        [SwaggerResponse(422)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<RequestModel>> PostRequestAsync([FromBody] InputRequestModel requestModel, [FromQuery(Name = "_expand")] ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (requestModel.Account.Id != Session.AccountId)
                return Forbid();

            if (requestModel.Type == null)
                return UnprocessableEntity(new UnprocessableEntityException("missing_request_type", "Request Type Not Defined!"));

            var dateTime = DateTime.Now;
            var accountId = requestModel.Account.Id;

            if (requestModel.Type.Id == RequestTypeIds.PaymentOfMoney)
            {
                var bankAccount = await _bankAccountRepository.GetBankAccountAsync(accountId, cancellationToken);
                if (bankAccount == null)
                   return UnprocessableEntity(new UnprocessableEntityException("missing_bank_account", "Bank Account Is Not Defined!"));

                if (bankAccount.DailyPayment ?? false)
                   return UnprocessableEntity(new UnprocessableEntityException("active_daily_payment", "Daily Payment Is Activated!"));

                var existingRequest = await _requestRepository.GetRequestAsync(accountId, requestModel.Type.Id, dateTime.Date, cancellationToken);
                if (existingRequest != null)
                   return UnprocessableEntity(new UnprocessableEntityException("duplicated_payment_request", "Pyament Request Is Duplicated!"));

                var balance = await _balanceRepository.GetByAccountIdAsync(accountId, cancellationToken);
                if (balance.CurrentBalance <= 5000M)
                   return UnprocessableEntity(new UnprocessableEntityException("not_enough_credit", "Balance Amount Must Be More Than 5000 Toman!"));
            }

            var request = _requestRepository.Create();
            request.AccountId = accountId;
            request.TypeId = requestModel.Type.Id;
            request.StatusId = requestModel.Status?.Id ?? RequestStatusIds.Pending;
            request.DateTime = dateTime;
            request.Comments = requestModel.Comments;
            request.Parameters = requestModel.Parameters?.Select(p => _requestRepository.CreateParameter(p.Key, p.Value, p.Type));
            request = await _requestRepository.SaveAsync(request, cancellationToken);
            
            return Ok(await request.ToRequestModelAsync(GetExpandOptions(expandOptions), cancellationToken));
        }
    }
}
