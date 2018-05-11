using System;
using IrpsApi.Framework.User;

namespace Noandishan.IrpsApi.Repositories.User
{
    public class User : IUser
    {
        public int Id
        {
            get;
            set;
        }
       
        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string PhoneNumber
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public long UserCode
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public byte[] PasswordHash
        {
            get;
            set;
        }

        public byte[] PasswordSalt
        {
            get;
            set;
        }

        public bool FingerprintEnabled
        {
            get;
            set;
        }
        public byte[] Image
        {
            get;
            set;
        }

        public string ImageMimeType
        {
            get;
            set;
        }

        public byte[] Barcode
        {
            get;
            set;
        }

        public string BarcodeMimeType
        {
            get;
            set;
        }

        public DateTime RegistrationDateTime
        {
            get;
            set;
        }

        public string RegistrationCode
        {
            get;
            set;
        }

        public bool? IsActive
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