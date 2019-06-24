namespace IrpsApi.Framework.OnlinePayment
{
    public interface IOnlinePaymentParameter : IRecord
    {
        string OnlinePaymentId
        {
            get;
            set;
        }

        string Key
        {
            get;
            set;
        }

        object Value
        {
            get;
            set;
        }
    }
}
