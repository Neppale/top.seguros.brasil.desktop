using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Top_Seguros_Brasil_Desktop
{
    public partial class antigoTsbButton : UserControl
    {
        public antigoTsbButton()
        {
            InitializeComponent();
            button2.BackColor = Color.FromArgb(244, 84, 70);
            this.BackColor = Color.Transparent;
        }
        
        public void changeButtonText(string text)
        {
            button2.Text = text.ToUpper();
        }

        public void isClicked()
        {
            
        }
        
        public void button2_Click(object sender, EventArgs e)
        {

        

        }
    }
}
