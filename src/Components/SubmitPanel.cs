using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.font;
using Top_Seguros_Brasil_Desktop.src.Screens.Management_Stage;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class SubmitPanel : TableLayoutPanel
    {
        
        public SubmitPanel() 
        {
            this.Size = new Size(1173, 909);
            this.Visible = false;

            this.ControlAdded += (sender, e) =>
            {
                if (e.Control.FindForm() is ManagementStage)
                {
                    this.FindForm().Controls.Add(this);
                    this.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right);
                    this.Dock = DockStyle.Fill;
                    this.Visible = true;
                    this.BringToFront();
                }
            };

            this.Size = new Size(1654, 965);
            this.AutoSize = true;
            this.Dock = DockStyle.Fill;
            this.ColumnCount = 3;

            Panel divider = new Panel();

            divider.Height = 1;
            divider.BackColor = TsbColor.neutralWhite;
            divider.Dock = DockStyle.Top;
            this.Controls.Add(divider, 0, 0);
            this.SetColumnSpan(divider, 3);

            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            
            this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.RowStyles.Add(new RowStyle(SizeType.AutoSize));


            InitializeComponent();
        }

    }
}
