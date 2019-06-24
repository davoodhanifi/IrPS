namespace IrpsApi.Services.OnlinePayment
{
    public class VerifyResult : ResultBase
    {
        public decimal PaidAmount
        {
            get;
            set;
        }
    }
}