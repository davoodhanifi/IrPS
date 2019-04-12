using IrpsApi.Framework.Operation;
using Noandishan.IrpsApi.Repositories.Parameter;

namespace Noandishan.IrpsApi.Repositories.Operation
{
    public class RequestParameter : IRequestParameter, IParameter
    {
        public string Key
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }
    }
}
