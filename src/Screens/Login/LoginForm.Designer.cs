namespace Top_Seguros_Brasil_Desktop
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.passwordInput = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.emailInput = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonLogin = new Top_Seguros_Brasil_Desktop.ButtonTsb(this.components);
            this.illustrationLogin = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.illustrationLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // passwordInput
            // 
            this.passwordInput.Depth = 0;
            this.passwordInput.Hint = "";
            this.passwordInput.Location = new System.Drawing.Point(541, 496);
            this.passwordInput.MaxLength = 32767;
            this.passwordInput.MouseState = MaterialSkin.MouseState.HOVER;
            this.passwordInput.Name = "passwordInput";
            this.passwordInput.PasswordChar = '\0';
            this.passwordInput.SelectedText = "";
            this.passwordInput.SelectionLength = 0;
            this.passwordInput.SelectionStart = 0;
            this.passwordInput.Size = new System.Drawing.Size(343, 23);
            this.passwordInput.TabIndex = 4;
            this.passwordInput.TabStop = false;
            this.passwordInput.Text = "Senha123-";
            this.passwordInput.UseSystemPasswordChar = false;
            // 
            // emailInput
            // 
            this.emailInput.Depth = 0;
            this.emailInput.Hint = "";
            this.emailInput.Location = new System.Drawing.Point(541, 417);
            this.emailInput.MaxLength = 32767;
            this.emailInput.MouseState = MaterialSkin.MouseState.HOVER;
            this.emailInput.Name = "emailInput";
            this.emailInput.PasswordChar = '\0';
            this.emailInput.SelectedText = "";
            this.emailInput.SelectionLength = 0;
            this.emailInput.SelectionStart = 0;
            this.emailInput.Size = new System.Drawing.Size(343, 23);
            this.emailInput.TabIndex = 3;
            this.emailInput.TabStop = false;
            this.emailInput.Text = "jonathan.santos@topseguros.br";
            this.emailInput.UseSystemPasswordChar = false;
            this.emailInput.Click += new System.EventHandler(this.emailInput_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(267, 44);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(84)))), ((int)(((byte)(70)))));
            this.buttonLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonLogin.Location = new System.Drawing.Point(555, 760);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(331, 56);
            this.buttonLogin.TabIndex = 8;
            this.buttonLogin.Text = "funcionou mesmo?";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // illustrationLogin
            // 
            this.illustrationLogin.Image = global::Top_Seguros_Brasil_Desktop.Properties.Resources._34__newsletter_2;
            this.illustrationLogin.InitialImage = global::Top_Seguros_Brasil_Desktop.Properties.Resources._34__newsletter_2;
            this.illustrationLogin.Location = new System.Drawing.Point(531, 112);
            this.illustrationLogin.Name = "illustrationLogin";
            this.illustrationLogin.Size = new System.Drawing.Size(256, 256);
            this.illustrationLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.illustrationLogin.TabIndex = 9;
            this.illustrationLogin.TabStop = false;
            this.illustrationLogin.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1420, 981);
            this.Controls.Add(this.illustrationLogin);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.passwordInput);
            this.Controls.Add(this.emailInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1440, 1024);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.illustrationLogin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialSingleLineTextField passwordInput;
        private MaterialSkin.Controls.MaterialSingleLineTextField emailInput;
        private PictureBox pictureBox1;
        private antigoTsbButton tsbButton1;
        private ButtonTsb buttonLogin;
        private PictureBox illustrationLogin;
    }
}