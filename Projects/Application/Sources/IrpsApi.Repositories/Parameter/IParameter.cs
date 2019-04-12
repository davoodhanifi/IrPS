using System;

namespace Noandishan.IrpsApi.Repositories.Parameter
{
    public interface IParameter
    {
        string Key
        {
            get;
            set;
        }

        object Value
        {
            get;
            set;
        }
    }
}
