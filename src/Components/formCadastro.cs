using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class formCadastro : Panel
    {

        public formCadastro()
        {
            SetupForm();
            InitializeComponent();
            
        }

        

        public void SetupForm()
        {
            this.Width = 395;
            this.Height = 1024;
            this.BackColor = TsbColor.background;



            this.Click += new EventHandler(formCadastro_OnClick);
        }

        protected void formCadastro_OnClick(object sender, EventArgs e)
        {
            this.Hide();
        }

        


        public formCadastro(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
