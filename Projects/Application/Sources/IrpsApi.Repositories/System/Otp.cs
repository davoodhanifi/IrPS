using System;
using IrpsApi.Framework.System;

namespace Noandishan.IrpsApi.Repositories.System
{
    public class Otp : IOtp
    {
        public int Id
        {
            get;
            set;
        }

        public string PhoneNumber
        {
            get;
            set;
        }

        public string DeviceId
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public DateTime CreationDate
        {
            get;
            set;
        }

        public DateTime ExpiryDate
        {
            get;
            set;
        }
        
        public void EnsureLoaded()
        {
            throw new NotImplementedException();
        }
    }
}
