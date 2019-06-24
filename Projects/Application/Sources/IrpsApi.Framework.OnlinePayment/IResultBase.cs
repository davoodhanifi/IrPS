namespace IrpsApi.Framework.OnlinePayment
{
    public interface IResultBase
    {
        bool IsSuccess
        {
            get;
            set;
        }

        string ErrorCode
        {
            get;
            set;
        }

        string ErrorMessage
        {
            get;
            set;
        }
    }
}