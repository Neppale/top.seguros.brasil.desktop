using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Import the Material Skin
using MaterialSkin;
using MaterialSkin.Controls;

namespace Top_Seguros_Brasil_Desktop
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

            usersPage.ForeColor = Color.FromArgb(158, 158, 158);
            policyRequestPage.ForeColor = Color.FromArgb(158, 158, 158);
            customerPage.ForeColor = Color.FromArgb(158, 158, 158);
            incidentPage.ForeColor = Color.FromArgb(158, 158, 158);
            coveryPage.ForeColor = Color.FromArgb(158, 158, 158);
            vehiclesPage.ForeColor = Color.FromArgb(158, 158, 158);
            outsourcedPage.ForeColor = Color.FromArgb(158, 158, 158);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(250, 250, 250);
        }

        private void navMenu_Paint(object sender, PaintEventArgs e)
        {
            navMenu.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void usersPage_Click(object sender, EventArgs e)
        {
            usersPage.ForeColor = Color.FromArgb(244, 84, 70);
        }
    }
}