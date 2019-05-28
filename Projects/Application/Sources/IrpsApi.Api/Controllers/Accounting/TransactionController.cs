using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Api.ExpandOptionsHelpers;
using IrpsApi.Api.Helpers;
using IrpsApi.Api.Models.Accounting;
using IrpsApi.Api.Models.Fcm;
using IrpsApi.Api.Services;
using IrpsApi.Framework.Accounting;
using IrpsApi.Framework.Accounting.Repositories;
using IrpsApi.Framework.Accounts.Repositories;
using IrpsApi.Framework.System;
using IrpsApi.Framework.System.Repositories;
using Mabna.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace IrpsApi.Api.Controllers.Accounting
{
    public class TransactionController : RequiresAuthenticationApiControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogRepository _logRepository;
        private readonly IFcmService _fcmService;

        public TransactionController(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IBalanceRepository balanceRepository, ITransactionTypeRepository transactionTypeRepository, ILogRepository logRepository, IFcmService fcmService)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _balanceRepository = balanceRepository;
            _logRepository = logRepository;
            _fcmService = fcmService;

            ExpandEngines.Add("from_account", _accountRepository.GetAsync);
            ExpandEngines.Add("to_account", _accountRepository.GetAsync);
            ExpandEngines.Add("type", transactionTypeRepository.GetAsync);
        }

        /// <summary>
        /// Get user transactions.
        /// </summary>
        /// <response code="404">invalid_account_id</response>  
        /// <response code="403">forbidden</response>
        [HttpGet]
        [Route("accounting/{account_id}/transactions")]
        [SwaggerResponse(200, type : typeof(IEnumerable<TransactionModel>))]
        [SwaggerResponse(404)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> GetAllTransactionsAsync([FromRoute(Name = "account_id")] string accountId, [FromQuery(Name = "_expand")] ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (accountId != Session.AccountId)
                return Forbid();

            var account = await _accountRepository.GetAsync(accountId, cancellationToken);
            if (account == null)
                return NotFound("invalid_account_id");

            var transactions = await _transactionRepository.GetAllTransactionsAsync(accountId, cancellationToken);
            var result = new List<TransactionModel>();
            foreach (var transaction in transactions)
                result.Add(await transaction.ToTransactionModelAsync(GetExpandOptions(expandOptions), cancellationToken));

            return Ok(result);
        }

        /// <summary>
        /// Get user transaction.
        /// </summary>
        /// <response code="404">invalid_account_id, invalid_transaction_id</response>  
        /// <response code="403">forbidden</response>
        [HttpGet]
        [Route("accounting/{account_id}/transactions/{transaction_id}")]
        [SwaggerResponse(200, type: typeof(TransactionModel))]
        [SwaggerResponse(404)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<TransactionModel>> GetTransactionsAsync([FromRoute(Name = "account_id")] string accountId, [FromRoute(Name = "transaction_id")] int transactionId, [FromQuery(Name = "_expand")] ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (accountId != Session.AccountId)
                return Forbid();

            var account = await _accountRepository.GetAsync(accountId, cancellationToken);
            if (account == null)
                return NotFound("invalid_account_id");

            var transaction = await _transactionRepository.GetTransactionAsync(accountId,transactionId, cancellationToken);

            if (transaction == null)
                return NotFound("invalid_transaction_id");

            return Ok(await transaction.ToTransactionModelAsync(GetExpandOptions(expandOptions), cancellationToken));
        }

        /// <summary>
        /// Add new transaction.
        /// </summary>
        /// <response code="422">missing_transaction_type, invalid_method, invalid_amount, not_enough_credit, rollback_transaction</response>  
        /// <response code="403">forbidden</response>
        [HttpPost]
        [Route("accounting/transactions/add")]
        [SwaggerResponse(200, type: typeof(TransactionModel))]
        [SwaggerResponse(422)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<TransactionModel>> AddTransactionAsync([FromBody] InputTransactionModel transactionModel, [FromQuery(Name = "_expand")] ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //if (transactionModel.FromAccount.Id != Session.AccountId)
            //    return Forbid();

            if (transactionModel?.Type == null || transactionModel?.Type.Id == TransactionTypeIds.None)
                return UnprocessableEntity(new UnprocessableEntityException("missing_transaction_type", "Transaction Type Not Defined!"));

            if (transactionModel.Type.Id == TransactionTypeIds.IncreaseCredit)
                return UnprocessableEntity(new UnprocessableEntityException("invalid_method", "Invalid Method For Increase Credit!"));

            if (transactionModel?.Amount <= 0M)
                return UnprocessableEntity(new UnprocessableEntityException("invalid_amount", "Amount Must Be Positive!"));

            var oldFromUserBalance = await _balanceRepository.GetByAccountIdAsync(transactionModel.FromAccount.Id, cancellationToken);
            if (oldFromUserBalance == null || oldFromUserBalance.CurrentBalance < transactionModel.Amount)
                return UnprocessableEntity(new UnprocessableEntityException("not_enough_credit", "Amount Must Be Positive!"));

            var dateTimeNow = DateTime.Now;

            var transaction = _transactionRepository.Create();
            transaction.FromAccountId = transactionModel.FromAccount.Id;
            transaction.ToAccountId = transactionModel.ToAccount.Id;
            transaction.Amount = transactionModel.Amount;
            transaction.DateTime = dateTimeNow;
            transaction.Description = transactionModel.Description;
            transaction.TypeId = transactionModel.Type.Id;
            transaction.OnlinePaymentId = transactionModel.OnlinePaymentId;

            var newFromUserBalance = _balanceRepository.Create();
            // Get 2th user balance
            var oldToUserBalance = await _balanceRepository.GetByAccountIdAsync(transactionModel.ToAccount.Id, cancellationToken);
            var newtoUserBalance = _balanceRepository.Create();

            try
            {
                // Save Transaction
                await _transactionRepository.SaveAsync(transaction, cancellationToken);

                // Update 1th user balance
                newFromUserBalance.AccountId = transactionModel.FromAccount.Id;
                newFromUserBalance.DateTime = dateTimeNow;
                newFromUserBalance.CurrentBalance = oldFromUserBalance.CurrentBalance - transaction.Amount;
                newFromUserBalance.IsActive = true;
                newFromUserBalance.Description = $"{transactionModel.Type.Title} به کاربر با شناسه : {transactionModel.ToAccount.Id}";
                await _balanceRepository.SaveAsync(newFromUserBalance, cancellationToken);

                // Disable 1th user balance
                oldFromUserBalance.IsActive = false;
                await _balanceRepository.UpdateAsync(oldFromUserBalance, cancellationToken);

                // Update 2th user balance
                newtoUserBalance.AccountId = transactionModel.ToAccount.Id;
                newtoUserBalance.DateTime = dateTimeNow;
                newtoUserBalance.CurrentBalance = (oldToUserBalance?.CurrentBalance ?? 0) + transaction.Amount;
                newtoUserBalance.IsActive = true;
                newtoUserBalance.Description = $"{transactionModel.Type.Title} از کاربر با شناسه : {transactionModel.FromAccount.Id}";
                await _balanceRepository.SaveAsync(newtoUserBalance, cancellationToken);

                // Disable 2th user balance if not null
                if (oldToUserBalance != null)
                {
                    oldToUserBalance.IsActive = false;
                    await _balanceRepository.UpdateAsync(oldToUserBalance, cancellationToken);
                }

                _fcmService.Send(transactionModel.ToAccount, new AndroidPushMessageModel
                {
                    Notification = new NotificationModel
                    {
                        Title = "پولینو",
                        Body = $"مبلغ {transactionModel.Amount.ToString("N0").ToPersianNumber()} تومان، به کیف پول شما واریز شد."
                    },
                    //Data = JsonConvert.SerializeObject(transactionModel)
                });

                _logRepository.InsertLog("Irps.API", LogLevelIds.Information, null, "Accounting.Transactions.AddTransaction", "Description", "عملیات پرداخت.", "IP", RemoteIpAddress);

                return Ok(await transaction.ToTransactionModelAsync(GetExpandOptions(expandOptions), cancellationToken));
            }
            catch
            {
                // Rollback transaction
                await _transactionRepository.DeleteAsync(transaction, cancellationToken);

                // Rollback from user balances
                await _balanceRepository.DeleteAsync(newFromUserBalance, cancellationToken);
                oldFromUserBalance.IsActive = true;
                await _balanceRepository.UpdateAsync(oldFromUserBalance, cancellationToken);

                // Rollback to user balances
                await _balanceRepository.DeleteAsync(newtoUserBalance, cancellationToken);
                if (oldToUserBalance != null)
                {
                    oldToUserBalance.IsActive = true;
                    await _balanceRepository.UpdateAsync(oldToUserBalance, cancellationToken);
                }

                return UnprocessableEntity(new UnprocessableEntityException("rollback_transaction", "Error in transaction!"));
            }
        }

        /// <summary>
        /// Increase user credit.
        /// </summary>
        /// <response code="422">missing_transaction_type, invalid_method, mismatch_accounts, invalid_amount, rollback_transaction</response>  
        /// <response code="403">forbidden</response>
        [HttpPost]
        [Route("accounting/transactions/credit/increase")]
        [SwaggerResponse(200, type: typeof(TransactionModel))]
        [SwaggerResponse(422)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<TransactionModel>> IncreaseCreditAsync([FromBody] InputTransactionModel transactionModel, [FromQuery(Name = "_expand")] ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (transactionModel.FromAccount.Id != Session.AccountId)
                return Forbid();

            if (transactionModel?.Type == null || transactionModel?.Type.Id == TransactionTypeIds.None)
                return UnprocessableEntity(new UnprocessableEntityException("missing_transaction_type", "Transaction Type Not Defined!"));

            if (transactionModel.Type.Id != TransactionTypeIds.IncreaseCredit)
                return UnprocessableEntity(new UnprocessableEntityException("invalid_method", "Invalid Method!"));

            if (transactionModel.FromAccount.Id != transactionModel.ToAccount.Id)
                return UnprocessableEntity(new UnprocessableEntityException("mismatch_accounts", "Mismatch In From Account With To Account!"));

            if (transactionModel?.Amount <= 0M)
                return UnprocessableEntity(new UnprocessableEntityException("invalid_amount", "Amount Must Be Positive!"));

            var oldUserBalance = await _balanceRepository.GetByAccountIdAsync(transactionModel.FromAccount.Id, cancellationToken);

            var dateTimeNow = DateTime.Now;

            var transaction = _transactionRepository.Create();
            transaction.FromAccountId = transactionModel.FromAccount.Id;
            transaction.ToAccountId = transactionModel.ToAccount.Id;
            transaction.Amount = transactionModel.Amount;
            transaction.DateTime = dateTimeNow;
            transaction.Description = transactionModel.Description;
            transaction.TypeId = transactionModel.Type.Id;
            transaction.OnlinePaymentId = transactionModel.OnlinePaymentId;

            var newUserBalance = _balanceRepository.Create();
           
            try
            {
                // Disable user balance
                if (oldUserBalance != null)
                {
                    oldUserBalance.IsActive = false;
                    await _balanceRepository.UpdateAsync(oldUserBalance, cancellationToken);
                }

                // Save Transaction
                await _transactionRepository.SaveAsync(transaction, cancellationToken);

                // Update user balance
                newUserBalance.AccountId = transactionModel.FromAccount.Id;
                newUserBalance.DateTime = dateTimeNow;
                newUserBalance.CurrentBalance = (oldUserBalance?.CurrentBalance ?? 0) + transaction.Amount;
                newUserBalance.IsActive = true;
                newUserBalance.Description = "افزایش اعتبار";
                await _balanceRepository.SaveAsync(newUserBalance, cancellationToken);

                return Ok(await transaction.ToTransactionModelAsync(GetExpandOptions(expandOptions), cancellationToken));
            }
            catch
            {
                // Rollback transaction
                await _transactionRepository.DeleteAsync(transaction, cancellationToken);

                // Rollback user balances
                await _balanceRepository.DeleteAsync(newUserBalance, cancellationToken);
                if (oldUserBalance != null)
                {
                    oldUserBalance.IsActive = true;
                    await _balanceRepository.UpdateAsync(oldUserBalance, cancellationToken);
                }

                return UnprocessableEntity(new UnprocessableEntityException("rollback_transaction", "Error in transaction!"));
            }
        }
    }
}