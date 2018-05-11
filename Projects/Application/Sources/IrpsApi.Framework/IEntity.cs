using System;

namespace IrpsApi.Framework
{
    public interface IEntity
    {
        int Id
        {
            get;
            set;
        }

        void EnsureLoaded();
    }
}
