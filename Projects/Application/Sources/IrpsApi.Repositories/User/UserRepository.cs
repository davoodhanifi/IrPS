using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework.User;
using Microsoft.Extensions.Configuration;

namespace Noandishan.IrpsApi.Repositories.User
{
    public class UserRepository : EntityRepositoryBase<IUser>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IUser> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            var command = GetCommand();
            command.CommandText = "SELECT * From [User].[User] WHERE [PhoneNumber] = @phoneNumber AND [IsActive] = 1";

            command.AddParameter("@phoneNumber", phoneNumber);

            using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                if (await reader.ReadAsync(cancellationToken))
                    return SetEntity(reader);

            return null;
        }

        public async Task<IUser> GetByUserCodeAsync(string userCode, CancellationToken cancellationToken)
        {
            var command = GetCommand();
            command.CommandText = "SELECT * From [User].[User] WHERE [UserCode] = @userCode AND [IsActive] = 1";

            command.AddParameter("@userCode", userCode);

            using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                if (await reader.ReadAsync(cancellationToken))
                    return SetEntity(reader);

            return null;
        }

        public Task<IUser> GetAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IUser> CreateAsync(IUser user, CancellationToken cancellationToken)
        {
            user.UserCode = new Random().Next(0, 99999999).ToString("D8");
            user.RegistrationDateTime = DateTime.Now;

            var command = GetCommand();
            command.CommandText = "INSERT INTO [User].[User]([FirstName], [LastName], [PhoneNumber], [UserCode], [RegistrationDateTime], [FingerprintEnabled], [IsActive]) OUTPUT Inserted.Id VALUES(@firstName, @lastName, @phoneNumber, @userCode, @registrationDateTime, @fingerprintEnabled, @isActive)";

            command.AddParameter("@firstName", user.FirstName);
            command.AddParameter("@lastName", user.LastName);
            command.AddParameter("@phoneNumber", user.PhoneNumber);
            command.AddParameter("@userCode", user.UserCode);
            command.AddParameter("@registrationDateTime", user.RegistrationDateTime);
            command.AddParameter("@isActive", user.IsActive ?? true);
            command.AddParameter("@fingerprintEnabled", user.FingerprintEnabled ?? false);

            user.Id = await command.ExecuteScalarAsync<int>(cancellationToken);

            return user;
        }

        protected override IUser SetEntity(IDataReader reader)
        {
            return new User
            {
                Id = reader.ReadInt32("Id"),
                FirstName = reader.ReadString("FirstName"),
                LastName = reader.ReadString("LastName"),
                PhoneNumber = reader.ReadString("PhoneNumber"),
                Username = reader.ReadString("Username"),
                Email = reader.ReadString("Email"),
                UserCode = reader.ReadString("UserCode"),
                PasswordHash = reader.ReadByteArray("PasswordHash"),
                PasswordSalt = reader.ReadByteArray("PasswordSalt"),
                FingerprintEnabled = reader.ReadBoolean("FingerprintEnabled"),
                Image = reader.ReadByteArray("Image"),
                ImageMimeType = reader.ReadString("ImageMimeType"),
                Barcode = reader.ReadByteArray("Barcode"),
                BarcodeMimeType = reader.ReadString("BarcodeMimeType"),
                RegistrationDateTime = reader["RegistrationDateTime"] is DateTime ? (DateTime)reader["RegistrationDateTime"] : new DateTime(),
                IsActive = reader.ReadBoolean("IsActive")
            };
        }
    }
}
