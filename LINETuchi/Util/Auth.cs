using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LINETuchi.Util
{
    public class Auth
    {
        // 署名チェック
        public static bool IsSingatureOk(string signature, string text, string key)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            var keyBytes = Encoding.UTF8.GetBytes(key);

            using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
            {
                var hash = hmac.ComputeHash(textBytes, 0, textBytes.Length);
                var hash64 = Convert.ToBase64String(hash);

                return signature == hash64;
            }
        }
    }
}
