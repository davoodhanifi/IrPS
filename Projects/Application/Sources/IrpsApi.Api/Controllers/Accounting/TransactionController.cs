using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IrpsApi.Api.ViewModels.Accounting;
using IrpsApi.Framework.Accounting;
using IrpsApi.Framework.User;
using Microsoft.AspNetCore.Mvc;

namespace IrpsApi.Api.Controllers.Accounting
{
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase<IUser>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBalanceRepository _balanceRepository;

        public TransactionController(ITransactionRepository transactionRepository, IUserRepository userRepository, IBalanceRepository balanceRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _balanceRepository = balanceRepository;
        }

        [HttpGet("getallbyusercode/{userCode}", Name = "GetTranscationsByUserCode")]
        public async Task<IActionResult> GetByUserCode(string userCode, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (string.IsNullOrEmpty(userCode))
            {
                return BadRequest("User Code Not Defined!");
            }

            try
            {
                var user = await _userRepository.GetByUserCodeAsync(userCode, cancellationToken);
                if (user == null)
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "User Not Found!");

                var transactions = await _transactionRepository.GetAllByUserCodeAsync(userCode, cancellationToken);
                var transactionsViewModels = Mapper.Map<IEnumerable<TransactionModel>>(transactions);
                return Ok(transactionsViewModels);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Error In Processing");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TransactionModel transactionModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (transactionModel?.TransactionType == null || transactionModel?.TransactionType == TransactionType.None)
            {
                return BadRequest("Transaction Type Not Defined!");
            }

            if (transactionModel?.TransactionType == TransactionType.Payment && transactionModel?.FromUserCode == null)
            {
                return BadRequest("First User Not Defined!");
            }

            if (transactionModel?.ToUserCode == null)
            {
                return BadRequest("Second User Not Defined!");
            }

            if (transactionModel?.Amount == null || transactionModel?.Amount == 0)
            {
                return BadRequest("Amount Must Be Positive!");
            }
           
            try
            {
                var toUser = await _userRepository.GetByUserCodeAsync(transactionModel.ToUserCode, cancellationToken);
                if (toUser == null)
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Second User Not Found!");

                if (transactionModel.TransactionType == TransactionType.Payment)
                {
                    var fromUser = await _userRepository.GetByUserCodeAsync(transactionModel.FromUserCode, cancellationToken);
                    if (fromUser == null)
                        return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "First User Not Found!");

                    var fromUserBalance = await _balanceRepository.GetByUserCodeAsync(transactionModel.FromUserCode, cancellationToken);
                    if (fromUserBalance == null || fromUserBalance.CurrentBalance < transactionModel.Amount)
                        return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity, "First User Not Enough Credit!");
                }

                var transaction = await _transactionRepository.CreateAsync(transactionModel, cancellationToken);

                if (transaction != null)
                {
                    if (transactionModel.TransactionType == TransactionType.Payment)
                    {
                        var fromUserBalance = await _balanceRepository.GetByUserCodeAsync(transactionModel.FromUserCode, cancellationToken);

                        var fromBalance = new BalanceModel
                        {
                            UserCode = transactionModel.FromUserCode,
                            CurrentBalance = (fromUserBalance?.CurrentBalance ?? 0) - transactionModel.Amount,
                            IsActive = true
                        };

                        await _balanceRepository.CreateAsync(fromBalance, cancellationToken);
                    }

                    var toUserBalance = await _balanceRepository.GetByUserCodeAsync(transactionModel.ToUserCode, cancellationToken);

                    var toBalance = new BalanceModel
                    {
                        UserCode = transactionModel.ToUserCode,
                        CurrentBalance = (toUserBalance?.CurrentBalance ?? 0) + transactionModel.Amount,
                        IsActive = true
                    };

                    await _balanceRepository.CreateAsync(toBalance, cancellationToken);
                }

                var transactionViewModel = Mapper.Map<TransactionModel>(transaction);
                return CreatedAtRoute("GetTranscationsByUserCode", new { userCode = transactionViewModel.ToUserCode }, transactionViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error In Processing");
            }
        }
    }
}