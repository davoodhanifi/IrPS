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
                    return ReadOtp(reader);

            return null;
        }

        public async Task<IOtp> CreateNewOtpAsync(string phoneNumber, string deviceId, CancellationToken cancellationToken)
        {
            var password = 12345;//Guid.NewGuid().ToString("N");
            var creationTime = DateTime.Now;
            var expiryTime = creationTime.AddMinutes(10);

            var command = GetCommand();
            command.CommandText = "INSERT INTO [System].[Otp]([PhoneNumber], [DeviceId], [Password], [CreationDate], [ExpiryDate]) OUTPUT Inserted.Id VALUES(@phoneNumber, @deviceId, @password, @creationTime, @expiryTime)";

            command.AddParameter("@phoneNumber", phoneNumber);
            command.AddParameter("@deviceId", deviceId);
            command.AddParameter("@password", password);
            command.AddParameter("@creationTime", creationTime);
            command.AddParameter("@expiryTime", expiryTime);

            var id = await command.ExecuteScalarAsync<int>(cancellationToken);

            return new Otp
            {
                Id = id,
                PhoneNumber = phoneNumber,
                DeviceId = deviceId,
                Password = password,
                CreationDate = creationTime,
                ExpiryDate = expiryTime
            };
        }

        private IOtp ReadOtp(IDataRecord record)
        {
            return new Otp
            {
                Id = (int)record["Id"],
                PhoneNumber = record["PhoneNumber"] as string,
                DeviceId = record["DeviceId"] as string,
                Password = (int)record["Password"]
            };
        }
    }
}
