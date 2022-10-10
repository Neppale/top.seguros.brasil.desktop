using Top_Seguros_Brasil_Desktop.src.font;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TitleBox : UserControl
    {
        public string titleText
        {
            get
            {
                return title.Text;
            }
            set
            {
                title.Text = value;
            }
        }

        public int FontSize
        {

            set
            {

                title.Font = new Font(TsbFont.TsbFonts.Families[0], value, FontStyle.Bold);
                
            }
        }

        public string subtitleText
        {
            get
            {
                return subTitle.Text;
            }
            set
            {
                subTitle.Text = value;
            }
        }

        public event EventHandler GoBack
        {
            add
            {
                pictureBox1.Click += value;
            }
            remove
            {
                pictureBox1.Click -= value;
            }
        }

        public new bool GoBackable
        {
            get
            {
                return pictureBox1.Visible;
            }
            set
            {
                pictureBox1.Visible = value;
            }
        }

        public TitleBox()
        {

            ControlAdded += (sender, e) =>
            {
                title.Font = new Font(TsbFont.TsbFonts.Families[0], 18, FontStyle.Bold);
                title.ForeColor = TsbColor.neutral;
                title.AutoSize = true;

                subTitle.Font = new Font(TsbFont.TsbFonts.Families[0], 10, FontStyle.Regular);
                subTitle.ForeColor = TsbColor.neutralGray;
                subTitle.Width = subTitle.Width - this.Width;
                subTitle.Height = 24;
                subTitle.AutoSize = true;
                pictureBox1.Cursor = Cursors.Hand;
            };

            InitializeComponent();
        }

        private void TitleBox_Load(object sender, EventArgs e)
        {

        }

    }


}

