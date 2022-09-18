using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.Models;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class appBar : Panel
    {
        Label userName = new Label();
        Label userType = new Label();
        Panel userBox = new Panel();
        PictureBox userIcon = new PictureBox();
        PictureBox notificationIcon = new PictureBox();



        public appBar(string currentUserName, string currentUserType)
        {
            

            this.Dock = DockStyle.Top;
            this.Height = 115;
            this.Padding = new Padding(32);

            string[] nameAka = currentUserName.Split();


            if (currentUserName != null)
            {
                
                userName.Text = nameAka[0] + " " + nameAka[1];
            } else
            {
                userName.Text = "John Due";
            }

            if(currentUserType != null)
            {
                userType.Text = currentUserType;
            }
            else
            {
                userType.Text = "Jobless";
            }

            userName.Font = new Font("Arial", 12, FontStyle.Bold);
            userType.Font = new Font("Arial", 12, FontStyle.Regular);
           

            userName.Location = new Point(0, 0);
            userName.Width = userBox.Width;

            userType.Location = new Point(0, 27);
            userName.ForeColor = TsbColor.secondaryDarkest;
          

            userType.ForeColor = TsbColor.neutralGray;


            userIcon.Dock = DockStyle.Right;
            userIcon.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\src\\img\\icon\\appbar\\userIcon.png");
            userIcon.SizeMode = PictureBoxSizeMode.Zoom;
            userIcon.Size = new Size(50, 50);


            notificationIcon.Dock = DockStyle.Right;
            notificationIcon.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\src\\img\\icon\\appbar\\notification.png");
            notificationIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            notificationIcon.Size = new Size(64, 32);
            


            userBox.Controls.Add(userName);
            userBox.Controls.Add(userType);
           

            userBox.Dock = DockStyle.Right;
            this.Controls.Add(notificationIcon);
            this.Controls.Add(userIcon);
            this.Controls.Add(userBox);

            InitializeComponent();

        }

        

        public appBar(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
