using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Security;

namespace DotNetFramework.TestAsymmetric
{
    class Program
    {
        static void Main()
        {
            const string plainText = "Hello World!";
            Console.WriteLine($"Plain text:\t{plainText}");

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // When using the store make sure you mark the certificate as exportable.
            // (BouncyCastle needs it) !!!
            var cert = GetCertificate(fromFile: false);

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // DO NOT USE ASYMMETRIC ENCRYPTION FOR LARGE DATA
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // Encrypt
            var rsaPublicKey = DotNetUtilities.GetRsaPublicKey(cert.PublicKey.Key as RSACryptoServiceProvider);
            var encryptEngine = new OaepEncoding(new RsaEngine(), DigestUtilities.GetDigest("SHA-256"));
            encryptEngine.Init(true, rsaPublicKey);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedData = encryptEngine.ProcessBlock(plainTextBytes, 0, plainTextBytes.Length);
            Console.WriteLine($"Encrypted Data: {Convert.ToBase64String(encryptedData)}");

            // Decrypt
            var rsaPrivateKey = DotNetUtilities.GetRsaKeyPair(cert.PrivateKey as RSACryptoServiceProvider).Private;
            var decryptEngine = new OaepEncoding(new RsaEngine(), DigestUtilities.GetDigest("SHA-256"));
            decryptEngine.Init(false, rsaPrivateKey);
            var decryptedData = decryptEngine.ProcessBlock(encryptedData, 0, encryptedData.Length);
            Console.WriteLine($"Decrypted Data: {Encoding.UTF8.GetString(decryptedData)}");
        }

        private static X509Certificate2 GetCertificate(bool fromFile)
        {
            if (fromFile)
            {
                const string certificatePath = "./files/certificate.pfx";
                var certificate = new X509Certificate2(certificatePath, "", X509KeyStorageFlags.Exportable);
                return certificate;
            }

            // from the store
            using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                const string subjectName = "api.example.com";
                store.Open(OpenFlags.ReadOnly);
                var certs = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, false);
                return certs.Count > 0 ? certs[0] : null;
            }
        }
    }
}