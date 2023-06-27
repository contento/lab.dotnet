using System.Text;
using Encryption;

var key = new byte[32]
{
    1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
    17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32
};
var textBytes = Encoding.UTF8.GetBytes("Hello world !");
var clearText = Encoding.UTF8.GetString(textBytes);


Console.WriteLine($"[Original] Plain Text: {clearText}");


var aes = new AesEncryptionImp(key);
var cipherText = aes.Encrypt(textBytes);
Console.WriteLine($"[Encrypted] Cipher Text (Base64): {Convert.ToBase64String(cipherText)}");


textBytes = aes.Decrypt(cipherText);
clearText = Encoding.UTF8.GetString(textBytes);
Console.WriteLine($"[Restored] Plain Text: {clearText}");