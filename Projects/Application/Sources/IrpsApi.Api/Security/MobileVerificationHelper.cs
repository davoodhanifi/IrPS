using System.Security.Cryptography;
using System.Text;

namespace IrpsApi.Api.Security
{
    public class MobileVerificationHelper
    {
        private static readonly char[] AvailableCharacters =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        public static string GetUniqueKey(int length)
        {
            byte[] data;
            using (var crypto = new RNGCryptoServiceProvider())
            {
                data = new byte[length];
                crypto.GetNonZeroBytes(data);
            }
            var result = new StringBuilder(length);
            foreach (var b in data)
            {
                result.Append(AvailableCharacters[b % AvailableCharacters.Length]);
            }
            return result.ToString();
        }

        public static string GenerateToken(int length = 6)
        {
            return GetUniqueKey(length);
        }
    }
}
