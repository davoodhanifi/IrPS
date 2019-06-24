using System;
using System.Runtime.Serialization;

namespace IrpsApi.Services.OnlinePayment.Exceptions
{
    public class PaygateException : Exception
    {
        public PaygateException()
        {
        }

        public PaygateException(string message) : base(message)
        {
        }

        public PaygateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PaygateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Description
        {
            get;
            set;
        }
    }
}