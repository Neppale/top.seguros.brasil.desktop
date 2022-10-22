using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.Components;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Home : BasePanel
    {
        TsbDataTable usersDataTable = new TsbDataTable();

        public Home()
        {
        }

        public Home(string pageTitle, string subtitle)
        {

            ButtonTsbPrimary putButton = new ButtonTsbPrimary();
            this.Controls.Add(putButton, 2, 9);

            SubTitle(subtitle);
            Title(pageTitle);
            InitializeComponent();
        }

        public Home(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
