

namespace Top_Seguros_Brasil_Desktop
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.materialContextMenuStrip1 = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.navMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.navItens = new System.Windows.Forms.Panel();
            this.outsourcedPage = new System.Windows.Forms.Button();
            this.vehiclesPage = new System.Windows.Forms.Button();
            this.coveryPage = new System.Windows.Forms.Button();
            this.incidentPage = new System.Windows.Forms.Button();
            this.customerPage = new System.Windows.Forms.Button();
            this.policyRequestPage = new System.Windows.Forms.Button();
            this.usersPage = new System.Windows.Forms.Button();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ManagementStage = new Top_Seguros_Brasil_Desktop.Login();
            this.navMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.navItens.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialContextMenuStrip1
            // 
            this.materialContextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialContextMenuStrip1.Depth = 0;
            this.materialContextMenuStrip1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialContextMenuStrip1.Name = "materialContextMenuStrip1";
            this.materialContextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // navMenu
            // 
            this.navMenu.BackColor = System.Drawing.Color.White;
            this.navMenu.Controls.Add(this.pictureBox1);
            this.navMenu.Controls.Add(this.navItens);
            this.navMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.navMenu.Location = new System.Drawing.Point(0, 0);
            this.navMenu.Margin = new System.Windows.Forms.Padding(0);
            this.navMenu.Name = "navMenu";
            this.navMenu.Padding = new System.Windows.Forms.Padding(0, 32, 0, 32);
            this.navMenu.Size = new System.Drawing.Size(267, 981);
            this.navMenu.TabIndex = 1;
            this.navMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.navMenu_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 32);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(267, 44);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // navItens
            // 
            this.navItens.BackColor = System.Drawing.Color.Transparent;
            this.navItens.Controls.Add(this.outsourcedPage);
            this.navItens.Controls.Add(this.vehiclesPage);
            this.navItens.Controls.Add(this.coveryPage);
            this.navItens.Controls.Add(this.incidentPage);
            this.navItens.Controls.Add(this.customerPage);
            this.navItens.Controls.Add(this.policyRequestPage);
            this.navItens.Controls.Add(this.usersPage);
            this.navItens.Dock = System.Windows.Forms.DockStyle.Top;
            this.navItens.Location = new System.Drawing.Point(0, 341);
            this.navItens.Margin = new System.Windows.Forms.Padding(0, 265, 0, 0);
            this.navItens.Name = "navItens";
            this.navItens.Size = new System.Drawing.Size(267, 800);
            this.navItens.TabIndex = 1;
            // 
            // outsourcedPage
            // 
            this.outsourcedPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.outsourcedPage.Dock = System.Windows.Forms.DockStyle.Top;
            this.outsourcedPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.outsourcedPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.outsourcedPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.outsourcedPage.ForeColor = System.Drawing.Color.DimGray;
            this.outsourcedPage.Image = global::Top_Seguros_Brasil_Desktop.Properties.Resources.engineering_FILL0_wght400_GRAD0_opsz48_1;
            this.outsourcedPage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.outsourcedPage.Location = new System.Drawing.Point(0, 264);
            this.outsourcedPage.Margin = new System.Windows.Forms.Padding(0);
            this.outsourcedPage.Name = "outsourcedPage";
            this.outsourcedPage.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.outsourcedPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.outsourcedPage.Size = new System.Drawing.Size(267, 44);
            this.outsourcedPage.TabIndex = 6;
            this.outsourcedPage.Text = "         Terceirizados";
            this.outsourcedPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.outsourcedPage.UseVisualStyleBackColor = true;
            this.outsourcedPage.Click += new System.EventHandler(this.outsourcedPage_Click);
            // 
            // vehiclesPage
            // 
            this.vehiclesPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.vehiclesPage.Dock = System.Windows.Forms.DockStyle.Top;
            this.vehiclesPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.vehiclesPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vehiclesPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.vehiclesPage.ForeColor = System.Drawing.Color.DimGray;
            this.vehiclesPage.Image = global::Top_Seguros_Brasil_Desktop.Properties.Resources.directions_car_FILL0_wght400_GRAD0_opsz48_1;
            this.vehiclesPage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.vehiclesPage.Location = new System.Drawing.Point(0, 220);
            this.vehiclesPage.Margin = new System.Windows.Forms.Padding(0);
            this.vehiclesPage.Name = "vehiclesPage";
            this.vehiclesPage.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.vehiclesPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.vehiclesPage.Size = new System.Drawing.Size(267, 44);
            this.vehiclesPage.TabIndex = 5;
            this.vehiclesPage.Text = "         Veículos";
            this.vehiclesPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.vehiclesPage.UseVisualStyleBackColor = true;
            this.vehiclesPage.Click += new System.EventHandler(this.vehiclesPage_Click);
            // 
            // coveryPage
            // 
            this.coveryPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.coveryPage.Dock = System.Windows.Forms.DockStyle.Top;
            this.coveryPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.coveryPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.coveryPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.coveryPage.ForeColor = System.Drawing.Color.DimGray;
            this.coveryPage.Image = global::Top_Seguros_Brasil_Desktop.Properties.Resources.health_and_safety_FILL0_wght400_GRAD0_opsz48_1;
            this.coveryPage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.coveryPage.Location = new System.Drawing.Point(0, 176);
            this.coveryPage.Margin = new System.Windows.Forms.Padding(0);
            this.coveryPage.Name = "coveryPage";
            this.coveryPage.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.coveryPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.coveryPage.Size = new System.Drawing.Size(267, 44);
            this.coveryPage.TabIndex = 4;
            this.coveryPage.Text = "         Coberturas";
            this.coveryPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.coveryPage.UseVisualStyleBackColor = true;
            this.coveryPage.Click += new System.EventHandler(this.coveryPage_Click);
            // 
            // incidentPage
            // 
            this.incidentPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.incidentPage.Dock = System.Windows.Forms.DockStyle.Top;
            this.incidentPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.incidentPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.incidentPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.incidentPage.ForeColor = System.Drawing.Color.DimGray;
            this.incidentPage.Image = global::Top_Seguros_Brasil_Desktop.Properties.Resources.campaign_FILL0_wght400_GRAD0_opsz48_1;
            this.incidentPage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.incidentPage.Location = new System.Drawing.Point(0, 132);
            this.incidentPage.Margin = new System.Windows.Forms.Padding(0);
            this.incidentPage.Name = "incidentPage";
            this.incidentPage.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.incidentPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.incidentPage.Size = new System.Drawing.Size(267, 44);
            this.incidentPage.TabIndex = 3;
            this.incidentPage.Text = "         Ocorrências";
            this.incidentPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.incidentPage.UseVisualStyleBackColor = true;
            this.incidentPage.Click += new System.EventHandler(this.incidentPage_Click);
            // 
            // customerPage
            // 
            this.customerPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customerPage.Dock = System.Windows.Forms.DockStyle.Top;
            this.customerPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customerPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customerPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.customerPage.ForeColor = System.Drawing.Color.DimGray;
            this.customerPage.Image = global::Top_Seguros_Brasil_Desktop.Properties.Resources.support_agent_FILL0_wght400_GRAD0_opsz48_1;
            this.customerPage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.customerPage.Location = new System.Drawing.Point(0, 88);
            this.customerPage.Margin = new System.Windows.Forms.Padding(0);
            this.customerPage.Name = "customerPage";
            this.customerPage.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.customerPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.customerPage.Size = new System.Drawing.Size(267, 44);
            this.customerPage.TabIndex = 2;
            this.customerPage.Text = "         Clientes";
            this.customerPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.customerPage.UseVisualStyleBackColor = true;
            this.customerPage.Click += new System.EventHandler(this.customerPage_Click);
            // 
            // policyRequestPage
            // 
            this.policyRequestPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.policyRequestPage.Dock = System.Windows.Forms.DockStyle.Top;
            this.policyRequestPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.policyRequestPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.policyRequestPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.policyRequestPage.ForeColor = System.Drawing.Color.DimGray;
            this.policyRequestPage.Image = global::Top_Seguros_Brasil_Desktop.Properties.Resources.task_FILL0_wght400_GRAD0_opsz48_1;
            this.policyRequestPage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.policyRequestPage.Location = new System.Drawing.Point(0, 44);
            this.policyRequestPage.Margin = new System.Windows.Forms.Padding(0);
            this.policyRequestPage.Name = "policyRequestPage";
            this.policyRequestPage.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.policyRequestPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.policyRequestPage.Size = new System.Drawing.Size(267, 44);
            this.policyRequestPage.TabIndex = 1;
            this.policyRequestPage.Text = "         Solicitações de Apólice";
            this.policyRequestPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.policyRequestPage.UseVisualStyleBackColor = true;
            this.policyRequestPage.Click += new System.EventHandler(this.policyRequestPage_Click);
            // 
            // usersPage
            // 
            this.usersPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.usersPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.usersPage.Dock = System.Windows.Forms.DockStyle.Top;
            this.usersPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.usersPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.usersPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.usersPage.ForeColor = System.Drawing.Color.DimGray;
            this.usersPage.Image = global::Top_Seguros_Brasil_Desktop.Properties.Resources.group_add_FILL0_wght400_GRAD0_opsz48_1;
            this.usersPage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.usersPage.Location = new System.Drawing.Point(0, 0);
            this.usersPage.Margin = new System.Windows.Forms.Padding(0);
            this.usersPage.Name = "usersPage";
            this.usersPage.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this.usersPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.usersPage.Size = new System.Drawing.Size(267, 44);
            this.usersPage.TabIndex = 0;
            this.usersPage.Text = "         Usuários";
            this.usersPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.usersPage.UseVisualStyleBackColor = true;
            this.usersPage.Click += new System.EventHandler(this.usersPage_Click);
            // 
            // materialDivider1
            // 
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(267, 0);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(1, 1024);
            this.materialDivider1.TabIndex = 2;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ManagementStage);
            this.panel1.Location = new System.Drawing.Point(267, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1156, 981);
            this.panel1.TabIndex = 3;
            // 
            // ManagementStage
            // 
            this.ManagementStage.Location = new System.Drawing.Point(-276, -32);
            this.ManagementStage.MaximumSize = new System.Drawing.Size(1440, 1024);
            this.ManagementStage.Name = "ManagementStage";
            this.ManagementStage.Size = new System.Drawing.Size(1440, 1024);
            this.ManagementStage.TabIndex = 0;
            this.ManagementStage.Load += new System.EventHandler(this.login1_Load);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1420, 981);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.navMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1440, 1024);
            this.MinimizeBox = false;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Management Stage";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.navMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.navItens.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialSkin.Controls.MaterialContextMenuStrip materialContextMenuStrip1;
        private FlowLayoutPanel navMenu;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private PictureBox pictureBox1;
        private Panel navItens;
        private Button usersPage;
        private Button outsourcedPage;
        private Button vehiclesPage;
        private Button coveryPage;
        private Button incidentPage;
        private Button customerPage;
        private Button policyRequestPage;
        private Panel panel1;
        private Welcome welcome1;
        private Top_Seguros_Brasil_Desktop.Login ManagementStage;
    }
}