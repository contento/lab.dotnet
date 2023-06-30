namespace CryptographyTester
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            textBoxSecret = new TextBox();
            label4 = new Label();
            textBoxCipherSecret = new TextBox();
            label3 = new Label();
            linkLabel1 = new LinkLabel();
            buttonDecrypt = new Button();
            checkBoxBouncy = new CheckBox();
            buttonEncrypt = new Button();
            textBoxCipherText = new TextBox();
            label2 = new Label();
            textBoxClearText = new TextBox();
            label1 = new Label();
            checkBoxRandomSecret = new CheckBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(checkBoxRandomSecret);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(textBoxSecret);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(textBoxCipherSecret);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(linkLabel1);
            panel1.Controls.Add(buttonDecrypt);
            panel1.Controls.Add(checkBoxBouncy);
            panel1.Controls.Add(buttonEncrypt);
            panel1.Controls.Add(textBoxCipherText);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBoxClearText);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(17, 22);
            panel1.Name = "panel1";
            panel1.Size = new Size(1329, 946);
            panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Location = new Point(30, 275);
            groupBox1.Margin = new Padding(0);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(0);
            groupBox1.Size = new Size(1265, 4);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            // 
            // textBoxSecret
            // 
            textBoxSecret.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxSecret.Location = new Point(30, 157);
            textBoxSecret.Name = "textBoxSecret";
            textBoxSecret.ReadOnly = true;
            textBoxSecret.Size = new Size(1273, 39);
            textBoxSecret.TabIndex = 11;
            textBoxSecret.KeyDown += textBoxSecret_KeyDown;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(30, 120);
            label4.Name = "label4";
            label4.Size = new Size(91, 32);
            label4.TabIndex = 10;
            label4.Text = "Secret:";
            // 
            // textBoxCipherSecret
            // 
            textBoxCipherSecret.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxCipherSecret.Location = new Point(30, 332);
            textBoxCipherSecret.Name = "textBoxCipherSecret";
            textBoxCipherSecret.Size = new Size(1273, 39);
            textBoxCipherSecret.TabIndex = 9;
            textBoxCipherSecret.KeyDown += textBoxCipherSecret_KeyDown;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 291);
            label3.Name = "label3";
            label3.Size = new Size(473, 32);
            label3.TabIndex = 8;
            label3.Text = "Cipher Secret (Encrypted with Asymmetric):";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(756, 218);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(428, 32);
            linkLabel1.TabIndex = 7;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "(https://www.bouncycastle.org/csharp/)";
            // 
            // buttonDecrypt
            // 
            buttonDecrypt.Location = new Point(200, 211);
            buttonDecrypt.Name = "buttonDecrypt";
            buttonDecrypt.Size = new Size(150, 46);
            buttonDecrypt.TabIndex = 6;
            buttonDecrypt.Text = "[↑] Decrypt";
            buttonDecrypt.UseVisualStyleBackColor = true;
            buttonDecrypt.Click += buttonDecrypt_Click;
            // 
            // checkBoxBouncy
            // 
            checkBoxBouncy.AutoSize = true;
            checkBoxBouncy.Checked = true;
            checkBoxBouncy.CheckState = CheckState.Checked;
            checkBoxBouncy.Location = new Point(369, 217);
            checkBoxBouncy.Name = "checkBoxBouncy";
            checkBoxBouncy.Size = new Size(397, 36);
            checkBoxBouncy.TabIndex = 5;
            checkBoxBouncy.Text = "Use 'BouncyCastle.Cryptography'";
            checkBoxBouncy.UseVisualStyleBackColor = true;
            // 
            // buttonEncrypt
            // 
            buttonEncrypt.Location = new Point(30, 211);
            buttonEncrypt.Name = "buttonEncrypt";
            buttonEncrypt.Size = new Size(150, 46);
            buttonEncrypt.TabIndex = 4;
            buttonEncrypt.Text = "[↓] Encrypt";
            buttonEncrypt.UseVisualStyleBackColor = true;
            buttonEncrypt.Click += buttonEncrypt_Click;
            // 
            // textBoxCipherText
            // 
            textBoxCipherText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxCipherText.Location = new Point(30, 437);
            textBoxCipherText.Multiline = true;
            textBoxCipherText.Name = "textBoxCipherText";
            textBoxCipherText.Size = new Size(1273, 476);
            textBoxCipherText.TabIndex = 3;
            textBoxCipherText.KeyDown += TextBoxCipherTextKeyDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 397);
            label2.Name = "label2";
            label2.Size = new Size(437, 32);
            label2.TabIndex = 2;
            label2.Text = "Cipher Text (Encrypted with Symmetric):";
            // 
            // textBoxClearText
            // 
            textBoxClearText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxClearText.Location = new Point(30, 63);
            textBoxClearText.Name = "textBoxClearText";
            textBoxClearText.Size = new Size(1273, 39);
            textBoxClearText.TabIndex = 1;
            textBoxClearText.Text = "Hello world !";
            textBoxClearText.KeyDown += TextBoxClearTextKeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(30, 28);
            label1.Name = "label1";
            label1.Size = new Size(133, 32);
            label1.TabIndex = 0;
            label1.Text = "Clear Text:";
            // 
            // checkBoxRandomSecret
            // 
            checkBoxRandomSecret.AutoSize = true;
            checkBoxRandomSecret.Checked = true;
            checkBoxRandomSecret.CheckState = CheckState.Checked;
            checkBoxRandomSecret.Enabled = false;
            checkBoxRandomSecret.Location = new Point(127, 120);
            checkBoxRandomSecret.Name = "checkBoxRandomSecret";
            checkBoxRandomSecret.Size = new Size(397, 36);
            checkBoxRandomSecret.TabIndex = 13;
            checkBoxRandomSecret.Text = "Use 'BouncyCastle.Cryptography'";
            checkBoxRandomSecret.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1369, 980);
            Controls.Add(panel1);
            Name = "MainForm";
            Text = "Test Asymmetric";
            Load += MainForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Label label2;
        private TextBox textBoxClearText;
        private CheckBox checkBoxBouncy;
        private Button buttonEncrypt;
        private TextBox textBoxCipherText;
        private LinkLabel linkLabel1;
        private Button buttonDecrypt;
        private Label label3;
        private TextBox textBoxCipherSecret;
        private TextBox textBoxSecret;
        private Label label4;
        private GroupBox groupBox1;
        private CheckBox checkBoxRandomSecret;
    }
}