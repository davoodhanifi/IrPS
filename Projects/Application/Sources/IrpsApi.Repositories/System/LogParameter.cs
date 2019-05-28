using IrpsApi.Framework.System;

namespace Noandishan.IrpsApi.Repositories.System
{
    internal class LogParameter : ILogParameter
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
