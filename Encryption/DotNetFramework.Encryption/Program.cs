using System;
using System.Security.Cryptography;


namespace DotNetFramework.Encryption
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string plainText = "Hello World!";
            Console.WriteLine($"Plain text:\t{plainText}");

            var key = new byte[16];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(key);
            }

            Console.WriteLine($"Key:\t\t{Convert.ToBase64String(key)}");

            var cipherData = DataEncryptionHelper.Instance?.Encrypt(plainText, key);

            if (cipherData == null)
            {
                throw new InvalidOperationException();
            }

            var decryptedText = DataEncryptionHelper.Instance?.Decrypt(cipherData, key);
            Console.WriteLine($"Decrypted text:\t{decryptedText}");
        }
    }
}