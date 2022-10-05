using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbComboBox : ComboBox
    {
        public TsbComboBox()
        {
            InitializeComponent();
        }

        public TsbComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
