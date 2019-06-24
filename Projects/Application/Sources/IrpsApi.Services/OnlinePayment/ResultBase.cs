namespace IrpsApi.Services.OnlinePayment
{
    public class ResultBase
    {
        public bool IsSuccess
        {
            get;
            set;
        }

        public string ErrorCode
        {
            get;
            set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }
    }
}