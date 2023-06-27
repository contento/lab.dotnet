using System;
using System.IO;
using System.Security.Cryptography;

public class AesEncryption
{
    private readonly byte[] key;

    public AesEncryption(byte[] key)
    {
        this.key = key;
    }

    public byte[] Encrypt(byte[] plainText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;

            byte[] iv = aesAlg.IV;

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    csEncrypt.Write(plainText, 0, plainText.Length);
                    csEncrypt.FlushFinalBlock();
                    byte[] cipherText = msEncrypt.ToArray();

                    byte[] encrypted = new byte[iv.Length + cipherText.Length];
                    Buffer.BlockCopy(iv, 0, encrypted, 0, iv.Length);
                    Buffer.BlockCopy(cipherText, 0, encrypted, iv.Length, cipherText.Length);

                    return encrypted;
                }
            }
        }
    }

    public byte[] Decrypt(byte[] cipherText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;

            byte[] iv = new byte[aesAlg.BlockSize / 8];
            byte[] cipher = new byte[cipherText.Length - iv.Length];
            Buffer.BlockCopy(cipherText, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(cipherText, iv.Length, cipher, 0, cipher.Length);

            aesAlg.IV = iv;

            using (MemoryStream msDecrypt = new MemoryStream())
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    csDecrypt.Write(cipher, 0, cipher.Length);
                    csDecrypt.FlushFinalBlock();
                    byte[] plainText = msDecrypt.ToArray();

                    return plainText;
                }
            }
        }
    }
}

// use the class AesEncryption
byte[] key = new byte[32] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 };
byte[] plainText = System.Text.Encoding.UTF8.GetBytes("my secret text");
byte[] cipherText;


AesEncryption aes = new AesEncryption(key);

Console.WriteLine("[Original] Plain Text: " + plainText);
cipherText = aes.Encrypt(plainText);

plainText = aes.Decrypt(cipherText);
Console.WriteLine("[Restored] Plain Text: " + plainText);

