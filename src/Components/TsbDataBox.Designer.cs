namespace Top_Seguros_Brasil_Desktop.src.Components
{
    partial class TsbDataBox
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
            this.Title = new System.Windows.Forms.Label();
            this.Subtitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.Title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Title.Location = new System.Drawing.Point(0, 0);
            this.Title.Margin = new System.Windows.Forms.Padding(0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(52, 21);
            this.Title.TabIndex = 0;
            this.Title.Text = "label1";
            // 
            // Subtitle
            // 
            this.Subtitle.AutoSize = true;
            this.Subtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.Subtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Subtitle.Location = new System.Drawing.Point(0, 21);
            this.Subtitle.Margin = new System.Windows.Forms.Padding(0);
            this.Subtitle.Name = "Subtitle";
            this.Subtitle.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.Subtitle.Size = new System.Drawing.Size(45, 23);
            this.Subtitle.TabIndex = 1;
            this.Subtitle.Text = "label1";
            // 
            // TsbDataBox
            // 
            this.Controls.Add(this.Subtitle);
            this.Controls.Add(this.Title);
            this.Name = "TsbDataBox";
            this.Size = new System.Drawing.Size(139, 48);
            this.Load += new System.EventHandler(this.TsbDataBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label Title;
        private Label Subtitle;
    }
}
