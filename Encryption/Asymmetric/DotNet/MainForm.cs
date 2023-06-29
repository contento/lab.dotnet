using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DotNet.TestAsymmetric
{
    public partial class MainForm : Form
    {
        private static X509Certificate2? _cert;
        private const string SubjectName = "api.example.com";

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

            var secret = textBoxCipherSecret.Text;
            var cipherBytes = AsymmetricEncrypt(secret);
            textBoxCipherSecret.Text = Convert.ToBase64String(cipherBytes ?? throw new InvalidOperationException());

            var clearText = textBoxClearText.Text;
        }

        private static byte[]? AsymmetricEncrypt(string plainText)
        {
            // DO NOT USE ASYMMETRIC ENCRYPTION FOR LARGE DATA !!!
            var rsaPublicKey = (_cert ?? throw new InvalidOperationException()).GetRSAPublicKey();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var cipherBytes = rsaPublicKey?.Encrypt(plainTextBytes, RSAEncryptionPadding.OaepSHA256);
            return cipherBytes;
        }

        private void DoDecrypt()
        {
            textBoxClearText.Clear();
            textBoxSecret.Clear();

            var cipherBytes = Convert.FromBase64String(textBoxCipherSecret.Text);
            var clearBytes = AsymmetricDecrypt(cipherBytes);
            textBoxClearText.Text = Encoding.UTF8.GetString(clearBytes ?? throw new InvalidOperationException());
        }

        private static byte[]? AsymmetricDecrypt(byte[] cipherBytes)
        {
            var rsaPrivateKey = (_cert ?? throw new InvalidOperationException()).GetRSAPrivateKey();
            var clearBytes = rsaPrivateKey?.Decrypt(cipherBytes, RSAEncryptionPadding.OaepSHA256);
            return clearBytes;
        }

        private static X509Certificate2? GetCertificate()
        {
            using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            var certs = store.Certificates.Find(X509FindType.FindBySubjectName, SubjectName, false);
            return certs.Count > 0 ? certs[0] : null;
        }
    }
}