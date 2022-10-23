namespace Top_Seguros_Brasil_Desktop.src.Components
{
    partial class TsbPaginationRow
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.previousButton = new System.Windows.Forms.PictureBox();
            this.nextButton = new System.Windows.Forms.PictureBox();
            this.pageNumber = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.previousButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextButton)).BeginInit();
            this.SuspendLayout();
            // 
            // previousButton
            // 
            this.previousButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.previousButton.Image = global::Top_Seguros_Brasil_Desktop.Properties.Resources.enabled_left_arrow;
            this.previousButton.Location = new System.Drawing.Point(147, 12);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(50, 28);
            this.previousButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.previousButton.TabIndex = 0;
            this.previousButton.TabStop = false;
            // 
            // nextButton
            // 
            this.nextButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.nextButton.Image = global::Top_Seguros_Brasil_Desktop.Properties.Resources.enabled_right_arrow;
            this.nextButton.Location = new System.Drawing.Point(197, 12);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(50, 28);
            this.nextButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.nextButton.TabIndex = 1;
            this.nextButton.TabStop = false;
            // 
            // pageNumber
            // 
            this.pageNumber.AutoSize = true;
            this.pageNumber.Depth = 0;
            this.pageNumber.Dock = System.Windows.Forms.DockStyle.Left;
            this.pageNumber.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pageNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pageNumber.Location = new System.Drawing.Point(24, 12);
            this.pageNumber.MouseState = MaterialSkin.MouseState.HOVER;
            this.pageNumber.Name = "pageNumber";
            this.pageNumber.Size = new System.Drawing.Size(62, 19);
            this.pageNumber.TabIndex = 2;
            this.pageNumber.Text = "Página: ";
            // 
            // TsbPaginationRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.pageNumber);
            this.Controls.Add(this.previousButton);
            this.Controls.Add(this.nextButton);
            this.Name = "TsbPaginationRow";
            this.Padding = new System.Windows.Forms.Padding(24, 12, 24, 12);
            this.Size = new System.Drawing.Size(271, 52);
            this.Load += new System.EventHandler(this.TsbPaginationRow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.previousButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox previousButton;
        private PictureBox nextButton;
        private MaterialSkin.Controls.MaterialLabel pageNumber;
    }
}
