using MaterialSkin.Controls;
using Newtonsoft.Json.Linq;
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
    public partial class Coverages : BasePanel
    {
        private static readonly HttpClient client = new HttpClient();
        public static ArrayList selectedCoverage = new ArrayList();
        TsbDataTable coveragesDataTable = new TsbDataTable();
        EngineInterpreter engineInterpreter = new EngineInterpreter(token);

        public Coverages()
        {
            InitializeComponent();
        }

        public Coverages(string pageTitle, string subtitle)
        {

            ButtonTsbPrimary putButton = new ButtonTsbPrimary();
            this.Controls.Add(putButton, 2, 9);

            MaterialSingleLineTextField customerSearchBox = new MaterialSingleLineTextField
            {
                Hint = "🔎 | Buscar cobertura: ",
                SelectionStart = 6,
                Dock = DockStyle.Top,
                Margin = new Padding(32),
            };

            this.coveragesDataTable.CellClick += async (sender, e) =>
            {

                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == coveragesDataTable.Columns["Editar"].Index && e.RowIndex >= 0)
                {
                    for (int i = 0; i < coveragesDataTable.Columns.Count; i++)
                    {
                        selectedCoverage.Add(coveragesDataTable.SelectedRows[0].Cells[i].Value.ToString());
                    }
                    await SubmitPanelSetup<Cobertura>(selectedCoverage[0].ToString());
                }

            };

            this.Controls.Add(customerSearchBox, 0, 5);
            putButton.Dock = DockStyle.Top;
            putButton.Margin = new Padding(32);
            putButton.Text = "Adicionar Usuário";
            putButton.Click += PutButton_Click;


            SubTitle(subtitle);
            Title(pageTitle);
            InitializeComponent();

            GetCoverages();

            InitializeComponent();
        }

        protected async void GetCoverages()
        {

            await coveragesDataTable.Get<Cobertura>("https://tsb-api-policy-engine.herokuapp.com/cobertura/");

            coveragesDataTable.DataBindingComplete += (sender, e) =>
            {
                coveragesDataTable.Columns["id_cobertura"].HeaderText = "ID";
                coveragesDataTable.Columns["nome"].HeaderText = "Nome";
                coveragesDataTable.Columns["descricao"].HeaderText = "Descricao";
                coveragesDataTable.Columns["valor"].HeaderText = "Valor";
                coveragesDataTable.Columns["taxa_indenizacao"].HeaderText = "Taxa Indenizacao";

                coveragesDataTable.Columns["status"].Visible = false;
                coveragesDataTable.Columns["message"].Visible = false;
            };

            Controls.Add(coveragesDataTable, 0, 7);
            SetColumnSpan(coveragesDataTable, 3);
        }

        private void SubmitPanelSetup()
        {
            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Cadastrar Usuário",
                subtitleText = "Cadastre um novo usuário. ",
                Margin = new Padding(32),

            };


            submitPanel.Controls.Add(titlebox, 0, 1);


            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                submitPanel.Dispose();
            };


            TsbInput nameField = new TsbInput
            {
                LabelText = "Nome",
                HintText = "Nome da cobertura aqui",
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
            submitPanel.Controls.Add(nameField, 1, 1); ;


            TsbInput descriptionField = new TsbInput
            {
                LabelText = "Descrição da cobertura",
                HintText = " O que ela vai cobrir?",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Width = 331,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(descriptionField, 2, 1);
            

            TsbInput priceField = new TsbInput
            {
                LabelText = "Valor",
                HintText = "Preço da cobertura",
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
            submitPanel.Controls.Add(priceField, 1, 2);

            TsbInput rateField = new TsbInput
            {
                LabelText = "Taxa",
                HintText = "Porcentagem de cobertura",
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
            submitPanel.Controls.Add(rateField, 2, 2);


            ButtonTsbPrimary submitButton = new ButtonTsbPrimary
            {
                Text = "Cadastrar Cobertura",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };

            submitButton.Click += async (sender, e) =>
            {
                Cobertura cobertura = new Cobertura
                (
                    nome: nameField.Text,
                    descricao: descriptionField.Text,
                    valor: priceField.Text,
                    taxaIndenizacao: double.Parse(rateField.Text),
                    status: true
                );

                await PostCoverage(cobertura, null);
            };


            submitPanel.Controls.Add(submitButton, 3, 3);


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


            string address = "https://tsb-api-policy-engine.herokuapp.com/cobertura/";

            response = await engineInterpreter.Request<Type>($"{address}{id}", "GET", null);

            Cobertura responseBody = response.Body;

            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = $"Editar Cobertura {responseBody.nome}",
                subtitleText = "Cadastre um novo usuário. ",
                Margin = new Padding(32),

            };

            submitPanel.Controls.Add(titlebox, 0, 1);


            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                submitPanel.Dispose();
            };


            TsbInput nameField = new TsbInput
            {
                LabelText = "Nome",
                Text = $"{responseBody.nome}",
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
            submitPanel.Controls.Add(nameField, 1, 1); ;


            TsbInput descriptionField = new TsbInput
            {
                LabelText = "Descrição da cobertura",
                Text = $"{responseBody.descricao}",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Width = 331,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(descriptionField, 2, 1);


            TsbInput priceField = new TsbInput
            {
                LabelText = "Valor",
                Text = $"{responseBody.valor}",
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
            submitPanel.Controls.Add(priceField, 1, 2);

            TsbInput rateField = new TsbInput
            {
                LabelText = "Taxa",
                Text = $"{responseBody.taxa_indenizacao}",
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
            submitPanel.Controls.Add(rateField, 2, 2);


            ButtonTsbPrimary submitButton = new ButtonTsbPrimary
            {
                Text = "Editar Cobertura",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };

            submitButton.Click += async (sender, e) =>
            {
                Cobertura cobertura = new Cobertura
                (
                    nome: nameField.Text,
                    descricao: descriptionField.Text,
                    valor: priceField.Text,
                    taxaIndenizacao: double.Parse(rateField.Text),
                    status: true
                );

                await PutCoverage(cobertura, id);
                GetCoverages();
            };


            submitPanel.Controls.Add(submitButton, 3, 3);


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

        private void PutButton_Click(object? sender, EventArgs? e)
        {
            SubmitPanelSetup();
        }

        protected async Task PostCoverage(Cobertura coverageData, EventHandler? e)
        {

            await coveragesDataTable.Post<Cobertura>(coverageData);

            Controls.Remove(coveragesDataTable);

            GetCoverages();
        }

        protected async Task PutCoverage(Cobertura coverageData, string id)
        {
            await coveragesDataTable.Put<Cobertura>(coverageData, id);
            Controls.Remove(coveragesDataTable);
            GetCoverages();
        }
        
        public Coverages(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
