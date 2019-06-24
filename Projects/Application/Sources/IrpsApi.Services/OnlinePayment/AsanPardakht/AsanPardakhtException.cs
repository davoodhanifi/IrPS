using IrpsApi.Services.OnlinePayment.Exceptions;

namespace IrpsApi.Services.OnlinePayment.AsanPardakht
{
    public class AsanPardakhtException : PaygateException
    {
        public string Code
        {
            get;
        }

        public AsanPardakhtException(string code) : base($"AsanPardakhtPaygateException: {code}")
        {
            Code = code;
        }
    }
}