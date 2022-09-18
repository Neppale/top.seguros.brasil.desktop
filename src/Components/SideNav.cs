﻿using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.Components;

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
            panel.Controls.Add(user);

            MenuItem policySolicitation = new MenuItem("Solicitações de apólice", Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\policys.png");
            policySolicitation.Dock = DockStyle.Top;
            panel.Controls.Add(policySolicitation);

            MenuItem customer = new MenuItem("Clientes", Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\customer.png");
            customer.Dock = DockStyle.Top;
            panel.Controls.Add(customer);

            MenuItem incident = new MenuItem("Ocorrências", Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\incident.png");
            incident.Dock = DockStyle.Top;
            panel.Controls.Add(incident);

            MenuItem covery = new MenuItem("Coberturas", Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\covery.png");
            covery.Dock = DockStyle.Top;
            panel.Controls.Add(covery);

            MenuItem vehicle = new MenuItem("Veículos", Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\vehicles.png");
            vehicle.Dock = DockStyle.Top;
            panel.Controls.Add(vehicle);

            MenuItem outSourced = new MenuItem("Terceirizados", Directory.GetCurrentDirectory() + "\\src\\img\\icon\\nav\\outsourced.png");
            outSourced.Dock = DockStyle.Top;
            panel.Controls.Add(outSourced);

        }

        public SideNav(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }

    public partial class MenuItem : Button
    {

        public MenuItem(string name, string img)
        {
            this.Height = 50;
            this.ForeColor = TsbColor.neutralGray;
            this.Text = "    " + name;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.Padding = new Padding(32, 8, 32, 8);

            Bitmap bitmap = new Bitmap(img);
            this.Image = bitmap;
            this.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.ImageAlign = ContentAlignment.MiddleLeft;
        }

        protected override void OnMouseHover(EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            base.OnMouseHover(e);
        }
    }
}