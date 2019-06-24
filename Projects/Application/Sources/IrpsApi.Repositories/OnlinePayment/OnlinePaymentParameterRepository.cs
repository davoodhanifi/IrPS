using IrpsApi.Framework.OnlinePayment;
using IrpsApi.Framework.OnlinePayment.Repositories;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace Noandishan.IrpsApi.Repositories.OnlinePayment
{
    public class OnlinePaymentParameterRepository : GeneratedQueryEditableRecordRepository<IOnlinePaymentParameter, OnlinePaymentParameter>, IOnlinePaymentParameterRepository
    {
        public OnlinePaymentParameterRepository(IIrpsConnectionString connectionString) : base(connectionString)
        {
        }
    }
}