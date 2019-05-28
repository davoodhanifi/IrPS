using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Api.Configurations;
using IrpsApi.Api.Exceptions;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Api.Models.Fcm;
using IrpsApi.Api.Models.FcmModels;
using IrpsApi.Framework.Accounts.Repositories;
using IrpsApi.Framework.System;
using IrpsApi.Framework.System.Repositories;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IrpsApi.Api.Services
{
    public class FcmService : IFcmService
    {
        private readonly IOptionsMonitor<FcmSettings> _settings;
        private readonly IPushTargetRepository _pushTargetRepository;
        private readonly ILogRepository _logRepository;

        public FcmService(IOptionsMonitor<FcmSettings> settings, IPushTargetRepository pushTargetRepository, ILogRepository logRepository)
        {
            _settings = settings;
            _pushTargetRepository = pushTargetRepository;
            _logRepository = logRepository;
        }

        public async void Send(AccountModel account, AndroidPushMessageModel androidPushModel)
        {
            try
            {
                var pushTargets = (await _pushTargetRepository.GetAllByAccountIdAsync(account.Id)).ToList();
                foreach (var pushTarget in pushTargets)
                {
                    androidPushModel.ToRegistrationId = pushTarget.Token;
                    await SendNotification(androidPushModel);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private async Task SendNotification(AndroidPushMessageModel androidPushModel, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var msg = androidPushModel;

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={_settings.CurrentValue.ServerKey}");
                    var res = await client.PostAsync("https://fcm.googleapis.com/fcm/send", new StringContent(JsonConvert.SerializeObject(new FcmModel
                    {
                        Notification = msg.Notification,
                        RegistrationIds = new[]
                        {
                            msg.ToRegistrationId
                        },
                        Data = msg.Data
                    }, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }), Encoding.UTF8, "application/json"), cancellationToken);

                    if (!res.IsSuccessStatusCode)
                    {
                        _logRepository.InsertLog("Irps.API", LogLevelIds.Error, null, "FcmService.SendNotification", "Description", "خطا در ارسال پوش نوتیفیکیشن.", "key", _settings.CurrentValue.ServerKey, "Notification", msg.Notification, "RegistrationIds", msg.ToRegistrationId, "Data", msg.Data, "body", msg.Notification.Body, "HttpStatusCode", res.StatusCode);

                        if (res.StatusCode >= (HttpStatusCode)500)
                            throw new RetryableException($"HTTP Error, StatusCode: {res.StatusCode}");

                        if (res.StatusCode >= (HttpStatusCode)400)
                            throw new StopConsumingException($"HTTP Error, StatusCode: {res.StatusCode}");
                    }

                    _logRepository.InsertLog("Irps.API", LogLevelIds.Information, null, "FcmService.SendNotification", "Description", "ارسال پوش نوتیفیکیشن.", "key", _settings.CurrentValue.ServerKey, "Notification", msg.Notification, "RegistrationIds", msg.ToRegistrationId, "Data", msg.Data, "body", msg.Notification.Body);

                    var responseString = await res.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<FcmResponseModel>(responseString);
                    if (response.Failure != 0)
                    {
                        _logRepository.InsertLog("Irps.API", LogLevelIds.Error, null, "FcmService.SendNotification", "Description", "خطا در ارسال پوش نوتیفیکیشن، response.Failure.", "key", _settings.CurrentValue.ServerKey, "Notification", msg.Notification, "RegistrationIds", msg.ToRegistrationId, "Data", msg.Data, "body", msg.Notification.Body, "HttpStatusCode", res.StatusCode);

                        throw new InvalidMessageException($"android push failed, reg_ids: {msg.ToRegistrationId}, body: {msg.Notification.Body}");
                    }
                }
            }
            catch (HttpRequestException exception)
            {
                throw new RetryableException(exception.Message, exception);
            }
        }
    }
}