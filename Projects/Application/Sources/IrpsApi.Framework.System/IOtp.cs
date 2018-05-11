using System;

namespace IrpsApi.Framework.System
{
    public interface IOtp : IEntity
    {
        string PhoneNumber
        {
            get;
            set;
        }

        string DeviceId
        {
            get;
            set;
        }

        string Password
        {
            get;
            set;
        }

        DateTime CreationDate
        {
            get;
            set;
        }

        DateTime ExpiryDate
        {
            get;
            set;
        }
    }
}
