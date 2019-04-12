using IrpsApi.Framework.Operation;
using Noandishan.IrpsApi.Repositories.Common.Generated;
using Noandishan.IrpsApi.Repositories.Common.QueryGenerator;

namespace Noandishan.IrpsApi.Repositories.Operation
{
    [Table("RequestType", "Operation")]
    public class RequestType : GeneratedQueryRecord, IRequestType
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
