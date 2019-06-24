using System;
using System.Collections.Generic;
using IrpsApi.Framework.OnlinePayment;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.OnlinePayment
{
    [Table("OnlinePayment", "OnlinePayment")]
    public class OnlinePayment : GeneratedQueryRecord, IOnlinePayment
    {
        public Guid? UniqueId
        {
            get;
            set;
        }

        public string AccountId
        {
            get;
            set;
        }

        public decimal Amount
        {
            get;
            set;
        }

        public string StateId
        {
            get;
            set;
        }

        public decimal? PaidAmount
        {
            get;
            set;
        }

        public string GatewayId
        {
            get;
            set;
        }

        public DateTime CreationDateTime
        {
            get;
            set;
        }

        public DateTime? PaymentDateTime
        {
            get;
            set;
        }

        [IgnoreColumn]
        public IEnumerable<IOnlinePaymentParameter> OnlinePaymentParameters
        {
            get;
            set;
        }
    }
}
