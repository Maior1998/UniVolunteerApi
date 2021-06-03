using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UniVolunteerApi
{
    public static class SaltHelper
    {
        private static readonly Random rnd = new();
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[rnd.Next(30, 60)];

            using (RNGCryptoServiceProvider provider = new())
            {
                provider.GetNonZeroBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }

        public static string GetHash(string source, string salt)
        {
            return GetSha256Hash($"{source}{salt}");
        }

        private static string GetSha256Hash(string value)
        {
            StringBuilder sb = new();

            using SHA256 hash = SHA256.Create();
            Encoding enc = Encoding.UTF8;
            byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (byte b in result)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }
}
