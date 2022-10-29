using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.font;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbMaskedTextBox : Panel
    {

        TsbFont tsbFont = new TsbFont();

        Label Label = new Label
        {
            Font = new Font(TsbFont.TsbFonts.Families[3], 12, FontStyle.Regular),
            ForeColor = TsbColor.neutral,
            AutoSize = true,
            Padding = new Padding(0, 0, 0, 16),
            Dock = DockStyle.Top
        };

        public MaskedTextBox input = new MaskedTextBox
        {
            BorderStyle = BorderStyle.None,
            ForeColor = TsbColor.neutralGray,
            PromptChar = ' ',
            Font = MaterialSkinManager.Instance.ROBOTO_MEDIUM_11,
            InsertKeyMode = InsertKeyMode.Overwrite,
            HidePromptOnLeave = true,
            BackColor = TsbColor.background,
            Dock = DockStyle.Top,
            Margin = new Padding(4, 32, 0, 0),
            Width = 331,
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
                input.ForeColor = TsbColor.neutral;
                input.Text = value;
            }
        }

        public string Mask
        {
            get
            {
                return input.Mask;
            }
            set
            {
                input.Mask = value;
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
                return input.Text;
            }
            set
            {
                input.ForeColor = TsbColor.neutralGray;
                input.Text = value;
            }
        }

        public bool ToUper
        {
            set
            {
                input.TextChanged += (sender, e) =>
                {
                    input.Text = input.Text.ToUpper();
                };
            }
        }


        public int TextLength
        {
            get
            {
                return input.TextLength;
            }
        }

        public new event EventHandler TextChanged
        {
            add
            {
                input.TextChanged += value;
            }
            remove
            {
                input.TextChanged -= value;
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

        public bool Currency
        {
            set
            {
                if (value)
                {
                    input.TextChanged += currency_TextChanged;
                    return;
                }
                return;
            }
        }

        public bool NewValue
        {
            set
            {
                if (value)
                {
                    input.Click += (sender, e) =>
                    {
                        input.Text = "";
                        input.Select(0, 0);
                    };
                }
            }
        }

        private void currency_TextChanged(object sender, EventArgs e)
        {
            input.Text = string.Format("{$\\d{9}.00}");
        }

        public TsbMaskedTextBox()
        {
            input.Culture = new System.Globalization.CultureInfo("pt-BR");
            ForeColor = TsbColor.neutralGray;
            InputPanel.Height = 100;
            InputPanel.AutoSize = true;
            InputPanel.Dock = DockStyle.Bottom;

            InputPanel.Controls.Add(input);
            InputPanel.Controls.Add(Label);

            Panel divider = new Panel();
            divider.Height = 1;
            divider.BackColor = Color.FromArgb(66, 0, 0, 0);
            divider.Dock = DockStyle.Bottom;
            input.Controls.Add(divider);

            input.Click += (sender, e) =>
            {

                if (!input.MaskFull)
                {
                    input.Select(0, 0);
                    input.ForeColor = TsbColor.neutral;
                    input.Text = "";
                }

                divider.Height = 2;
                divider.BackColor = Color.FromArgb(222, 0, 0, 0);
            };

            input.LostFocus += (sender, e) =>
            {
                divider.Height = 1;
                divider.BackColor = Color.FromArgb(66, 0, 0, 0);
            };

            input.Enter += (sender, e) =>
            {
                input.ForeColor = Color.FromArgb(222, 0, 0, 0);
            };

            Controls.Add(InputPanel);

            InitializeComponent();
        }

        public TsbMaskedTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
