﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Incidents : BasePanel
    {
        public Incidents()
        {
            InitializeComponent();
        }

        public Incidents(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
