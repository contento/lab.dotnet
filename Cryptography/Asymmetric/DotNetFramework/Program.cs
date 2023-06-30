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
        const string certificatePath = "./files/certificate.pfx";
        const string subjectName = "api.example.com";
        const string digestAlgorithm = "SHA-256";

        static void Main()
        {
            const string plainText = "Hello World!";
            Console.WriteLine($"Plain text:\n{plainText}");

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // When using the store make sure you mark the certificate as exportable.
            // (BouncyCastle needs it) !!!
            var cert = GetCertificate(fromFile: false);
            if (cert is null)
            {
                throw new InvalidOperationException($"Certificate with subject name {subjectName}");
            }

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // DO NOT USE ASYMMETRIC ENCRYPTION FOR LARGE DATA
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // Encrypt
            var encryptEngine = new OaepEncoding(new RsaEngine(), DigestUtilities.GetDigest(digestAlgorithm));
            var rsaPublicKey = DotNetUtilities.GetRsaPublicKey(cert.PublicKey.Key as RSACryptoServiceProvider);
            encryptEngine.Init(true, rsaPublicKey);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedData = encryptEngine.ProcessBlock(plainTextBytes, 0, plainTextBytes.Length);
            Console.WriteLine($"\nEncrypted Data:\n{Convert.ToBase64String(encryptedData)}");

            // Decrypt
            var rsaPrivateKey = DotNetUtilities.GetRsaKeyPair(cert.PrivateKey as RSACryptoServiceProvider).Private;
            var decryptEngine = new OaepEncoding(new RsaEngine(), DigestUtilities.GetDigest(digestAlgorithm));
            decryptEngine.Init(false, rsaPrivateKey);
            var decryptedData = decryptEngine.ProcessBlock(encryptedData, 0, encryptedData.Length);
            Console.WriteLine($"\nDecrypted Data:\n{Encoding.UTF8.GetString(decryptedData)}");
        }

        static X509Certificate2 GetCertificate(bool fromFile)
        {
            if (fromFile)
            {
                var certificate = new X509Certificate2(certificatePath, "", X509KeyStorageFlags.Exportable);
                return certificate;
            }

            // from the store
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            using (store)
            {
                store.Open(OpenFlags.ReadOnly);
                var certs = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, false);
                return certs.Count > 0 ? certs[0] : null;
            }
        }
    }
}