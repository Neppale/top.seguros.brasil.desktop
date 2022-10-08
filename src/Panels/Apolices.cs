using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.Models;
using Top_Seguros_Brasil_Desktop.Utils;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Apolices : BasePanel
    {

        private static readonly HttpClient client = new HttpClient();
        public ArrayList selectedUser = new ArrayList();
        TsbDataTable usersDataTable = new TsbDataTable();
        EngineInterpreter engineInterpreter = new EngineInterpreter(token);

        public Apolices()
        {
            
        }

        public Apolices(string pageTitle, string subtitle)
        {
            InitializeComponent();

            SubTitle(subtitle);
            Title(pageTitle);

            GetUsers();
        }

        protected async void GetUsers()
        {

            await usersDataTable.Get<Apolice>("https://tsb-api-policy-engine.herokuapp.com/usuario/");

            //usersDataTable.DataBindingComplete += (sender, e) =>
            //{
            //    usersDataTable.Columns["id_usuario"].HeaderText = "ID";
            //    usersDataTable.Columns["nome_completo"].HeaderText = "Nome";
            //    usersDataTable.Columns["email"].HeaderText = "Email";
            //    usersDataTable.Columns["tipo"].HeaderText = "Tipo";
            //    usersDataTable.Columns["senha"].Visible = false;
            //    usersDataTable.Columns["status"].Visible = false;
            //};

            Controls.Add(usersDataTable, 0, 7);
            SetColumnSpan(usersDataTable, 3);
        }

        public Apolices(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
