using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using IrpsApi.Framework.Accounting;
using IrpsApi.Framework.Accounting.Repositories;
using IrpsApi.Framework.OnlinePayment;
using IrpsApi.Framework.OnlinePayment.Repositories;
using IrpsApi.Services.OnlinePayment.AsanPardakht;
using IrpsApi.Services.OnlinePayment.Exceptions;
using IrpsApi.Services.OnlinePayment.Saman;
using Mabna.Diagnostics;
using Microsoft.Extensions.Options;

namespace IrpsApi.Services.OnlinePayment
{
    public class OnlinePaymentService : IOnlinePaymentService
    {
        private readonly IOnlinePaymentRepository _onlinePaymentRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IOnlinePaymentGatewayRepository _gatewayRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IOptionsMonitor<SamanGatewaySettings> _samanGateWaySettings;
        private string _baseCallbackUrl;

        public OnlinePaymentService(IOnlinePaymentRepository onlinePaymentRepository, ITransactionRepository transactionRepository, IOnlinePaymentGatewayRepository gatewayRepository, IOptionsMonitor<SamanGatewaySettings> samanGateWaySettings, IBalanceRepository balanceRepository)
        {
            _onlinePaymentRepository = onlinePaymentRepository;
            _transactionRepository = transactionRepository;
            _gatewayRepository = gatewayRepository;
            _balanceRepository = balanceRepository;
            _samanGateWaySettings = samanGateWaySettings;
            _baseCallbackUrl = samanGateWaySettings.CurrentValue.BaseUrl + "/onlinepayment/onlinepayments";
        }

        private IOnlinePaymentClient CreateClient(string gatewayId)
        {
            switch (gatewayId)
            {
                case OnlinePaymentGatewayIds.Saman:
                    return new SamanOnlinePaymentClient(_samanGateWaySettings);
                case OnlinePaymentGatewayIds.AsanPardakht:
                    return new AsanPardakhtPaymentClient();
                default:
                    throw new NotSupportedException($"GatewayId:{gatewayId} is not supported");
            }
        }

        public async Task<IOnlinePaymentInitResult> InitOnlinePayment(string accountId, string gatewayId, decimal amount, string callbackUrl, string[] additionalData, CancellationToken cancellationToken)
        {
            var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var onlinePayment = _onlinePaymentRepository.Create();
                onlinePayment.AccountId = accountId;
                onlinePayment.Amount = amount;
                onlinePayment.StateId = OnlinePaymentStateIds.Created;
                onlinePayment.GatewayId = gatewayId;
                onlinePayment.PaidAmount = null;
                onlinePayment.CreationDateTime = DateTime.Now;
                onlinePayment.UniqueId = Guid.NewGuid();

                onlinePayment = await _onlinePaymentRepository.SaveAsync(onlinePayment, cancellationToken);

                var client = CreateClient(gatewayId);
                var redirectUrl = $"{_baseCallbackUrl}/{onlinePayment.UniqueId:N}/result";
                var parameters = await client.InitPaymentAsync((long)amount, onlinePayment.Id, additionalData, redirectUrl, cancellationToken);

                foreach (var parameter in parameters)
                {
                    var p = _onlinePaymentRepository.CreateParameter();
                    p.OnlinePaymentId = onlinePayment.Id;
                    p.Key = parameter.Key;
                    p.Value = parameter.Value;
                    p = await _onlinePaymentRepository.SaveOnlinePaymentParameter(p, cancellationToken);
                }

                var callbackUrlParam = _onlinePaymentRepository.CreateParameter();
                callbackUrlParam.OnlinePaymentId = onlinePayment.Id;
                callbackUrlParam.Key = "callback_url";
                callbackUrlParam.Value = callbackUrl;
                callbackUrlParam = await _onlinePaymentRepository.SaveOnlinePaymentParameter(callbackUrlParam, cancellationToken);

                transactionScope.Complete();

                return new OnlinePaymentInitResult
                {
                    IsSuccess = true,
                    OnlinePayment = onlinePayment,
                    GatewayUrl = client.GatewayUrl,
                    PaymentUrl = $"{_samanGateWaySettings.CurrentValue.BaseUrl}/finance/payment/{onlinePayment.UniqueId}",
                    Parameters = parameters
                };
            }
            catch (PaygateException exception)
            {
                DefaultTraceSource.TraceWarning($"OnlinePaymentService: paygate_exception, {exception}");

                return new OnlinePaymentInitResult
                {
                    IsSuccess = false,
                    ErrorCode = "paygate_exception",
                    ErrorMessage = exception.Description
                };
            }
            finally
            {
                transactionScope.Dispose();
            }
        }
        
