using System;
using System.Runtime.Serialization;
using IrpsApi.Framework.User;

namespace IrpsApi.Api.ViewModels.User
{
    [DataContract(Name = "user")]
    public class UserModel : IUser
    {
        [DataMember(Name = "id")]
        public int Id
        {
            get;
            set;
        }

        [DataMember(Name = "first_name")]
        public string FirstName
        {
            get;
            set;
        }

        [DataMember(Name = "last_name")]
        public string LastName
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

        [DataMember(Name = "username")]
        public string Username
        {
            get;
            set;
        }

        [DataMember(Name = "email")]
        public string Email
        {
            get;
            set;
        }

        [DataMember(Name = "user_code")]
        public string UserCode
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

        [DataMember(Name = "password_hash")]
        public byte[] PasswordHash
        {
            get;
            set;
        }

        [DataMember(Name = "password_salt")]
        public byte[] PasswordSalt
        {
            get;
            set;
        }

        [DataMember(Name = "fingerprint_enabled")]
        public bool? FingerprintEnabled
        {
            get;
            set;
        }

        [DataMember(Name = "image")]
        public byte[] Image
        {
            get;
            set;
        }

        [DataMember(Name = "image_mime_type")]
        public string ImageMimeType
        {
            get;
            set;
        }

        [DataMember(Name = "barcode")]
        public byte[] Barcode
        {
            get;
            set;
        }

        [DataMember(Name = "barcode_mime_type")]
        public string BarcodeMimeType
        {
            get;
            set;
        }

        [DataMember(Name = "registration_date_time")]
        public DateTime RegistrationDateTime
        {
            get;
            set;
        }

        [DataMember(Name = "is_active")]
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
