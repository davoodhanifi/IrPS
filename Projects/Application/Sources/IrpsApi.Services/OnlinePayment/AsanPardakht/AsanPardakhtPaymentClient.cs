using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework.OnlinePayment;

namespace IrpsApi.Services.OnlinePayment.AsanPardakht
{
    public class AsanPardakhtPaymentClient : IOnlinePaymentClient
    {
        private string _tokenEndpointAddress;
        private int _merchantId;
        private string _username;
        private string _password;

        private string _aesKey;
        private string _aesIv;

        private IDictionary<string, string> _verificationErrorMessages = new Dictionary<string, string>
        {
            {"500","بازبيني تراكنش با موفقيت انجام شد"},
            {"501","پرادزش هنوز انجام نشده است"},
            {"502","وضعيت تراكنش نامشخص است"},
            {"503","تراكنش اصلي ناموفق بوده است"},
            {"504","قبﻼ درخواست بازبيني براي اين تراكنش داده شده است"},
            {"505","قبﻼ درخواست تسويه براي اين تراكنش ارسال شده است"},
            {"506","قبﻼ درخواست بازگشت براي اين تراكنش ارسال شده است"},
            {"507","تراكنش در ليست تسويه قرار دادر"},
            {"508","تراكنش در ليست بازگشت قرار دادر"},
            {"509","امكان انجام عمليات به سبب وجود مشكل دالخ ي وجود ندارد"},
            {"510","هويت درخواست كننده عمليتا نامعتبر است"},
            {"511","قبﻼ درخواست كنسلي بازبيني براي اين تراكنش شده است"},
        };

        private IDictionary<string, string> _reconciliationErrorMessages = new Dictionary<string, string>
        {
            {"600","درخواست تسويه تراكنش با موفقيت ارسال شد"},
            {"601","پرادزش هنوز انجام نشده است"},
            {"602","وضعيت تراكنش نامشخص است"},
            {"603","تراكنش اصلي ناموفق بوده است"},
            {"604","تراكنش بازبيني نشده است"},
            {"605","قبﻼ درخواست بازگشت براي اين تراكنش ارسال شده است"},
            {"606","قبﻼ درخواست تسويه براي اين تراكنش ارسال شده است"},
            {"607","امكان انجام عمليات به سبب وجود مشكل داخلي وجود ندارد"},
            {"608","تراكنش در ليست منتظر بازگشت ها وجود دادر"},
            {"609","تراكنش در ليست منتظر تسويه ها وجود دادر"},
            {"610","هويت درخواست كننده عمليات نامعتبر است"},
            {"611","قبﻼ درخواست كنسلي بازبيني براي اين تراكنش شده است"}
        };

        public string GatewayUrl
        {
            get;
        }

        public Task<VerifyResult> VerifyTransaction(IOnlinePayment onlinePayment, string paygateTransactionId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //public AsanPardakhtPaymentClient()
        //{
        //    _tokenEndpointAddress = ConfigurationManager.AppSettings["OnlinePayment.AsanPardakht.TokenWebServiceAddress"];
        //    _merchantId = int.Parse(ConfigurationManager.AppSettings["OnlinePayment.AsanPardakht.MerchantId"]);
        //    _username = ConfigurationManager.AppSettings["OnlinePayment.AsanPardakht.Username"];
        //    _password = ConfigurationManager.AppSettings["OnlinePayment.AsanPardakht.Password"];
        //    _aesKey = ConfigurationManager.AppSettings["OnlinePayment.AsanPardakht.AesKey"];
        //    _aesIv = ConfigurationManager.AppSettings["OnlinePayment.AsanPardakht.AesIV"];
        //    BaseUrl = ConfigurationManager.AppSettings["OnlinePayment.AsanPardakht.BaseUrl"];
        //}

        //public merchantservicesSoap CreateClient()
        //{
        //    var binding = new BasicHttpBinding();
        //    binding.Security.Mode = BasicHttpSecurityMode.Transport;

        //    var endpoint = new EndpointAddress(_tokenEndpointAddress);

        //    return new merchantservicesSoapClient(binding, endpoint);
        //}

        public bool Encrypt(String Input, out string encryptedString)
        {
            try
            {
                var aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 256;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Convert.FromBase64String(this._aesKey);
                aes.IV = Convert.FromBase64String(this._aesIv);

                var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] xBuff = null;
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                    {
                        byte[] xXml = Encoding.UTF8.GetBytes(Input);
                        cs.Write(xXml, 0, xXml.Length);
                    }

                    xBuff = ms.ToArray();
                }

                encryptedString = Convert.ToBase64String(xBuff);
                return true;
            }
            catch (Exception ex)
            {
                encryptedString = string.Empty;
                return false;
            }
        }

