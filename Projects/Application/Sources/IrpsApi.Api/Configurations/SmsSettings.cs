namespace IrpsApi.Api.Configurations
{
    public class SmsSettings
    {
        public string SmsVerificationProfile
        {
            get;
            set;
        }

        public string MessagingApplicationId
        {
            get;
            set;
        }

        public string SmsQueueExchange
        {
            get;
            set;
        }
    }
}