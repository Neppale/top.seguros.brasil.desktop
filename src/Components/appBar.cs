using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.Properties;
using Top_Seguros_Brasil_Desktop.src.Models;
using Top_Seguros_Brasil_Desktop.src.Screens.Management_Stage;

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

            this.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right);
            this.Dock = DockStyle.Top;
            this.Height = 115;


            string[] nameAka = currentUserName.Split();

            if (currentUserName != null)
            {
                userName.Text = nameAka[0] + " " + nameAka[1];
            }
            else
            {
                userName.Text = "John Due";
            }

            if (currentUserType != null)
            {
                userType.Text = currentUserType;
            }
            else
            {
                userType.Text = "Jobless";
            }

            userName.Font = new Font("Arial", 12, FontStyle.Bold);
            userType.Font = new Font("Arial", 12, FontStyle.Regular);

            userName.Location = new Point(0, 32);
            userName.Width = userBox.Width;

            userType.Location = new Point(0, 52);
            userName.ForeColor = TsbColor.secondaryDarkest;
            userType.ForeColor = TsbColor.neutralGray;

            userIcon.Dock = DockStyle.Right;
            userIcon.Image = Resources.userIcon;
            userIcon.SizeMode = PictureBoxSizeMode.Zoom;
            userIcon.Size = new Size(50, 50);

            notificationIcon.Dock = DockStyle.Right;
            notificationIcon.Image = Resources.notification;
            notificationIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            notificationIcon.Size = new Size(64, 32);

            userBox.Controls.Add(userName);
            userBox.Controls.Add(userType);

            userBox.SendToBack();
            userBox.Dock = DockStyle.Right;

            this.Controls.Add(notificationIcon);
            this.Controls.Add(userIcon);
            this.Controls.Add(userBox);

            this.SendToBack();
            this.SendToBack();
            this.SendToBack();

            InitializeComponent();
        }

        public appBar(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
