using System.Security.Cryptography;
using Encryption;

const string plainText = "Hello World!";
Console.WriteLine($"Plain text: {plainText}");

var key = new byte[16];
RandomNumberGenerator.Fill(key);
Console.WriteLine($"Key: {Convert.ToBase64String(key)}");

var encryptionHelper = new DataEncryptionHelper(key);

var cipherData = encryptionHelper.EncryptData(plainText);

var decryptedText = encryptionHelper.DecryptData(cipherData);

Console.WriteLine($"Decrypted text: {decryptedText}");