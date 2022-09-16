using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.Screens.Components;

namespace Top_Seguros_Brasil_Desktop.src.Screens.BaseForm
{
    public partial class BaseForm : Form
    {
        protected string token { get; set; }
        public BaseForm()
        {
            InitializeComponent();
            this.Size = new Size(1440, 1024);
            this.BackColor = TsbColor.background;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
            SideNav sideNav;
            this.Controls.Add(new SideNav());
            this.MaximizeBox = false;
        }
        private void BaseForm_Load(object sender, EventArgs e)
        {

        }

        public BaseForm(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
    }
}
