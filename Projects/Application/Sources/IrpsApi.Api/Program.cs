using Mabna.Diagnostics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace IrpsApi.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DefaultTraceSource.Instance = new TraceSource("IrPS v1")
            {
                Switch = new SourceSwitch("All")
                {
                    Level = SourceLevels.All
                }
            };

            DefaultTraceSource.Instance.Listeners.Add(new ColoredConsoleTraceListener
            {
                Filter = new EventTypeFilter(SourceLevels.All),
                TraceOutputOptions = TraceOptions.DateTime | TraceOptions.ProcessId | TraceOptions.ThreadId
            });

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("ConnectionStrings.config", optional: false, reloadOnChange: true);
                    config.AddJsonFile("Sessions.config", optional: false, reloadOnChange: true);
                    config.AddJsonFile("Settings.config", optional: false, reloadOnChange: true);
                });
    }
}
