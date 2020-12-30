using System;
using System.Security.Cryptography;
using System.Text;

namespace eShopWithReact.Services.UserAuthentication.Core.Helpers
{
    public class HashingHelper
    {
        private static readonly int saltSize = 40;
        private static readonly int iterationsCount = 10000;

        public static string HashUsingPbkdf2(string password, string salt)
        {
            using var bytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), iterationsCount, HashAlgorithmName.SHA256);
            var derivedRandomKey = bytes.GetBytes(32);
            var hash = Convert.ToBase64String(derivedRandomKey);
            return hash;
        }

        public static string GeneratePasswordSalt()
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            var randNum = new Random();
            var chars = new char[saltSize];
            var allowedCharCount = allowedChars.Length;
            for (var i = 0; i <= saltSize - 1; i++)
            {
                chars[i] = allowedChars[Convert.ToInt32((allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
    }
}
