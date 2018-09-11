namespace IrpsApi.Api.Services
{
    public interface ISmsService
    {
        void SendVerificationSms(string mobileNumber, string verificationCode);
    }
}