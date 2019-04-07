using System;
using System.Linq;
using CorePush.Google;
using IrpsApi.Api.Configurations;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Api.Models.Fcm;
using IrpsApi.Framework.Accounts.Repositories;
using Microsoft.Extensions.Options;

namespace IrpsApi.Api.Services
{
    public class FcmService : IFcmService
    {
        private readonly IOptionsMonitor<FcmSettings> _settings;
        private readonly IPushTargetRepository _pushTargetRepository;

        public FcmService(IOptionsMonitor<FcmSettings> settings, IPushTargetRepository pushTargetRepository)
        {
            _settings = settings;
            _pushTargetRepository = pushTargetRepository;
        }

        public async void Send(AccountModel account, NotificationModel notification)
        {
            try
            {
                var pushTargets = (await _pushTargetRepository.GetAllByAccountIdAsync(account.Id)).ToList();
                using (var fcm = new FcmSender(_settings.CurrentValue.ServerKey, _settings.CurrentValue.SenderId))
                {
                    foreach (var pushTarget in pushTargets)
                        await fcm.SendAsync(pushTarget.Token, notification);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}