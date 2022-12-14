using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.font;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbCheck : Panel
    {

        TsbFont tsbFont = new TsbFont();

        public string LabelText
        {
            get
            {
                return Label.Text;
            }
            set
            {
                Label.Text = value;
            }
        }

        public string HintText
        {
            get
            {
                return input.Text;
            }
            set
            {
                input.Text = value;
            }
        }

        Label Label = new Label
        {
            Font = new Font(TsbFont.TsbFonts.Families[0], 12, FontStyle.Bold),
            ForeColor = TsbColor.neutral,
            AutoSize = true,
            Padding = new Padding(0, 0, 0, 16),
            Dock = DockStyle.Top
        };

        CheckBox input = new CheckBox
        {
            ForeColor = TsbColor.neutralGray,
            Dock = DockStyle.Top,
            Margin = new Padding(4, 32, 0, 0),
            Width = 331,
            BackColor = Color.Purple,
            AutoSize = true
        };

        public TsbCheck()
        {
            Controls.Add(Label);
            Controls.Add(input);

            InitializeComponent();
        }

        public TsbCheck(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
