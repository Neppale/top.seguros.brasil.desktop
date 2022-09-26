using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Policies : BasePanel
    {
        public Policies()
        {
            this.BackColor = Color.Purple;   
            InitializeComponent();
        }

        public Policies(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
