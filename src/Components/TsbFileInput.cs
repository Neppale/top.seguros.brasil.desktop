using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Top_Seguros_Brasil_Desktop.src.font;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbFileInput : UserControl
    {
        TsbFont tsbFont = new TsbFont();
        public TsbFileInput()
        {
            this.BackColor = TsbColor.surface;
            this.ControlAdded += (sender, e) =>
            {
                title.Font = new Font(TsbFont.TsbFonts.Families[1], 15, FontStyle.Bold);
                title.ForeColor = TsbColor.secondaryDarkest;
                title.AutoSize = true;
                instruction.Font = new Font(TsbFont.TsbFonts.Families[3], 10, FontStyle.Regular);
                instruction.ForeColor = TsbColor.neutralGray;

            };

            this.AllowDrop = true;
            this.Cursor = Cursors.Hand;

            InitializeComponent();
        }        
    }
}
