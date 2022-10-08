using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.font;
using Top_Seguros_Brasil_Desktop.src.Panels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Top_Seguros_Brasil_Desktop.src.Screens.Components
{
    public partial class SideNav : Panel
    {

        Panel panel;

        public SideNav()
        {
            InitializeComponent();

           

            this.Width = 267;
            this.Height = 1024;
            this.BackColor = TsbColor.surface;

            string imgPath = Directory.GetCurrentDirectory() + "\\src\\img\\logo\\tsb-logo.png";
            PictureBox logoBox = new PictureBox();
            logoBox.Image = new Bitmap(imgPath);
            logoBox.SizeMode = PictureBoxSizeMode.Zoom;
            logoBox.Location = new Point(0, 32);
            logoBox.Width = this.Width;
            this.Controls.Add(logoBox);
            

            panel = new Panel();
            panel.Width = this.Width;
            panel.Parent = this;
            panel.Location = new Point(0, 380);
            panel.Height = 500;
            


            MenuItem user = new MenuItem("Usuários", Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\users.png");
            user.Dock = DockStyle.Top;

            MenuItem policySolicitation = new MenuItem("Solicitações de apólice", Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\policys.png");
            policySolicitation.Dock = DockStyle.Top;

            MenuItem customer = new MenuItem("Clientes", Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\customer.png");
            customer.Dock = DockStyle.Top;

            MenuItem incident = new MenuItem("Ocorrências", Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\incident.png");
            incident.Dock = DockStyle.Top;

            MenuItem covery = new MenuItem("Coberturas", Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\covery.png");
            covery.Dock = DockStyle.Top;

            MenuItem vehicle = new MenuItem("Veículos", Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\vehicles.png");
            vehicle.Dock = DockStyle.Top;

            MenuItem outSourced = new MenuItem("Terceirizados", Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\outsourced.png");
            outSourced.Dock = DockStyle.Top;

            panel.Controls.Add(outSourced);
            panel.Controls.Add(vehicle);
            panel.Controls.Add(covery);
            panel.Controls.Add(incident);
            panel.Controls.Add(customer);
            panel.Controls.Add(policySolicitation);
            panel.Controls.Add(user);

            this.Dock = DockStyle.Left;

            Panel divider = new Panel();
            divider.Width = 1;
            divider.Height = 50;
            divider.BackColor = TsbColor.neutralWhite;
            divider.Dock = DockStyle.Right;
            this.Controls.Add(divider);
            divider.BringToFront();



        }

        public SideNav(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        

    }

    public partial class MenuItem : Button
    {

        private string name { get; set; }

        public MenuItem(string name, string img)
        {
            TsbFont tsbFont = new TsbFont();
            
            this.name = name;
            this.Height = 50;
            this.ForeColor = TsbColor.neutralGray;
            this.Text = "    " + name;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.Padding = new Padding(32, 8, 32, 8);
            this.Font = new Font(TsbFont.TsbFonts.Families[3], 10);

            Bitmap bitmap = new Bitmap(img);
            this.Image = bitmap;
            this.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.ImageAlign = ContentAlignment.MiddleLeft;
            
            this.Click += new EventHandler(OnClickItem);

        }


        protected override void OnMouseHover(EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            base.OnMouseHover(e);
        }


        protected void OnClickItem(object sender, EventArgs e)
        {

            if (this.name == "Usuários")
            {
                foreach (Control control in this.FindForm().Controls)
                {
                    if (control is appBar || control is SideNav)
                    {
                        continue;
                    }

                    control.Dispose();
                }

                if (this.FindForm().Controls.OfType<Users>().Count() == 0)
                {
                    Users usersPage = new Users("Usuários", "Gerenciamento de Usuários");
                    FindForm().Controls.Add(usersPage);
                    usersPage.BringToFront();
                    return;
                }

                return;
            }
            
            if (this.name == "Clientes")
            {
                foreach (Control control in this.FindForm().Controls)
                {
                    if (control is appBar || control is SideNav)
                    {
                        continue;
                    }

                    control.Dispose();
                }

                if (this.FindForm().Controls.OfType<Customers>().Count() == 0)
                {
                    Customers customersPage = new Customers("Clientes", "Gerenciamento de Clientes");
                    FindForm().Controls.Add(customersPage);
                    customersPage.BringToFront();                    
                    return;
                }

                return;

            }

            if (this.name == "Ocorrências")
            {
                //dispose all controls except SideNav and appbar
                foreach (Control control in this.FindForm().Controls)
                {
                    if (control is appBar || control is SideNav)
                    {
                        continue;
                    }

                    control.Dispose();
                }


                if (this.FindForm().Controls.OfType<Incidents>().Count() == 0)
                {
                    Incidents IncidentsPage = new Incidents();
                    FindForm().Controls.Add(IncidentsPage);
                    IncidentsPage.BringToFront();
                    return;


                }

                return;

            }

            if (this.name == "Solicitações de apólice")
            {
                //dispose all controls except SideNav and appbar
                foreach (Control control in this.FindForm().Controls)
                {
                    if (control is appBar || control is SideNav)
                    {
                        continue;
                    }

                    control.Dispose();
                }


                if (this.FindForm().Controls.OfType<Apolices>().Count() == 0)
                {
                    Apolices ApolicesPage = new Apolices("Apolices", "Gerenciamento de apólices");
                    FindForm().Controls.Add(ApolicesPage);
                    ApolicesPage.BringToFront();
                    return;


                }

                return;

            }

        }

        

    }
}
