using Top_Seguros_Brasil_Desktop.src.Screens.Components;

namespace Top_Seguros_Brasil_Desktop.src.Screens.Components
{
    public partial class SideNav : Panel
    {

        Panel panel;

        public SideNav()
        {
            InitializeComponent();

            PictureBox logoBox = new PictureBox();

            this.Width = 267;
            this.Height = 1024;
            this.BackColor = TsbColor.surface;
            try
            {
                string imgPath = Directory.GetCurrentDirectory() + "\\src\\img\\logo\\tsb-logo.png";
                logoBox.Image = new Bitmap(imgPath);
                logoBox.SizeMode = PictureBoxSizeMode.Zoom;
                logoBox.Location = new Point(0, 32);
                logoBox.Width = this.Width;
                this.Controls.Add(logoBox);
            }
            catch
            {
                string imgPath = "..\\..\\..\\src\\img\\logo\\tsb-logo.png";
                logoBox.Image = new Bitmap(imgPath);
                logoBox.SizeMode = PictureBoxSizeMode.Zoom;
                logoBox.Location = new Point(0, 32);
                logoBox.Width = this.Width;
                this.Controls.Add(logoBox);
            }

            logoBox.Cursor = Cursors.Hand;
            logoBox.Click += (sender, e) =>
            {
                foreach (Control control in this.FindForm().Controls)
                {

                    if (control is appBar || control is SideNav)
                    {
                        continue;
                    }
                    
                    control.Dispose();
                }

                if (this.FindForm().Controls.OfType<Home>().Count() == 0)
                {
                    Home homePage = new Home($"Dashboard", "", BasePanel.userId) { Visible = true};
                    FindForm().Controls.Add(homePage);
                    homePage.BringToFront();
                    return;

                }
                return;
            };

            panel = new Panel();
            panel.Width = this.Width;
            panel.Parent = this;
            panel.Location = new Point(0, 380);
            panel.Height = 500;



            MenuItem user = new MenuItem("Usuários", Resources.users);
            user.Dock = DockStyle.Top;

            MenuItem policySolicitation = new MenuItem("Solicitações de apólice", Resources.policys);
            policySolicitation.Dock = DockStyle.Top;

            MenuItem customer = new MenuItem("Clientes", Resources.customers);
            customer.Dock = DockStyle.Top;

            MenuItem incident = new MenuItem("Ocorrências", Resources.incidents);
            incident.Dock = DockStyle.Top;

            MenuItem covery = new MenuItem("Coberturas", Resources.covery);
            covery.Dock = DockStyle.Top;

            MenuItem vehicle = new MenuItem("Veículos", Resources.vehicles);
            vehicle.Dock = DockStyle.Top;

            MenuItem outSourced = new MenuItem("Terceirizados", Resources.outsourced);
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

        private void SideNav_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.FindForm().Controls)
            {

                if (control is appBar || control is SideNav)
                {
                    continue;
                }

                control.Dispose();
            }

            if (this.FindForm().Controls.OfType<Home>().Count() == 0)
            {
                Home homePage = new Home("Home", "Bem vindo", BasePanel.userId);
                FindForm().Controls.Add(homePage);
                homePage.BringToFront();
                return;

            }
            return;
        }

    }

    public partial class MenuItem : Button
    {

        private string name { get; set; }
        private bool isSelected { get; set; }

        public MenuItem(string name, Bitmap img)
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
            this.Image = img;
            this.isSelected = false;

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
                    Incidents IncidentsPage = new Incidents("Ocorrências", "Gerenciamento de Ocorrências");
                    FindForm().Controls.Add(IncidentsPage);
                    IncidentsPage.BringToFront();
                    return;


                }

                return;

            }

            if (this.name == "Veículos")
            {

                foreach (Control control in this.FindForm().Controls)
                {
                    if (control is appBar || control is SideNav)
                    {
                        continue;
                    }

                    control.Dispose();
                }


                if (this.FindForm().Controls.OfType<Vehicles>().Count() == 0)
                {
                    Vehicles VehiclesPage = new Vehicles("Veículos", "Gerenciamento de Veículos");
                    FindForm().Controls.Add(VehiclesPage);
                    VehiclesPage.BringToFront();
                    return;


                }

                return;

            }

            if (this.name == "Coberturas")
            {

                foreach (Control control in this.FindForm().Controls)
                {
                    if (control is appBar || control is SideNav)
                    {
                        continue;
                    }

                    control.Dispose();
                }


                if (this.FindForm().Controls.OfType<Cobertura>().Count() == 0)
                {
                    Coverages CoveragesPage = new Coverages("Coberturas", "Gerenciamento de coberturas");
                    FindForm().Controls.Add(CoveragesPage);
                    CoveragesPage.BringToFront();
                    return;

                }

                return;

            }

            if (this.name == "Solicitações de apólice")
            {

                foreach (Control control in this.FindForm().Controls)
                {
                    if (control is appBar || control is SideNav)
                    {
                        continue;
                    }

                    control.Dispose();
                }


                if (this.FindForm().Controls.OfType<Policies>().Count() == 0)
                {
                    Policies CoveragesPage = new Policies("Solicitações de apólice", "Gerenciamento de apólices.");
                    FindForm().Controls.Add(CoveragesPage);
                    CoveragesPage.BringToFront();
                    return;


                }

                return;

            }

            if (this.name == "Terceirizados")
            {

                foreach (Control control in this.FindForm().Controls)
                {
                    if (control is appBar || control is SideNav)
                    {
                        continue;
                    }

                    control.Dispose();
                }


                if (this.FindForm().Controls.OfType<Policies>().Count() == 0)
                {
                    Outsourced OutsourcedPage = new Outsourced("Terceirizados", "Gerenciamento de serviços terceirizados.");
                    FindForm().Controls.Add(OutsourcedPage);
                    OutsourcedPage.BringToFront();
                    return;


                }

                return;

            }

        }
    }
}
