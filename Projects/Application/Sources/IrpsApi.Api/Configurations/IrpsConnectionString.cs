using Microsoft.Extensions.Options;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;

namespace IrpsApi.Api.Configurations
{
    public class IrpsConnectionString : IIrpsConnectionString
    {
        private readonly IOptionsMonitor<ConnectionStringsOption> _options;

        public string ConnectionString => _options.CurrentValue.Irps;

        public IrpsConnectionString(IOptionsMonitor<ConnectionStringsOption> options)
        {
            _options = options;
        }
    }
}