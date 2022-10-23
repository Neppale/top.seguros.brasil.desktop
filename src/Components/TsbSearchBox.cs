using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel.Contacts;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbSearchBox : UserControl
    {
        public TsbSearchBox()
        {

            InitializeComponent();
        }

        public new string Text
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                this.textBox1.Text = value;
            }
        }

        public event EventHandler SearchClick
        {
            add
            {
                this.materialFlatButton1.Click += value;
            }
            remove
            {
                this.materialFlatButton1.Click -= value;
            }
        }

        private void materialSingleLineTextField1_Click(object sender, EventArgs e)
        {

        }

        private void searchBtn_Click(object sender, EventArgs e)
        {

        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TsbSearchBox_Load(object sender, EventArgs e)
        {
            textBox1.BackColor = TsbColor.surface;
            panel1.BackColor = TsbColor.surface;
         
            
            this.Paint += (sender, e) =>
            {
                Rectangle borderRectangle = this.ClientRectangle;
                ControlPaint.DrawBorder(e.Graphics, borderRectangle,
                    TsbColor.neutralWhite, ButtonBorderStyle.Solid);

            };

            this.BackColor = TsbColor.surface;
            materialFlatButton1.BackColor = TsbColor.secondary;
            materialFlatButton1.ForeColor = TsbColor.surface;
            materialFlatButton1.Cursor = Cursors.Hand;
         
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
