namespace IrpsApi.Framework.OnlinePayment
{
    public interface IOnlinePaymentState : IRecord
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