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
    public partial class Policies : BasePanel
    {

        private static readonly HttpClient client = new HttpClient();
        public ArrayList selectedPolicy = new ArrayList();
        TsbDataTable policiesDataTable = new TsbDataTable();
        EngineInterpreter engineInterpreter = new EngineInterpreter(token);

        public Policies()
        {
            
        }

        public Policies(string pageTitle, string subtitle)
        {
            InitializeComponent();

            SubTitle(subtitle);
            Title(pageTitle);

            GetPolicies();
        }

        protected async void GetPolicies()
        {

            await policiesDataTable.Get<Apolice>($"https://tsb-api-policy-engine.herokuapp.com/apolice/usuario/{userId}");

            policiesDataTable.DataBindingComplete += (sender, e) =>
            {
                policiesDataTable.Columns["id_apolice"].HeaderText = "ID";
                policiesDataTable.Columns["nome"].HeaderText = "Nome";
                policiesDataTable.Columns["status"].HeaderText = "Status";
                policiesDataTable.Columns["veiculo"].HeaderText = "Veiculo";
                policiesDataTable.Columns["tipo"].HeaderText = "Tipo de Cobertura";

                policiesDataTable.Columns["data_inicio"].Visible = false;
                policiesDataTable.Columns["data_fim"].Visible = false;
                policiesDataTable.Columns["premio"].Visible = false;
                policiesDataTable.Columns["indenizacao"].Visible = false;
                policiesDataTable.Columns["id_cobertura"].Visible = false;
                policiesDataTable.Columns["id_usuario"].Visible = false;
                policiesDataTable.Columns["id_cliente"].Visible = false;
                policiesDataTable.Columns["id_veiculo"].Visible = false;
                policiesDataTable.Columns["message"].Visible = false;
            };

            Controls.Add(policiesDataTable, 0, 7);
            SetColumnSpan(policiesDataTable, 3);
        }

        public Policies(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
