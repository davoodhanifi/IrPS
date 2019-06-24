using System;
using IrpsApi.Framework.OnlinePayment;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.OnlinePayment
{
    [Table("OnlinePaymentParameter", "OnlinePayment")]
    public class OnlinePaymentParameter : GeneratedQueryRecord, IOnlinePaymentParameter
    {
        public string OnlinePaymentId
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public bool? BooleanValue
        {
            get;
            set;
        }

        public long? IntegerValue
        {
            get;
            set;
        }

        public decimal? DecimalValue
        {
            get;
            set;
        }

        public string TextValue
        {
            get;
            set;
        }

        public DateTime? DateTimeValue
        {
            get;
            set;
        }

        public byte[] BinaryValue
        {
            get;
            set;
        }

        [IgnoreColumn]
        public object Value
        {
            get
            {
                if (BooleanValue.HasValue)
                    return BooleanValue;

                if (IntegerValue.HasValue)
                    return IntegerValue.Value;

                if (DecimalValue.HasValue)
                    return DecimalValue.Value;

                if (TextValue != null)
                    return TextValue;

                if (DateTimeValue != null)
                    return DateTimeValue;

                if (BinaryValue != null)
                    return BinaryValue;

                return null;
            }
            set
            {
                if (value is bool)
                    BooleanValue = (bool)value;
                else if (value is int || value is long)
                    IntegerValue = (long)value;
                else if (value is decimal || value is float || value is double)
                    DecimalValue = (decimal)value;
                else if (value is string)
                    TextValue = (string)value;
                else if (value is DateTime)
                    DateTimeValue = (DateTime)value;
                else if (value is byte[])
                    BinaryValue = (byte[])value;
                else
                    throw new ArgumentException($"value type {value.GetType()} is not supported.");
            }
        }
    }
}