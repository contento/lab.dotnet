using System.Security.Cryptography;

namespace Encryption;

internal class AesEncryptionImp
{
    private readonly byte[] _key;

    public AesEncryptionImp(byte[] key)
    {
        this._key = key;
    }

    public byte[] Encrypt(byte[] plainText)
    {
        using var aesAlg = Aes.Create();
        aesAlg.Key = _key;

        var iv = aesAlg.IV;

        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write);
        csEncrypt.Write(plainText, 0, plainText.Length);
        csEncrypt.FlushFinalBlock();
        var cipherText = msEncrypt.ToArray();

        var encrypted = new byte[iv.Length + cipherText.Length];
        Buffer.BlockCopy(iv, 0, encrypted, 0, iv.Length);
        Buffer.BlockCopy(cipherText, 0, encrypted, iv.Length, cipherText.Length);

        return encrypted;
    }

    public byte[] Decrypt(byte[] cipherText)
    {
        using var aesAlg = Aes.Create();
        aesAlg.Key = _key;

        var iv = new byte[aesAlg.BlockSize / 8];
        var cipher = new byte[cipherText.Length - iv.Length];
        Buffer.BlockCopy(cipherText, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(cipherText, iv.Length, cipher, 0, cipher.Length);

        aesAlg.IV = iv;

        using var msDecrypt = new MemoryStream();
        using var csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Write);
        csDecrypt.Write(cipher, 0, cipher.Length);
        csDecrypt.FlushFinalBlock();
        var plainText = msDecrypt.ToArray();

        return plainText;
    }
}