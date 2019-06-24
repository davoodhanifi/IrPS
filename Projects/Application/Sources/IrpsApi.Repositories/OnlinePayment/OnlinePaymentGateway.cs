using IrpsApi.Framework.OnlinePayment;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.OnlinePayment
{
    [Table("OnlinePaymentGateway", "OnlinePayment")]
    public class OnlinePaymentGateway : GeneratedQueryRecord, IOnlinePaymentGateway
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