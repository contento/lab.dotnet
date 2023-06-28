using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace DotNetFramework.Encryption
{
    public class DataEncryptionHelper
    {
        private static readonly Lazy<DataEncryptionHelper> Lazy =
            new Lazy<DataEncryptionHelper>(() => new DataEncryptionHelper());

        public static DataEncryptionHelper Instance => Lazy.Value;

        private const int NonceSize = 12;

        private DataEncryptionHelper()
        {
        }

        public byte[] Encrypt(string plainText, byte[] key)
        {
            ValidateKeyLength(key);

            var nonce = new byte[NonceSize];
            new SecureRandom().NextBytes(nonce);

            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(key), 128, nonce);
            cipher.Init(true, parameters);

            var cipherText = new byte[cipher.GetOutputSize(plainBytes.Length)];
            var len = cipher.ProcessBytes(plainBytes, 0, plainBytes.Length, cipherText, 0);
            cipher.DoFinal(cipherText, len);

            var result = new byte[nonce.Length + cipherText.Length];
            Buffer.BlockCopy(nonce, 0, result, 0, nonce.Length);
            Buffer.BlockCopy(cipherText, 0, result, nonce.Length, cipherText.Length);

            return result;
        }

        public string Decrypt(byte[] cipherData, byte[] key)
        {
            ValidateKeyLength(key);

            var nonce = new byte[NonceSize];
            var cipherText = new byte[cipherData.Length - NonceSize];

            Buffer.BlockCopy(cipherData, 0, nonce, 0, NonceSize);
            Buffer.BlockCopy(cipherData, NonceSize, cipherText, 0, cipherText.Length);

            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(key), 128, nonce);
            cipher.Init(false, parameters);

            var plainBytes = new byte[cipher.GetOutputSize(cipherText.Length)];
            var len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainBytes, 0);
            cipher.DoFinal(plainBytes, len);

            return Encoding.UTF8.GetString(plainBytes);
        }

        private static void ValidateKeyLength(IReadOnlyCollection<byte> key)
        {
            if (key.Count != 16 && key.Count != 24 && key.Count != 32)
                throw new ArgumentException("Key must be 128, 192, or 256 bits.");
        }
    }
}