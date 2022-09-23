using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.Screens.Components;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class formCadastro : Panel
    {
        Button exit = new Button();
        Panel campos = new Panel();
        Panel botoes = new Panel();

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

            exit.Text = " <==  Usuários";
            exit.BackColor = TsbColor.neutralGray;
            exit.Width = 331;
            exit.Height = 60;
            exit.Location = new Point(32, 32);
            exit.Click += new EventHandler(formCadastro_close);
            this.Controls.Add(exit);


            campos.Width = 331;
            campos.Height = 348;
            campos.Location = new Point(32, 304);
            campos.BackColor = TsbColor.neutralGray;
            this.Controls.Add(campos);




        }

        protected void formCadastro_close(object sender, EventArgs e)
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