        public bool Decrypt(String input, out string decodedString)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Convert.FromBase64String(this._aesKey);
                aes.IV = Convert.FromBase64String(this._aesIv);
                var decrypt = aes.CreateDecryptor();
                byte[] xBuff = null;
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                    {
                        byte[] xXml = Convert.FromBase64String(input);
                        cs.Write(xXml, 0, xXml.Length);
                    }
                    xBuff = ms.ToArray();
                }
                decodedString = Encoding.UTF8.GetString(xBuff);
                return true;
            }
            catch (Exception ex)
            {
                decodedString = string.Empty;
                return false;
            }
        }

        public async Task<IDictionary<string, string>> InitPaymentAsync(long amount, string tracingId, string[] additionalData, string redirectUrl, CancellationToken cancellationToken)
        {
            //var client = CreateClient();

            var additionalDataStr = string.Join(";", additionalData);
            if (additionalDataStr.Length > 100)
                additionalDataStr = additionalDataStr.Substring(0, 100);

            var p1 = 1;
            var p2 = _username;
            var p3 = _password;
            var p4 = tracingId;
            var p5 = amount;
            var p6 = DateTime.Now.ToString("yyyyMMdd HHmmss");
            var p7 = additionalDataStr; // extra up to 100chars
            var p8 = redirectUrl;
            var p9 = 0;

            var request = $"{p1},{p2},{p3},{p4},{p5},{p6},{p7},{p8},{p9}";
            if (!Encrypt(request, out var encryptedRequest))
                throw new Exception("Encryption error");

            //var result = await client.RequestOperationAsync(new RequestOperationRequest(new RequestOperationRequestBody(_merchantId, encryptedRequest)));

            //var resultParts = result.Body.RequestOperationResult.Split(',');
            //if (resultParts.Length == 1 || resultParts[0] != "0")
            //    throw new AsanPardakhtException($"InitPayment Error: {resultParts[0]}");

            //return new Dictionary<string, string>
            //{
            //    { "RefId", resultParts[1] }
            //};
            return null;
        }

        public CallbackParametersProcessResult ProcessCallbackParameters(IDictionary<string, string> parameters)
        {
            Decrypt(parameters["ReturningParams"], out var returningParamsStr);
            var returningParams = returningParamsStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var additionalParameters = new Dictionary<string, string>
            {
                {"Amount", returningParams[0] },
                {"SaleOrderId", returningParams[1]},
                {"RefId", returningParams[2] },
                {"ResCode", returningParams[3] },
                {"MessageText", returningParams[4] },
                {"PayGateTranID", returningParams.Length > 5 ? returningParams[5] : null },
                {"PRN", returningParams.Length > 6 ? returningParams[6] : null },
                {"LastFourDigitsOfPan", returningParams.Length >= 7 ? returningParams[7] : null},
            };

            var result = new CallbackParametersProcessResult();
            result.AdditionalParameters = additionalParameters;
            result.OnlinePaymentId = additionalParameters["SaleOrderId"];

            additionalParameters.TryGetValue("PayGateTranID", out var transactionId);
            result.TransactionId = transactionId;

            var resCode = additionalParameters["ResCode"];
            if (resCode == "00" || resCode == "0")
            {
                result.IsSuccess = true;
                result.MessageText = additionalParameters["MessageText"];
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorCode = "payment_faield";
                additionalParameters.TryGetValue("MessageText", out var messageText);
                result.ErrorMessage = messageText;
            }

            return result;
        }

        //public async Task<VerifyResult> VerifyTransaction(IOnlinePayment onlinePayment, string paygateTransactionId, CancellationToken cancellationToken)
        //{
        //    var client = CreateClient();

        //    Encrypt($"{_username},{_password}", out var encryptedCredentials);

        //    var response = await client.RequestVerificationAsync(new RequestVerificationRequest(new RequestVerificationRequestBody(_merchantId, encryptedCredentials, ulong.Parse(paygateTransactionId))));
        //    var verifyResult = response.Body.RequestVerificationResult;

        //    if (verifyResult == "500")
        //    {
        //        var reconciliationResponse = await client.RequestReconciliationAsync(new RequestReconciliationRequest(new RequestReconciliationRequestBody(_merchantId, encryptedCredentials, ulong.Parse(paygateTransactionId))));
        //        var reconciliationResult = reconciliationResponse.Body.RequestReconciliationResult;

        //        if (reconciliationResult == "600")
        //        {
        //            return new VerifyResult
        //            {
        //                IsSuccess = true,
        //                PaidAmount = onlinePayment.Amount
        //            };
        //        }
        //        else
        //        {
        //            return new VerifyResult
        //            {
        //                IsSuccess = false,
        //                ErrorCode = "paygate_verify_error",
        //                ErrorMessage = _reconciliationErrorMessages.TryGetValue(reconciliationResult, out var msg) ? msg : reconciliationResult
        //            };
        //        }
        //    }
        //    else
        //    {
        //        return new VerifyResult
        //        {
        //            IsSuccess = false,
        //            ErrorCode = "paygate_verify_error",
        //            ErrorMessage = _verificationErrorMessages.TryGetValue(verifyResult, out var msg) ? msg : verifyResult
        //        };
        //    }
        //}
    }
}