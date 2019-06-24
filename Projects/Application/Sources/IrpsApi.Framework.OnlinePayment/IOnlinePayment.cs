using System;
using System.Collections.Generic;

namespace IrpsApi.Framework.OnlinePayment
{
    public interface IOnlinePayment : IRecord
    {
        Guid? UniqueId
        {
            get;
            set;
        }

        string AccountId
        {
            get;
            set;
        }

        decimal Amount
        {
            get;
            set;
        }

        string StateId
        {
            get;
            set;
        }

        decimal? PaidAmount
        {
            get;
            set;
        }

        string GatewayId
        {
            get;
            set;
        }

        DateTime CreationDateTime
        {
            get;
            set;
        }

        DateTime? PaymentDateTime
        {
            get;
            set;
        }

        IEnumerable<IOnlinePaymentParameter> OnlinePaymentParameters
        {
            get;
            set;
        }
    }
}
