using IrpsApi.Framework.System;
using IrpsApi.Framework.User;
using Microsoft.Extensions.DependencyInjection;
using Noandishan.IrpsApi.Repositories.System;
using Noandishan.IrpsApi.Repositories.User;

namespace IrpsApi.Api.Configurations
{
    public static class RunTimeConfiguration
    {
        public static void RegisterComponents(this IServiceCollection services)
        {
            // System
            services.AddScoped(typeof(IOtp), typeof(Otp));
            services.AddScoped(typeof(IOtpRepository), typeof(OtpRepository));

            // User
            services.AddScoped(typeof(IUser), typeof(User));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        }
    }
}
