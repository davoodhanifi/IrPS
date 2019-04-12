using IrpsApi.Framework.Operation;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Operation
{
    [Table("RequestStatus", "Operation")]
    public class RequestStatus : GeneratedQueryRecord, IRequestStatus
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
