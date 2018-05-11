using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework.System;
using Microsoft.Extensions.Configuration;

namespace Noandishan.IrpsApi.Repositories.System
{
    public class OtpRepository : EntityRepositoryBase<IOtp>, IOtpRepository
    {
        public OtpRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IOtp> GetAsync(int id, CancellationToken cancellationToken)
        {
            var command = GetCommand();
            command.CommandText = "SELECT * From [System].[Otp] WHERE [Id] = @id";

            command.AddParameter("@id", id);

            using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                if (await reader.ReadAsync(cancellationToken))
                    return SetEntity(reader);

            return null;
        }

        public async Task<IOtp> CreateAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            var password = new Random().Next(0, 999999).ToString("D6");
            var creationTime = DateTime.Now;
            var expiryDate = creationTime.AddMinutes(2);

            var command = GetCommand();
            command.CommandText = "INSERT INTO [System].[Otp]([PhoneNumber], [Password], [CreationDate], [ExpiryDate]) OUTPUT Inserted.Id VALUES(@phoneNumber, @password, @creationTime, @expiryDate)";

            command.AddParameter("@phoneNumber", phoneNumber);
            command.AddParameter("@password", password);
            command.AddParameter("@creationTime", creationTime);
            command.AddParameter("@expiryDate", expiryDate);

            var id = await command.ExecuteScalarAsync<int>(cancellationToken);

            return new Otp
            {
                Id = id,
                PhoneNumber = phoneNumber,
                Password = password,
                CreationDate = creationTime,
                ExpiryDate = expiryDate
            };
        }

        public async Task<IOtp> CheckAsync(string phoneNumber, string password, CancellationToken cancellationToken)
        {
            var dateTime = DateTime.Now;

            var command = GetCommand();
            command.CommandText = "SELECT * From [System].[Otp] WHERE [PhoneNumber] = @phoneNumber AND [Password] = @password AND [ExpiryDate] >= @expiryDate";

            command.AddParameter("@phoneNumber", phoneNumber);
            command.AddParameter("@password", password);
            command.AddParameter("@expiryDate", dateTime);

            using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                if (await reader.ReadAsync(cancellationToken))
                    return SetEntity(reader);

            return null;
        }

        public async void DeleteAsync(CancellationToken cancellationToken)
        {
            var dateTime = DateTime.Now.AddMinutes(-2);

            var command = GetCommand();
            command.CommandText = "DELETE From [System].[Otp] WHERE [ExpiryDate] < @expiryDate";

            command.AddParameter("@expiryDate", dateTime);

            await command.ExecuteNonQueryAsync(cancellationToken);
        }

        protected override IOtp SetEntity(IDataReader reader)
        {
            return new Otp
            {
                Id = reader.ReadInt32("Id"),
                PhoneNumber = reader.ReadString("PhoneNumber"),
                DeviceId = reader.ReadString("DeviceId"),
                Password = reader.ReadString("Password"),
                CreationDate = (DateTime)reader["CreationDate"],
                ExpiryDate = (DateTime)reader["ExpiryDate"]
            };
        }
    }
}
