using System;
using System.Security.Cryptography;

namespace Infrastructure.Extensions.Security
{
    public class SecurityCrypto
    {
        private static readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();

        /// <summary>
        /// Creates a random key byte array.
        /// </summary>
        /// <param name="length">Length.</param>
        /// <returns></returns>
        private static byte[] GetRandomKey(int length = 32)
        {
            var bytes = new byte[length];
            RandomNumberGenerator.GetBytes(bytes);

            return bytes;
        }

        /// <summary>
        /// Creates a safe unique identifier.
        /// </summary>
        /// <returns></returns>
        public static string GetRandomSecret()
        {
            var bytes = GetRandomKey();

            return BitConverter.ToString(bytes);
        }
    }
}