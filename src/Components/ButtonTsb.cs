using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.font;
using MaterialSkin;
using RestSharp;

namespace Top_Seguros_Brasil_Desktop
{
    public partial class ButtonTsbPrimary : Button
    {

        public ButtonTsbPrimary()
        {

            this.ForeColor = TsbColor.neutralWhite;
            this.BackColor = TsbColor.primary;
            this.Font = MaterialSkinManager.Instance.ROBOTO_MEDIUM_10;
            this.Size = new Size(331, 56);
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;

            //this.TextChanged += (sender, e) =>
            //{
            //    this.Text += this.Text.ToUpper();
            //};

            this.MouseEnter += (sender, e) =>
            {
                this.BackColor = TsbColor.primaryDarkest;
            };

            this.MouseLeave += (sender, e) =>
            {
                this.BackColor = TsbColor.primary;
            };

            this.EnabledChanged += (sender, e) =>
            {
                this.BackColor = TsbColor.primaryLightest;
                this.ForeColor = TsbColor.primaryLight;
            };

            InitializeComponent();

        }

        public ButtonTsbPrimary(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
    }

    public partial class ButtonTsbSecondary : Panel
    {
        TsbFont tsbFont = new TsbFont();
        Panel button = new Panel();
        Label buttonText = new Label();


        public new string Text
        {
            get
            {
                return buttonText.Text;
            }
            set
            {
                buttonText.Text = value;
            }
        }

        public new event EventHandler Click
        {
            add
            {
                buttonText.Click += value;
            }
            remove
            {
                buttonText.Click -= value;
            }
        }

        public ButtonTsbSecondary()
        {


            this.Height = 56;
            this.BackColor = TsbColor.neutralGray;
            this.Padding = new Padding(1, 1, 1, 1);

            InitializeComponent();
            button.BackColor = TsbColor.background;
            button.Dock = DockStyle.Fill;
            button.Location = new Point(1, 1);

            buttonText.Dock = DockStyle.Fill;
            buttonText.ForeColor = TsbColor.secondaryDarkest;
            buttonText.Font = new Font(TsbFont.TsbFonts.Families[0], 10, FontStyle.Bold);
            buttonText.TextAlign = ContentAlignment.MiddleCenter;
            //buttonText.Location = new Point(button.Width / 2 - buttonText.Width / 2, button.Height / 2 - buttonText.Height / 2);

            button.Controls.Add(buttonText);



            buttonText.MouseHover += (sender, e) =>
            {
                this.BackColor = TsbColor.background;
                button.BackColor = TsbColor.background;
                buttonText.ForeColor = TsbColor.secondary;
            };

            buttonText.MouseLeave += (sender, e) =>
            {
                this.BackColor = TsbColor.neutralGray;
                button.BackColor = TsbColor.background;
                buttonText.ForeColor = TsbColor.secondaryDarkest;
            };

            buttonText.Cursor = Cursors.Hand;

            this.Controls.Add(button);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {

        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (Enabled == false)
            {
                this.BackColor = TsbColor.primaryDarkest;
                this.ForeColor = Color.FromArgb(255, 129, 129);
            }
            base.OnEnabledChanged(e);
        }

        public ButtonTsbSecondary(IContainer container)
        {

            this.Text = "funcionou?";
            container.Add(this);

            InitializeComponent();
        }
    }

    public partial class ButtonTsbTertiary : Panel
    {
        TsbFont tsbFont = new TsbFont();
        Panel button = new Panel();
        Label buttonText = new Label();


        public event EventHandler Click
        {
            add
            {
                buttonText.Click += value;
            }
            remove
            {
                buttonText.Click -= value;
            }
        }


        public new string Text
        {
            get
            {
                return buttonText.Text;
            }
            set
            {
                buttonText.Text = value;
            }
        }

        public ButtonTsbTertiary()
        {


            this.Height = 56;
            //this.BackColor = TsbColor.neutralGray;
            this.Padding = new Padding(1, 1, 1, 1);

            InitializeComponent();
            //button.BackColor = TsbColor.background;
            button.Dock = DockStyle.Fill;
            button.Location = new Point(1, 1);

            buttonText.Dock = DockStyle.Fill;
            buttonText.ForeColor = TsbColor.primaryDarkest;
            buttonText.Font = new Font(TsbFont.TsbFonts.Families[0], 10, FontStyle.Bold);
            buttonText.TextAlign = ContentAlignment.MiddleCenter;
            //buttonText.Location = new Point(button.Width / 2 - buttonText.Width / 2, button.Height / 2 - buttonText.Height / 2);

            button.Controls.Add(buttonText);



            buttonText.MouseHover += (sender, e) =>
            {
                buttonText.ForeColor = TsbColor.neutralWhite;
            };

            buttonText.MouseLeave += (sender, e) =>
            {
                buttonText.ForeColor = TsbColor.primaryDarkest;
            };

            buttonText.Cursor = Cursors.Hand;

            this.Controls.Add(button);
        }



        protected override void OnVisibleChanged(EventArgs e)
        {

        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (Enabled == false)
            {
                this.BackColor = TsbColor.primaryDarkest;
                this.ForeColor = Color.FromArgb(255, 129, 129);
            }
            base.OnEnabledChanged(e);
        }

        public ButtonTsbTertiary(IContainer container)
        {

            this.Text = "funcionou?";
            container.Add(this);

            InitializeComponent();
        }
    }

}
