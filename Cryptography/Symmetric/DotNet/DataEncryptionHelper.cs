using System.Security.Cryptography;
using System.Text;

namespace DotNet.Encryption;

public class DataEncryptionHelper
{
    private static readonly Lazy<DataEncryptionHelper> LazyInstance = new(() => new DataEncryptionHelper());
    public static DataEncryptionHelper Instance => LazyInstance.Value;


    public byte[] Encrypt(string plainText, byte[] key)
    {
        ValidateKeyLength(key);

        using var aesGcm = new AesGcm(key);
        var nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
        RandomNumberGenerator.Fill(nonce);

        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var cipherText = new byte[plainBytes.Length];
        var tag = new byte[AesGcm.TagByteSizes.MaxSize];

        aesGcm.Encrypt(nonce, plainBytes, cipherText, tag);

        var result = new byte[nonce.Length + cipherText.Length + tag.Length];
        Buffer.BlockCopy(nonce, 0, result, 0, nonce.Length);
        Buffer.BlockCopy(cipherText, 0, result, nonce.Length, cipherText.Length);
        Buffer.BlockCopy(tag, 0, result, nonce.Length + cipherText.Length, tag.Length);

        return result;
    }

    public string Decrypt(byte[] cipherData, byte[] key)
    {
        ValidateKeyLength(key);

        using var aesGcm = new AesGcm(key);
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

    private static void ValidateKeyLength(IReadOnlyCollection<byte> key)
    {
        if (key.Count != 16 && key.Count != 24 && key.Count != 32)
            throw new ArgumentException("Key must be 128, 192, or 256 bits.");
    }
}