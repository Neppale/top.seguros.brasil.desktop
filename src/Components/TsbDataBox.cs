using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Top_Seguros_Brasil_Desktop.src.font;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbDataBox : UserControl
    {

        public string titleText
        {
            get
            {
                return Title.Text;
            }
            set
            {
                Title.Text = value;
            }
        }

        public int FontSize
        {

            set
            {

                Title.Font = new Font(TsbFont.TsbFonts.Families[0], value, FontStyle.Bold);

            }
        }

        public string subtitleText
        {
            get
            {
                return Subtitle.Text;
            }
            set
            {
                Subtitle.Text = value;
            }
        }

        public TsbDataBox()
        {
            ControlAdded += (sender, e) =>
            {
                Title.Font = new Font(TsbFont.TsbFonts.Families[0], 12, FontStyle.Bold);
                Title.ForeColor = TsbColor.neutral;
                Title.AutoSize = true;


                Subtitle.Font = new Font(TsbFont.TsbFonts.Families[0], 10, FontStyle.Regular);
                Subtitle.ForeColor = TsbColor.neutralGray;
                Subtitle.Width = Subtitle.Width - this.Width;
                Subtitle.AutoSize = true;
            };

            InitializeComponent();
        }

        //public TsbDataBox(IContainer container)
        //{
        //    container.Add(this);

        //    InitializeComponent();
        //}

        private void TsbDataBox_Load(object sender, EventArgs e)
        {

        }
    }
}
