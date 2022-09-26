using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.Screens.Components;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class BasePanel : Panel
    {
        public static string? token { get; set; }
        public string? userName { get; set; }
        public string? userType { get; set; }

        public BasePanel()
        {
            this.Anchor = (AnchorStyles.None | AnchorStyles.None);
            this.Size = new Size(1173, 909);
            this.Location = new Point(267, 115);
            
            InitializeComponent();
        }

        public BasePanel(string name, string type)
        {
            this.userName = name;
            this.userType = type;
            InitializeComponent();
        }

        public BasePanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
