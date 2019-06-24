using System;
using IrpsApi.Services.OnlinePayment.Exceptions;

namespace IrpsApi.Services.OnlinePayment.Saman
{
    [Serializable]
    public class SamanException : PaygateException
    {
        public string Code
        {
            get;
        }

        public SamanException(string code) : base($"SamanPaygateException: {code}")
        {
            Code = code;
        }
    }
}