using IrpsApi.Framework.OnlinePayment;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.OnlinePayment
{
    [Table("OnlinePaymentState", "OnlinePayment")]
    public class OnlinePaymentState : GeneratedQueryRecord, IOnlinePaymentState
    {
        public string Title
        {
            get;
            set;
        }

        public string TitleEn
        {
            get;
            set;
        }
    }
}