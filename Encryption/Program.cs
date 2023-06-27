using System.Security.Cryptography;
using Encryption;

const string plainText = "Hello World!";
Console.WriteLine($"Plain text:\t{plainText}");

var key = new byte[16];
RandomNumberGenerator.Fill(key);
Console.WriteLine($"Key:\t\t{Convert.ToBase64String(key)}");

var cipherData = DataEncryptionHelper.Instance?.Encrypt(plainText, key);

if (cipherData != null)
{
    var decryptedText = DataEncryptionHelper.Instance?.Decrypt(cipherData, key);
    Console.WriteLine($"Decrypted text:\t{decryptedText}");
}