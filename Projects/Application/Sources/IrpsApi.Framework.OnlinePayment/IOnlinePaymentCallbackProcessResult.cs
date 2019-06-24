namespace IrpsApi.Framework.OnlinePayment
{
    public interface IOnlinePaymentCallbackProcessResult : IResultBase
    {
        string RedirectUrl
        {
            get;
            set;
        }

        string Message
        {
            get;
            set;
        }
    }
}