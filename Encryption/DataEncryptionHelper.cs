using System.Security.Cryptography;
using System.Text;

namespace Encryption;

public class DataEncryptionHelper
{
    private readonly byte[] _key;

    public DataEncryptionHelper(byte[] key)
    {
        if (key.Length != 16 && key.Length != 24 && key.Length != 32)
            throw new ArgumentException("Key must be 128, 192, or 256 bits.");

        this._key = key;
    }

    public byte[] EncryptData(string plainText)
    {
        using var aesGcm = new AesGcm(_key);
        var nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
        RandomNumberGenerator.Fill(nonce);

        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var cipherText = new byte[plainBytes.Length];
        var tag = new byte[AesGcm.TagByteSizes.MaxSize];

        aesGcm.Encrypt(nonce, plainBytes, cipherText, tag);

        // Concatenate nonce, cipher text, and tag
        var result = new byte[nonce.Length + cipherText.Length + tag.Length];
        Buffer.BlockCopy(nonce, 0, result, 0, nonce.Length);
        Buffer.BlockCopy(cipherText, 0, result, nonce.Length, cipherText.Length);
        Buffer.BlockCopy(tag, 0, result, nonce.Length + cipherText.Length, tag.Length);

        return result;
    }

    public string DecryptData(byte[] cipherData)
    {
        using var aesGcm = new AesGcm(_key);
        var nonceSize = AesGcm.NonceByteSizes.MaxSize;
        var tagSize = AesGcm.TagByteSizes.MaxSize;

        var nonce = new byte[nonceSize];
        var cipherText = new byte[cipherData.Length - nonceSize - tagSize];
        var tag = new byte[tagSize];

        Buffer.BlockCopy(cipherData, 0, nonce, 0, nonceSize);
        Buffer.BlockCopy(cipherData, nonceSize, cipherText, 0, cipherText.Length);
        Buffer.BlockCopy(cipherData, nonceSize + cipherText.Length, tag, 0, tagSize);

        var decryptedData = new byte[cipherText.Length];

        aesGcm.Decrypt(nonce, cipherText, tag, decryptedData);

        return Encoding.UTF8.GetString(decryptedData);
    }
}