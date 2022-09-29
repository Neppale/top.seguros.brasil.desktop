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
    public partial class BasePanel : Panel
    {
        public static string? token { get; set; }
        public string? userName { get; set; }
        public string? userType { get; set; }
      
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
            this.Controls.Add(divider);
            divider.BringToFront();

            this.Size = new Size(1654, 965);
            this.AutoSize = true;
            InitializeComponent();
            
        }

        public void Title(string title)
        {
            TsbFont tsbFont = new TsbFont();
            Label pageTitleLabel = new Label();
            pageTitleLabel.ForeColor = TsbColor.neutral;
            pageTitleLabel.Text = title;
            pageTitleLabel.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
            pageTitleLabel.Location = new Point(30, 32);
            pageTitleLabel.Font = new Font(TsbFont.TsbFonts.Families[0], 18, FontStyle.Bold);
            pageTitleLabel.AutoSize = true;

            Controls.Add(pageTitleLabel);
        }

        public void SubTitle(string title)
        {
            TsbFont tsbFont = new TsbFont();
            Label pageSubTitleLabel = new Label();
            pageSubTitleLabel.ForeColor = TsbColor.neutralGray;
            pageSubTitleLabel.Text = title;
            pageSubTitleLabel.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
            pageSubTitleLabel.Location = new Point(32, 64);
            pageSubTitleLabel.AutoSize = true;
            pageSubTitleLabel.Font = new Font(TsbFont.TsbFonts.Families[3], 10);

            Controls.Add(pageSubTitleLabel);
        }

        public BasePanel(string name, string type)
        {
            this.userName = name;
            this.userType = type;
            InitializeComponent();
        }

        public BasePanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
