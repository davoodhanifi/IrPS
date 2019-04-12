using IrpsApi.Api.Models.Accounts;
using IrpsApi.Api.Security;
using IrpsApi.Api.Services;
using IrpsApi.Framework.Accounting.Repositories;
using IrpsApi.Framework.Accounts.Repositories;
using IrpsApi.Framework.Bank.Repositories;
using IrpsApi.Framework.Operation;
using IrpsApi.Framework.Operation.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Noandishan.IrpsApi.Repositories.Accounting;
using Noandishan.IrpsApi.Repositories.Accounts;
using Noandishan.IrpsApi.Repositories.Bank;
using Noandishan.IrpsApi.Repositories.ConnectionStrings;
using Noandishan.IrpsApi.Repositories.Operation;

namespace IrpsApi.Api.Configurations
{
    public static class DependencyConfigurations
    {
        public static void RegisterConnectionStrings(this IServiceCollection services)
        {
            services.AddSingleton<IIrpsConnectionString, IrpsConnectionString>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ISessionRepository, SessionRepository>();
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<IPersonProfileRepository, PersonProfileRepository>();
            services.AddSingleton<ITransactionRepository, TransactionRepository>();
            services.AddSingleton<ITransactionTypeRepository, TransactionTypeRepository>();
            services.AddSingleton<IBalanceRepository, BalanceRepository>();
            services.AddSingleton<IPushTargetRepository, PushTargetRepository>();
            services.AddSingleton<IDocumentRepository, DocumentRepository>();
            services.AddSingleton<IBankAccountRepository, BankAccountRepository>();
            services.AddSingleton<IRequestStatusRepository, RequestStatusRepository>();
            services.AddSingleton<IRequestTypeRepository, RequestTypeRepository>();
            services.AddSingleton<IRequestRepository, RequestRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<ISessionPrincipalProvider, SessionPrincipalProvider>();
            //AddMessagingClient(services);
            //AddMessagingQueueSmsService(services);
            services.AddSingleton<ISmsService, SmsService>();
            services.AddSingleton<IFcmService, FcmService>();
        }

        //private static void AddMessagingQueueSmsService(IServiceCollection services)
        //{
        //    services.AddSingleton(sp =>
        //    {
        //        var client = sp.GetRequiredService<Mabna.MessagingQueueSdk.Client>();
        //        var smsExchange = sp.GetRequiredService<IOptionsMonitor<SmsSettings>>().CurrentValue.SmsQueueExchange;
        //        return client.CreateSmsService(smsExchange);
        //    });
        //}

        //private static void AddMessagingClient(IServiceCollection services)
        //{
        //    services.AddSingleton(c =>
        //    {
        //        var connectionString = c.GetRequiredService<IOptionsMonitor<ConnectionStringsOption>>().CurrentValue.MessagingQueue;
        //        var applicationId = c.GetRequiredService<IOptionsMonitor<SmsSettings>>().CurrentValue.MessagingApplicationId;
        //        return new Mabna.MessagingQueueSdk.Client(connectionString, applicationId);
        //    });
        //}
    }
}