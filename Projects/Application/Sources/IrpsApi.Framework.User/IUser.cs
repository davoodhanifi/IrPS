using System;

namespace IrpsApi.Framework.User
{
    public interface IUser : IEntity
    {
        string FirstName
        {
            get;
            set;
        }

        string LastName
        {
            get;
            set;
        }

        string PhoneNumber
        {
            get;
            set;
        }

        string Username
        {
            get;
            set;
        }

        string Email
        {
            get;
            set;
        }

        long UserCode
        {
            get;
            set;
        }

        string Password
        {
            get;
            set;
        }

        byte[] PasswordHash
        {
            get;
            set;
        }

        byte[] PasswordSalt
        {
            get;
            set;
        }

        bool FingerprintEnabled
        {
            get;
            set;
        }
        byte[] Image
        {
            get;
            set;
        }

        string ImageMimeType
        {
            get;
            set;
        }

        byte[] Barcode
        {
            get;
            set;
        }

        string BarcodeMimeType
        {
            get;
            set;
        }

        DateTime RegistrationDateTime
        {
            get;
            set;
        }

        string RegistrationCode
        {
            get;
            set;
        }

        bool? IsActive
        {
            get;
            set;
        }
    }
}
