using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.font;
using Top_Seguros_Brasil_Desktop.src.Screens.Components;
using Top_Seguros_Brasil_Desktop.src.Screens.Management_Stage;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class BasePanel : TableLayoutPanel
    {
        public static int userId { get; set; }
        public static string? token { get; set; }
        public string? userName { get; set; }
        public string? userEmail { get; set; }
        public string? userType { get; set; }

        public Label pageTitleLabel = new Label();
        public Label pageSubTitleLabel = new Label();

        public BasePanel()
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
                    this.BringToFront();
                    this.Visible = true;
                }
            };

            Panel divider = new Panel();
            
            divider.Height = 1;
            divider.BackColor = TsbColor.neutralWhite;
            divider.Dock = DockStyle.Top;
            this.Controls.Add(divider, 0, 0);
            this.SetColumnSpan(divider, 3);



            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44));
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26));
            
            
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 31));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 27));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 21));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 39));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 56));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 392));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 194));


            this.Dock = DockStyle.Fill;
            this.AutoSize = true;

            Controls.Add(pageTitleLabel, 0, 1);
            this.SetColumnSpan(pageTitleLabel, 3);

            Controls.Add(pageSubTitleLabel, 0, 2);
            this.SetColumnSpan(pageSubTitleLabel, 3);


            InitializeComponent();

            
        }

        public void Title(string title)
        {
            TsbFont tsbFont = new TsbFont();
            pageTitleLabel.ForeColor = TsbColor.neutral;
            pageTitleLabel.Text = title;
            pageTitleLabel.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
            pageTitleLabel.Margin = new Padding(30, 0, 0, 0);
            pageTitleLabel.Location = new Point(30, 32);
            pageTitleLabel.Font = new Font(TsbFont.TsbFonts.Families[0], 18, FontStyle.Bold);
            pageTitleLabel.AutoSize = true;
            pageTitleLabel.Dock = DockStyle.Top;
            pageTitleLabel.SendToBack();
            
        }

        public void SubTitle(string title)
        {
            TsbFont tsbFont = new TsbFont();
            pageSubTitleLabel.ForeColor = TsbColor.neutralGray;
            pageSubTitleLabel.Text = title;
            pageSubTitleLabel.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
            pageSubTitleLabel.Margin = new Padding(32, 0, 0, 0);
            //pageSubTitleLabel.Location = new Point(32, 64);
            pageSubTitleLabel.AutoSize = true;
            pageSubTitleLabel.Font = new Font(TsbFont.TsbFonts.Families[3], 10);
            pageSubTitleLabel.Dock = DockStyle.Top;
            
        }

        public BasePanel(string name, string type, int id, string email)
        {
            this.userName = name;
            this.userType = type;
            this.userEmail = email;
            InitializeComponent();
        }

        public BasePanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
