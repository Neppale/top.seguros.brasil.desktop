using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.font;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbInput : Panel
    {
        TsbFont tsbFont = new TsbFont();

        Label Label = new Label
        {

            Font = new Font(TsbFont.TsbFonts.Families[0], 12, FontStyle.Bold),
            ForeColor = TsbColor.neutral,
            AutoSize = true,
            Padding = new Padding(0, 0, 0, 16),
            Dock = DockStyle.Top
        };

        MaterialSingleLineTextField input = new MaterialSingleLineTextField
        {
            ForeColor = TsbColor.neutralGray,
            Dock = DockStyle.Top,
            Margin = new Padding(4, 32, 0, 0),
            Width = 331,
            BackColor = Color.Purple,
            AutoSize = true
        };

        Panel InputPanel = new Panel
        {
            AutoSize = true
        };

        public string Text
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

        public new bool Enabled
        {
            get
            {
                return input.Enabled;
            }
            set
            {
                input.Enabled = value;
                Label.Enabled = value;
            }
        }

        public int MaxLength
        {
            get
            {
                return input.MaxLength;
            }
            set
            {
                input.MaxLength = value;
            }

        }


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
                return input.Hint;
            }
            set
            {
                input.Hint = value;
            }
        }

        public TsbInput()
        {
            ForeColor = TsbColor.neutralGray;
            InputPanel.Height = 100;
            InputPanel.AutoSize = true;
            InputPanel.Dock = DockStyle.Bottom;

            InputPanel.Controls.Add(input);
            InputPanel.Controls.Add(Label);
            Controls.Add(InputPanel);

            InitializeComponent();
        }



        public TsbInput(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}

