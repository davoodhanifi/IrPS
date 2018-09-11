using IrpsApi.Api.Configurations;
using Kavenegar;
using Microsoft.Extensions.Options;
using Kavenegar.Core.Exceptions;

namespace IrpsApi.Api.Services
{
    public class SmsService : ISmsService
    {
        private readonly IOptionsMonitor<SmsSettings> _settings;
        //private readonly ISmsService _smsService;

        public SmsService(IOptionsMonitor<SmsSettings> settings)
        {
            _settings = settings;
        }

        public async void SendVerificationSms(string mobileNumber, string verificationCode)
        {
            try
            {
                var api = new KavenegarApi("734E6D44426F516F4973307A485662586E4B6E5449513D3D");
                await api.VerifyLookup(mobileNumber, verificationCode, "IrPSVerificationTemplate");
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                throw new ApiException(ex.ToString(), (int)ex.Code);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                throw new HttpException(ex.ToString(), ex.Code);
            }
        }
    }
}