using IrpsApi.Api.Models.Accounts;
using IrpsApi.Api.Models.Fcm;
using IrpsApi.Framework.Accounts;

namespace IrpsApi.Api.Services
{
    public interface IFcmService
    {
        void Send(AccountModel account, NotificationModel notification);
    }
}