using System;

namespace IrpsApi.Framework.Bank
{
    public interface IBank : IRecord
    {
        string Key
        {
            get;
            set;
        }

        string Name
        {
            get;
        }

        string NameEn
        {
            get;
        }
    }
}
