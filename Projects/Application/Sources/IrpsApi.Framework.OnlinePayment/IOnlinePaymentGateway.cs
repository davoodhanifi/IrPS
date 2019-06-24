namespace IrpsApi.Framework.OnlinePayment
{
    public interface IOnlinePaymentGateway : IRecord
    {
        string Title
        {
            get;
            set;
        }

        string TitleEn
        {
            get;
            set;
        }
    }
}