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
        public string userName { get; set; }
        public string userType { get; set; }

        public BaseForm()
        {
            this.BackColor = TsbColor.background;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;

            this.AutoSize = true;
            this.FormClosed += (sender, e) =>
            {
                Application.Exit();
            };

            this.MaximizeBox = true;
            InitializeComponent();
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
