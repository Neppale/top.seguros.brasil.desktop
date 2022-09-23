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

        Button btn = new Button();

        public formCadastro()
        {
            SetupForm();
            InitializeComponent();

            
        }

         

        public void SetupForm()
        {
            this.Width = 1173;
            this.Height = 1024;
            this.BackColor = Color.Purple;

            this.Click += new EventHandler(formCadastro_OnClick);
        }


        protected void formCadastro_OnClick(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionou?");
        }


        public formCadastro(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