        public async Task<IOnlinePaymentCallbackProcessResult> ProcessCallbackAsync(IOnlinePayment onlinePayment, IDictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            var client = CreateClient(onlinePayment.GatewayId);
            var callbackResult = client.ProcessCallbackParameters(parameters);

            if (onlinePayment.StateId != OnlinePaymentStateIds.Created)
                return OnlinePaymentCallbackProcessResult.Failed(onlinePayment, "invalid_online_payment_state");

            foreach (var callbackAdditionalParameter in callbackResult.AdditionalParameters)
            {
                if (!string.IsNullOrWhiteSpace(callbackAdditionalParameter.Value) && callbackAdditionalParameter.Key != "uid")
                {
                    var parameter = _onlinePaymentRepository.CreateParameter();
                    parameter.OnlinePaymentId = onlinePayment.Id;
                    parameter.Key = callbackAdditionalParameter.Key;
                    parameter.Value = callbackAdditionalParameter.Value;
                    parameter = await _onlinePaymentRepository.SaveOnlinePaymentParameter(parameter, cancellationToken);
                }
            }

            if (!callbackResult.IsSuccess)
            {
                onlinePayment.StateId = OnlinePaymentStateIds.Failed;
                onlinePayment = await _onlinePaymentRepository.SaveAsync(onlinePayment, cancellationToken);
                return OnlinePaymentCallbackProcessResult.Failed(onlinePayment, callbackResult.ErrorCode, callbackResult.ErrorMessage);
            }

            VerifyResult verifyResult;
            try
            {
                verifyResult = await client.VerifyTransaction(onlinePayment, callbackResult.TransactionId, cancellationToken);
            }
            catch (Exception exception)
            {
                DefaultTraceSource.TraceWarning($"OnlinePaymentService: verify_failed, {exception}");
                onlinePayment.StateId = OnlinePaymentStateIds.Failed;
                onlinePayment = await _onlinePaymentRepository.SaveAsync(onlinePayment, cancellationToken);
                return OnlinePaymentCallbackProcessResult.Failed(onlinePayment, "verify_failed", "internal_error");
            }

            if (verifyResult.IsSuccess)
            {
                var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                try
                {
                    onlinePayment.StateId = OnlinePaymentStateIds.Paid;
                    onlinePayment.PaidAmount = verifyResult.PaidAmount;
                    onlinePayment.PaymentDateTime = DateTime.Now;
                    onlinePayment = await _onlinePaymentRepository.SaveAsync(onlinePayment, cancellationToken);

                    var gateway = await _gatewayRepository.GetAsync(onlinePayment.GatewayId, cancellationToken);

                    if (!parameters.TryGetValue("TRACENO", out var traceNo))
                        parameters.TryGetValue("PayGateTranID", out traceNo);

                    // افزایش اعتبار کاربر و تغییر بالانس کیف پول
                    var dateTimeNow = DateTime.Now;
                    var transaction = _transactionRepository.Create();
                    transaction.FromAccountId = onlinePayment.AccountId;
                    transaction.ToAccountId = onlinePayment.AccountId;
                    transaction.Amount = onlinePayment.PaidAmount != null ? (onlinePayment.PaidAmount.Value / 10) : 0M;
                    transaction.DateTime = dateTimeNow;
                    transaction.Description = $"شارژ کیف پول آنلاین درگاه {gateway.Title} با شماره پیگیری {traceNo}";
                    transaction.TypeId = TransactionTypeIds.IncreaseCredit;
                    transaction.OnlinePaymentId = onlinePayment.Id;

                    var oldUserBalance = await _balanceRepository.GetByAccountIdAsync(onlinePayment.AccountId, cancellationToken);
                    var newUserBalance = _balanceRepository.Create();

                    // Disable user balance
                    if (oldUserBalance != null)
                    {
                        oldUserBalance.IsActive = false;
                        await _balanceRepository.UpdateAsync(oldUserBalance, cancellationToken);
                    }

                    // Save Transaction
                    await _transactionRepository.SaveAsync(transaction, cancellationToken);

                    // Update user balance
                    newUserBalance.AccountId = onlinePayment.AccountId;
                    newUserBalance.DateTime = dateTimeNow;
                    newUserBalance.CurrentBalance = (oldUserBalance?.CurrentBalance ?? 0) + transaction.Amount;
                    newUserBalance.IsActive = true;
                    newUserBalance.Description = "افزایش اعتبار";
                    await _balanceRepository.SaveAsync(newUserBalance, cancellationToken);
                    
                    transactionScope.Complete();

                    return OnlinePaymentCallbackProcessResult.Success(onlinePayment, callbackResult.MessageText);

                }
                catch
                {
                    onlinePayment.StateId = OnlinePaymentStateIds.Failed;
                    onlinePayment = await _onlinePaymentRepository.SaveAsync(onlinePayment, cancellationToken);
                    return OnlinePaymentCallbackProcessResult.Failed(onlinePayment, "verify_failed", verifyResult.ErrorMessage);
                }
                finally
                {
                    transactionScope.Dispose();
                }
            }

            onlinePayment.StateId = OnlinePaymentStateIds.Failed;
            onlinePayment = await _onlinePaymentRepository.SaveAsync(onlinePayment, cancellationToken);
            return OnlinePaymentCallbackProcessResult.Failed(onlinePayment, "verify_failed", verifyResult.ErrorMessage);
        }
    }
}