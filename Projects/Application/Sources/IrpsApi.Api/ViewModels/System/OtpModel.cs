using System;
using System.Runtime.Serialization;
using IrpsApi.Framework.System;

namespace IrpsApi.Api.ViewModels.System
{
    [DataContract(Name = "otp")]
    public class OtpModel : IOtp
    {
        [DataMember(Name = "id")]
        public int Id
        {
            get;
            set;
        }

        [DataMember(Name = "phone_number")]
        public string PhoneNumber
        {
            get;
            set;
        }

        [DataMember(Name = "device_id")]
        public string DeviceId
        {
            get;
            set;
        }

        [DataMember(Name = "password")]
        public string Password
        {
            get;
            set;
        }

        [DataMember(Name = "creation_date")]
        public DateTime CreationDate
        {
            get;
            set;
        }

        [DataMember(Name = "expiry_date")]
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
