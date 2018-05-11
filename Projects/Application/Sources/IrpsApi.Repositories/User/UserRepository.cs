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

        protected override IUser SetEntity(IDataReader reader)
        {
            return new User
            {
                Id = reader.ReadInt32("Id"),
                FirstName = reader.ReadString("FirstName"),
                LastName = reader.ReadString("FirstName"),
                PhoneNumber = reader.ReadString("PhoneNumber"),
                Username = reader.ReadString("Username"),
                Email = reader.ReadString("Email"),
                UserCode = reader.ReadInt64("UserCode"),
                PasswordHash = reader.ReadByteArray("PasswordHash"),
                PasswordSalt = reader.ReadByteArray("PasswordSalt"),
                FingerprintEnabled = reader.ReadBoolean("FingerprintEnabled"),
                Image = reader.ReadByteArray("Image"),
                ImageMimeType = reader.ReadString("ImageMimeType"),
                Barcode = reader.ReadByteArray("Barcode"),
                BarcodeMimeType = reader.ReadString("BarcodeMimeType"),
                RegistrationDateTime = reader["RegistrationDateTime"] is DateTime ? (DateTime)reader["RegistrationDateTime"] : new DateTime(),
                RegistrationCode = reader.ReadString("RegistrationCode"),
                IsActive = reader.ReadBoolean("IsActive")
            };
        }
    }
}
