using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DotNet.TestAsymmetric
{
    public partial class MainForm : Form
    {
        private static X509Certificate2? _cert;
        private const string SubjectName = "api.example.com";
        private static readonly RSAEncryptionPadding EncryptionPadding = RSAEncryptionPadding.OaepSHA256;
        private const int SymmetricKeySize = 16;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = $@"Asymmetric Encryption - Subject={SubjectName}";

            // When using the store you may need to mark the certificate as exportable (BouncyCastle).
            _cert = GetCertificate();
            if (_cert == null)
                throw new InvalidOperationException($"Certificate not found with {SubjectName}");
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            DoEncrypt();
            ;
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            DoDecrypt();
        }

        private void TextBoxClearTextKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            DoEncrypt();

            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void TextBoxCipherTextKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            DoDecrypt();

            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void textBoxCipherSecret_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            DoDecrypt();


            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void textBoxSecret_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            DoEncrypt();

            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void DoEncrypt()
        {
            textBoxCipherText.Clear();
            textBoxCipherSecret.Clear();

            // Generate a random secret.
            var secretBytes = new byte[SymmetricKeySize];
            RandomNumberGenerator.Fill(secretBytes);

            textBoxSecret.Text = Convert.ToBase64String(secretBytes);

            // Encrypt the clear text with the secret.
            var clearText = textBoxClearText.Text;
            var cipherTextBytes = SymmetricEncrypt(clearText, secretBytes);
            textBoxCipherText.Text = Convert.ToBase64String(cipherTextBytes ?? throw new InvalidOperationException());

            // Encrypt the secret with the public key.
            var cipherSecretBytes = AsymmetricEncrypt(secretBytes);
            textBoxCipherSecret.Text = Convert.ToBase64String(cipherSecretBytes ?? throw new InvalidOperationException());
        }

        private void DoDecrypt()
        {
            textBoxClearText.Clear();
            textBoxSecret.Clear();

            // Decrypt the secret with the private key.
            var cipherSecretBytes = Convert.FromBase64String(textBoxCipherSecret.Text);
            var secretBytes = AsymmetricDecrypt(cipherSecretBytes);
            var secret = Convert.ToBase64String(secretBytes ?? throw new InvalidOperationException());
            textBoxSecret.Text = secret;

            // Decrypt the cipher text with the secret.
            var cipherTextBytes = Convert.FromBase64String(textBoxCipherText.Text);
            var clearText = SymmetricDecrypt(cipherTextBytes, secretBytes);
            textBoxClearText.Text = clearText;
        }

        private static X509Certificate2? GetCertificate()
        {
            using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            var certs = store.Certificates.Find(X509FindType.FindBySubjectName, SubjectName, false);
            return certs.Count > 0 ? certs[0] : null;
        }

        private static byte[]? AsymmetricEncrypt(byte[] plainTextBytes)
        {
            // DO NOT USE ASYMMETRIC ENCRYPTION FOR LARGE DATA !!!
            var rsaPublicKey = (_cert ?? throw new InvalidOperationException()).GetRSAPublicKey();
            var cipherBytes = rsaPublicKey?.Encrypt(plainTextBytes, EncryptionPadding);
            return cipherBytes;
        }

        private static byte[]? AsymmetricDecrypt(byte[] cipherBytes)
        {
            var rsaPrivateKey = (_cert ?? throw new InvalidOperationException()).GetRSAPrivateKey();
            var clearBytes = rsaPrivateKey?.Decrypt(cipherBytes, EncryptionPadding);
            return clearBytes;
        }

        public byte[] SymmetricEncrypt(string plainText, byte[] key)
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
        public string SymmetricDecrypt(byte[] cipherData, byte[] key)
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
}