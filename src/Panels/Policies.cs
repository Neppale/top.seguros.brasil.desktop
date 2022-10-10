using Newtonsoft.Json;
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
        private static int selectedItemId;
        public ArrayList selectedPolicy = new ArrayList();
        TsbDataTable policiesDataTable = new TsbDataTable();
        EngineInterpreter engineInterpreter = new EngineInterpreter(token);

        public Policies()
        {
            
        }

        public Policies(string pageTitle, string subtitle)
        {

            this.policiesDataTable.CellClick += async (sender, e) =>
            {

                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == policiesDataTable.Columns["Editar"].Index && e.RowIndex >= 0)
                {
                    for (int i = 0; i < policiesDataTable.Columns.Count; i++)
                    {
                        selectedPolicy.Add(policiesDataTable.SelectedRows[0].Cells[i].Value.ToString());
                    }
                    await SubmitPanelSetup<Apolice>(selectedPolicy[0].ToString());
                }

            };

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

        private async void SubmitPanelSetup(string idCliente, string idVeiculo)
        {
            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Cadastro de apólice",
                subtitleText = "Dados gerais da apólice do veículo.",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(titlebox, 0, 1);

            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                submitPanel.Dispose();
            };

            TsbMaskedTextBox rewardField = new TsbMaskedTextBox
            {
                LabelText = "Prêmio",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(rewardField, 1, 1);

            TsbMaskedTextBox indemnField = new TsbMaskedTextBox
            {
                LabelText = "Indenização",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(indemnField, 2, 1);

            var engineInterpreter = new EngineInterpreter(token);
            var coverages = await engineInterpreter.Request<IEnumerable<Cobertura>>("https://tsb-api-policy-engine.herokuapp.com/cobertura/", "GET", null);

            IEnumerable<Cobertura> coveragesList = coverages.Body;

            
            TsbComboBox coverageField = new TsbComboBox
            {
                LabelText = "Cobertura",
                HintText = "Tipo de cobertura",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            foreach (Cobertura coverage in coveragesList)
            {
                coverageField.Items.Add(coverage.nome + " - " + coverage.valor + "/mês");
            }
            submitPanel.Controls.Add(coverageField, 1, 2);
            this.SetColumnSpan(coverageField, 2);

            
            TsbInput descriptionField = new TsbInput
            {
                LabelText = "Descrição da cobertura",
                HintText = $"",
                MaxLength = 11,
                Dock = DockStyle.Top,
                Enabled = false,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };

            

            coverageField.SelectedValueChanged += async (sender, e) =>
            {
                
                string itemNameSplited = coverageField.SelectedItem.ToString().Split()[0];
                
                var selectedCoverage = coveragesList.Where(brand => brand.nome == itemNameSplited);

                selectedItemId = coveragesList.First().id_cobertura;

                descriptionField.Text = selectedCoverage.First().descricao;

            };
            submitPanel.Controls.Add(descriptionField, 1, 3);
            this.SetColumnSpan(descriptionField, 2);

            DateTime dateTime = DateTime.Now;
            
            TsbMaskedTextBox startDateField = new TsbMaskedTextBox
            {
                LabelText = "Data de Início",
                Mask = "00/00/0000",
                HintText = dateTime.Day.ToString() + dateTime.Month.ToString() + dateTime.Year.ToString(),
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(startDateField, 1, 4);

            TsbMaskedTextBox endDateField = new TsbMaskedTextBox
            {
                LabelText = "Data de Fim",
                Mask = "00/00/0000",
                HintText = dateTime.Day.ToString() + dateTime.Month.ToString() + dateTime.Year.ToString(),
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(endDateField, 2, 4);

            ButtonTsbPrimary continueSubmit = new ButtonTsbPrimary
            {
                Text = "CADASTRAR APÓLICE",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(continueSubmit, 2, 8);


            continueSubmit.Click += async (sender, e) =>
            {
                Apolice policy = new Apolice
                (
                    dataInicio: startDateField.Text,
                    dataFim: endDateField.Text,
                    premio: int.Parse(rewardField.Text),
                    indenizacao: int.Parse(indemnField.Text),
                    idCobertura: selectedItemId,
                    idUsuario: userId,
                    idCliente: int.Parse(idCliente),
                    idVeiculo: int.Parse(idVeiculo),
                    status: "Em análise"
                );

                await PostPolicy(policy, null);
            };


            ButtonTsbTertiary cancelSubmit = new ButtonTsbTertiary
            {
                Text = "CANCELAR",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(cancelSubmit, 1, 8);


            if (Controls.OfType<SubmitPanel>().Count() != 0)
            {
                return;
            }


            FindForm().Controls.Add(submitPanel);
            submitPanel.BringToFront();
            submitPanel.Show();
            submitPanel.Visible = true;

            return;

        }

        private async Task SubmitPanelSetup<Type>(string id)
        {
            EngineInterpreterResponse response;

            string address = $"https://tsb-api-policy-engine.herokuapp.com/apolice/{id}";

            response = await engineInterpreter.Request<Type>(address, "GET", null);

            PolicyRequestResponse requestResponse = response.Body;

            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Solicitações de apólice",
                subtitleText = "Gerenciamento de solicitações de apólice.",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(titlebox, 0, 1);

            Panel divider = new Panel();
            divider.Height = 1;
            divider.BackColor = TsbColor.neutralWhite;
            divider.Dock = DockStyle.Top;
            submitPanel.Controls.Add(divider, 0, 6);
            submitPanel.SetColumnSpan(divider, 3);

            TitleBox customerDataTitle = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Dados do cliente",
                subtitleText = "Dados gerais sobre o cliente",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(customerDataTitle, 0, 2);

            TitleBox nameField = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Nome completo",
                FontSize = 16,
                subtitleText = requestResponse.cliente.nome_completo,
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(nameField, 1, 2);

            TitleBox birthDateField = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Data de nascimento",
                FontSize = 16,
                subtitleText = requestResponse.cliente.data_nascimento,
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(birthDateField, 2, 2);

            if (Controls.OfType<SubmitPanel>().Count() != 0)
            {
                return;
            }


            FindForm().Controls.Add(submitPanel);
            submitPanel.BringToFront();
            submitPanel.Show();
            submitPanel.Visible = true;

            return;

        }

        protected async Task PostPolicy(Apolice policyData, EventHandler? e)
        {
            var engineInterpreter = new EngineInterpreter(token);

            var json = JsonConvert.SerializeObject(policyData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await engineInterpreter.Request<Apolice>("https://tsb-api-policy-engine.herokuapp.com/apolice/", "POST", data);


            Controls.Remove(policiesDataTable);
            GetPolicies();
        }


        public Policies(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }   
    
    public class PolicyRequestResponse
    {
        public int indenizacao { get; set; }
        public Cobertura cobertura { get; set; }
        public Usuario usuario { get; set; }
        public Cliente cliente { get; set; }
        public Veiculo veiculo { get; set; }
        public string status { get; set; }
        public string? message { get; set; }
    }
    
}