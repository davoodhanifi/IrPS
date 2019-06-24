using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework;
using IrpsApi.Framework.OnlinePayment;
using IrpsApi.Framework.OnlinePayment.Repositories;
using Noandishan.IrpsApi.Repositories.Common;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.OnlinePayment
{
    public class OnlinePaymentRepository : GeneratedQueryEditableRecordRepository<IOnlinePayment, OnlinePayment>, IOnlinePaymentRepository
    {
        private readonly IOnlinePaymentParameterRepository _parameterRepository;

        public OnlinePaymentRepository(IIrpsConnectionString connectionString, IOnlinePaymentParameterRepository parameterRepository) : base(connectionString)
        {
            _parameterRepository = parameterRepository;
        }

        public IOnlinePaymentParameter CreateParameter()
        {
            return _parameterRepository.Create();
        }

        public async Task<IOnlinePayment> GetOnlinePaymentByUniqueIdAsync(Guid uniqueId, CancellationToken cancellationToken)
        {
            var onlinePayment = (await GetAsync(new IFilterParameter[]
            {
                new FilterParameter("UniqueId", uniqueId)
            }, null, null, cancellationToken)).FirstOrDefault();

            if (onlinePayment != null)
            {
                onlinePayment.OnlinePaymentParameters = await _parameterRepository.GetAsync(new IFilterParameter[]
                {
                    new FilterParameter("OnlinePaymentId", onlinePayment.Id),
                }, null, null, cancellationToken);
            }

            return onlinePayment;
        }

        public async Task<IEnumerable<IOnlinePayment>> GetOnlinePaymentsAsync(string accountId, CancellationToken cancellationToken)
        {
            var onlinePayments = await GetAsync(new IFilterParameter[]
            {
                new FilterParameter("AccountId", accountId),
            }, null, null, cancellationToken);

            var onlinePaymentsAsync = onlinePayments.ToList();
            foreach (var onlinePayment in onlinePaymentsAsync)
            {
                onlinePayment.OnlinePaymentParameters = await _parameterRepository.GetAsync(new IFilterParameter[]
                {
                    new FilterParameter("OnlinePaymentId", onlinePayment.Id),
                }, null, null, cancellationToken);
            }

            return onlinePaymentsAsync;
        }

        public async Task<IOnlinePaymentParameter> GetOnlinePaymentParameterAsync(string id, CancellationToken cancellationToken)
        {
            return await _parameterRepository.GetAsync(id, cancellationToken);
        }

        public async Task<IOnlinePaymentParameter> SaveOnlinePaymentParameter(IOnlinePaymentParameter onlinePaymentParameter, CancellationToken cancellationToken)
        {
            return await _parameterRepository.InsertAsync(onlinePaymentParameter, cancellationToken);
        }
    }
}