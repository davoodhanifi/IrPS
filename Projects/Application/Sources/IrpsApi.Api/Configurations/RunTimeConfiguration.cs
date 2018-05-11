using IrpsApi.Framework.System;
using Microsoft.Extensions.DependencyInjection;
using Noandishan.IrpsApi.Repositories.System;

namespace IrpsApi.Api.Configurations
{
    public static class RunTimeConfiguration
    {
        public static void RegisterComponents(this IServiceCollection services)
        {
            services.AddScoped(typeof(IOtp), typeof(Otp));

            services.AddScoped(typeof(IOtpRepository), typeof(OtpRepository));
        }
    }
}
