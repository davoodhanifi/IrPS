using System;
using IrpsApi.Framework.OnlinePayment;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace IrpsApi.Services.OnlinePayment.Saman
{
    public class SamanOnlinePaymentClient : IOnlinePaymentClient
    {
        private string _merchantId;
        private string _tokenEndpointAddress;
        private string _verifyEndpointAddress;

        private IDictionary<int, string> _verificationErrorMessages = new Dictionary<int, string>
        {
            {-1  , "خطا در پردازش اطلاعات ارسالی، مشکل در یکی از ورودی‌ها و ناموفق بودن فراخوانی متد برگشت"},
            {-3  , "ورودی‌ها حاوی کارکترهای غیرمجاز می‌باشند"},
            {-4  , "Merchant Authentication Failed (کلمه عبور یا کد فروشنده اشتباه است)"},
            {-6  , "سند قبلا برگشت کامل یافته است یا خارج از زمان ۳۰ دقیقه ارسال شده است"},
            {-7  , "رسید دیجیتالی تهی است"},
            {-8  , ".طول ورودی‌ها بیشتر از حد مجاز است"},
            {-9  , ".وجود کارکترهای غیرمجاز در مبلغ برگشتی"},
            {-10 , "رسید دیجیتالی به غیر مجاز است"},
            {-11 , "طول ورودی‌ها کمتر از حد مجاز است"},
            {-12 , "مبلغ برگشتی منفی است"},
            {-13 , "مبلغ برگشتی برای برگشت جزئی بیش از مبلغ برگشت نخورده‌ی رسید دیجیتالی است"},
            {-14 , "چنین تراکنشی تعریف نشده است"},
            {-15 , "مبلغ برگشتی به صورت اعشاری داده شده است"},
            {-16 , "خطای داخلی سیستم"},
            {-17 , "برگشت زدن جزیی تراکنش مجاز نمی‌باشد"},
            {-18 , "آدرس IP فروشنده یا رمز اشتباه است."},
        };

        public string GatewayUrl
        {
            get;
        }

        public SamanOnlinePaymentClient(IOptionsMonitor<SamanGatewaySettings> settings)
        {
            _merchantId = settings.CurrentValue.MerchantId;
            _tokenEndpointAddress = settings.CurrentValue.TokenWebServiceAddress;
            _verifyEndpointAddress = settings.CurrentValue.VerifyWebServiceAddress;
            GatewayUrl = settings.CurrentValue.GatewayUrl;
        }

        public PaymentIFBindingSoap CreateClient()
        {
            var binding = new BasicHttpBinding();
            binding.Security.Mode = BasicHttpSecurityMode.Transport;

            var endpoint = new EndpointAddress(_tokenEndpointAddress);

            return new PaymentIFBindingSoapClient(binding, endpoint);
        }

        public IrpsApi.OnlinePayment.Saman.ReferencePayment.PaymentIFBindingSoapClient CreateVerifyClient()
        {
            var binding = new BasicHttpBinding();
            binding.Security.Mode = BasicHttpSecurityMode.Transport;

            var endpoint = new EndpointAddress(_verifyEndpointAddress);

            return new IrpsApi.OnlinePayment.Saman.ReferencePayment.PaymentIFBindingSoapClient(binding, endpoint);
        }

        public async Task<IDictionary<string, string>> InitPaymentAsync(long amount, string tracingId, string[] additionalData, string redirectUrl, CancellationToken cancellationToken)
        {
            var additionalData1 = additionalData.Length > 0 ? additionalData[0] : null;
            var additionalData2 = additionalData.Length > 1 ? additionalData[1] : null;

            var client = CreateClient();
            var result = await client.RequestTokenAsync(_merchantId, tracingId, amount, 0, 0, 0, 0, 0, 0, additionalData1, additionalData2, 0, redirectUrl);

            if (result == "-1")
                throw new SamanException(result);

            return new Dictionary<string, string>
            {
                { "token", result },
                { "RedirectUrl", redirectUrl }
            };
        }

        IDictionary<string, string> _stateCodeErrorMessages = new Dictionary<string, string>
        {
            {"-1",  "تراکنش توسط خریدار کنسل شده است."},
            {"79",  "مبلغ سند برگشتی، از مبلغ تراکنش اصلی بیشتر است."},
            {"12",  "درخواست برگشت یک تراکنش رسیده است، در حالی که تراکنش اصلی پیدا نمی‌شود"},
            {"14",  "شماره کارت نامعتبر است."},
            {"15",  "چنین صادرکننده‌ کارتی وجود ندارد."},
            {"33",  "از تاریخ انقضای کارت گذشته و کارت دیگر معتبر نیست."},
            {"38",  "رمز کارت ۳ مرتبه اشتباه وارد شده است در نتیجه کارت غیرفعال خواهد شد."},
            {"55",  "خریدار رمز کارت را اشتباه وارد کرده است."},
            {"61",  "مبلغ بیش از سقف برداشت می‌باشد"},
            {"93",  "تراکنش Authorize شده است ولی امکان سند خوردن وجود ندارد"},
            {"68",  "تراکنش در شبکه بانکی Timout خورده است"},
            {"34",  "خریدار یا فیلد CVV2 یا فیلد ExpDate را اشتباه وارد کرده است."},
            {"51",  "موجودی حساب خریدار کافی نیست"},
            {"84",  "سیستم بانک صادر کننده کارت خریدارِ، در وضعیت عملیاتی نیست."},
            {"96",  "خطای بانکی"}
        };

        public CallbackParametersProcessResult ProcessCallbackParameters(IDictionary<string, string> parameters)
        {
            var state = parameters["State"];
            var stateCode = parameters["StateCode"];

            if (state == "OK")
            {
                return new CallbackParametersProcessResult
                {
                    IsSuccess = true,
                    OnlinePaymentId = parameters["ResNum"],
                    TransactionId = parameters["RefNum"],
                    AdditionalParameters = parameters
                };
            }

            _stateCodeErrorMessages.TryGetValue(stateCode, out var message);
            return new CallbackParametersProcessResult
            {
                IsSuccess = false,
                OnlinePaymentId = parameters["ResNum"],
                ErrorCode = stateCode,
                ErrorMessage = message,
                AdditionalParameters = parameters
            };
        }

        public async Task<VerifyResult> VerifyTransaction(IOnlinePayment onlinePayment, string paygateTransactionId, CancellationToken cancellationToken)
        {
            var client = CreateVerifyClient();
            var result = await client.verifyTransactionAsync(paygateTransactionId, _merchantId);

            if (result != Convert.ToDouble(onlinePayment.Amount))
            {
                if (result > 0)
                {
                    return new VerifyResult
                    {
                        IsSuccess = false,
                        ErrorCode = "paygate_verify_error",
                        ErrorMessage = "مقدار پرداختی نامعتبر است."
                    };
                }

                return new VerifyResult
                {
                    IsSuccess = false,
                    ErrorCode = "paygate_verify_error",
                    ErrorMessage = _verificationErrorMessages.TryGetValue((int)result, out var msg) ? msg : result.ToString(CultureInfo.InvariantCulture)
                };
            }

            return new VerifyResult
            {
                IsSuccess = true,
                PaidAmount = (decimal)result
            };
        }
    }
}