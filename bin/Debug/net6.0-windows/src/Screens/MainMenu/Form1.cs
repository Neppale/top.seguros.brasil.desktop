using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

// Import the Material Skin
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.VisualBasic.ApplicationServices;
using System.Security.Principal;

namespace Top_Seguros_Brasil_Desktop
{
    

    public partial class Home : Form
    {
        Welcome Welcome = new Welcome();
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

            //Se hide() for igual a true, cor do item muda para laranja.

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

            try
            {
                string imgPath = Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\users_orange.png";
                Bitmap bitmap = new Bitmap(imgPath);
                usersPage.Image = bitmap;

                usersPage.ForeColor = Color.FromArgb(244, 84, 70);
                Welcome.Show();
                
            }
            catch (Exception x)
            {

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void policyRequestPage_Click(object sender, EventArgs e)
        {
            try
            {
                string imgPath = Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\policy_orange.png";
                Bitmap bitmap = new Bitmap(imgPath);
                policyRequestPage.Image = bitmap;

                policyRequestPage.ForeColor = Color.FromArgb(244, 84, 70);
            }
            catch (Exception x)
            {

            }
        }

        private void customerPage_Click(object sender, EventArgs e)
        {
            try
            {
                string imgPath = Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\customer_orange.png";
                Bitmap bitmap = new Bitmap(imgPath);
                customerPage.Image = bitmap;

                customerPage.ForeColor = Color.FromArgb(244, 84, 70);
            }
            catch (Exception x)
            {

            }
        }

        private void incidentPage_Click(object sender, EventArgs e)
        {
            try
            {
                string imgPath = Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\incident_orange.png";
                Bitmap bitmap = new Bitmap(imgPath);
                incidentPage.Image = bitmap;

                incidentPage.ForeColor = Color.FromArgb(244, 84, 70);
            }
            catch (Exception x)
            {

            }
        }

        private void coveryPage_Click(object sender, EventArgs e)
        {
            try
            {
                string imgPath = Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\covery_orange.png";
                Bitmap bitmap = new Bitmap(imgPath);
                coveryPage.Image = bitmap;

                coveryPage.ForeColor = Color.FromArgb(244, 84, 70);
            }
            catch (Exception x)
            {

            }
        }

        private void vehiclesPage_Click(object sender, EventArgs e)
        {
            try
            {
                string imgPath = Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\vehicle_orange.png";
                Bitmap bitmap = new Bitmap(imgPath);
                vehiclesPage.Image = bitmap;

                vehiclesPage.ForeColor = Color.FromArgb(244, 84, 70);
            }
            catch (Exception x)
            {

            }
        }

        private void outsourcedPage_Click(object sender, EventArgs e)
        {
            try
            {
                string imgPath = Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\out_orange.png";
                Bitmap bitmap = new Bitmap(imgPath);
                outsourcedPage.Image = bitmap;

                outsourcedPage.ForeColor = Color.FromArgb(244, 84, 70);
            }
            catch (Exception x)
            {

            }
        }

        private void welcome1_Load(object sender, EventArgs e)
        {
            welcome1.Hide();

            string imgPath = Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\users_orange.png";
            Bitmap bitmap = new Bitmap(imgPath);
            usersPage.Image = bitmap;

            usersPage.ForeColor = Color.FromArgb(244, 84, 70);
        }

        private void login1_Load(object sender, EventArgs e)
        {
            
        }
    }
}