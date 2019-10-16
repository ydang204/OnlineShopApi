using System;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShop.Common.Utitlities
{
    public static class CryptoEngine
    {
        /// <summary>
        /// Hash password with HMACSHA512 instant
        /// </summary>
        /// <param name="password">input plain password</param>
        public static string HashPassword(this string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            SHA512 sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha512.ComputeHash(bytes);

            return hash.HexStringFromBytes();
        }

        /// <summary>
        /// Compare hashed password with an input password
        /// </summary>
        /// <param name="hashedPassword"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool VerifyHashPassword(this string hashedPassword, string password)
        {
            if (string.IsNullOrEmpty(hashedPassword) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            string comparePassword = password.HashPassword();

            if (hashedPassword != comparePassword)
            {
                return false;
            }

            return true;
        }
    }
}