using MaterialSkin.Controls;
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
using Top_Seguros_Brasil_Desktop.src.Screens.BaseForm;
using Top_Seguros_Brasil_Desktop.Utils;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Outsourced : BasePanel
    {
        private static readonly HttpClient client = new HttpClient();
        public ArrayList selectedOutsourced = new ArrayList();
        TsbDataTable outsourcedDataTable = new TsbDataTable();
        EngineInterpreter engineInterpreter = new EngineInterpreter(token);

        public Outsourced()
        {
            InitializeComponent();
        }

        public Outsourced(string pageTitle, string subtitle)
        {
            ButtonTsbPrimary putButton = new ButtonTsbPrimary();
            this.Controls.Add(putButton, 2, 9);

            this.outsourcedDataTable.CellClick += async (sender, e) =>
            {

                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == outsourcedDataTable.Columns["Editar"].Index && e.RowIndex >= 0)
                {

                    if (outsourcedDataTable.Columns["Editar"].Index == 1)
                    {
                        selectedOutsourced.Add(outsourcedDataTable.SelectedRows[0].Cells[3].Value.ToString());
                        await SubmitPanelSetup(selectedOutsourced[0].ToString());
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < outsourcedDataTable.Columns.Count; i++)
                        {
                            selectedOutsourced.Add(outsourcedDataTable.SelectedRows[0].Cells[i].Value.ToString());
                        }
                        await SubmitPanelSetup(selectedOutsourced[0].ToString());
                        return;
                    }

                }

            };

            putButton.Dock = DockStyle.Top;
            putButton.Margin = new Padding(32);
            putButton.Text = "ADICIONAR TERCEIRIZADO";
            putButton.Click += PutButton_Click;

            SubTitle(subtitle);
            Title(pageTitle);
            InitializeComponent();

            GetOutsourced();
        }

        protected async void GetOutsourced()
        {

            await outsourcedDataTable.Get<Terceirizado>("https://tsb-api-policy-engine.herokuapp.com/terceirizado/");

            outsourcedDataTable.DataBindingComplete += (sender, e) =>
            {
                //outsourcedDataTable.Columns["id_terceirizado"].HeaderText = "ID";
                //outsourcedDataTable.Columns["nome"].HeaderText = "Nome";
                //outsourcedDataTable.Columns["funcao"].HeaderText = "Função";
                //outsourcedDataTable.Columns["cnpj"].HeaderText = "CNPJ";
                //outsourcedDataTable.Columns["valor"].HeaderText = "Valor";
                
                //outsourcedDataTable.Columns["status"].Visible = false;
                //outsourcedDataTable.Columns["Detalhes"].Visible = false;


                string[] columns = { "status", "detalhes"};

                outsourcedDataTable.RemoveColumns(columns);
            };

            Controls.Add(outsourcedDataTable, 0, 5);
            SetColumnSpan(outsourcedDataTable, 3);
            SetRowSpan(outsourcedDataTable, 4);
        }

        private void SubmitPanelSetup()
        {
            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Cadastrar Tereceirizado",
                subtitleText = "Cadastre um novo usuário. ",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(titlebox, 0, 1);


            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                selectedOutsourced.Clear();
                submitPanel.Dispose();
            };


            TsbInput nameField = new TsbInput
            {
                LabelText = "Nome",
                HintText = "Nome do terceirizado",
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


            TsbInput roleField = new TsbInput
            {
                LabelText = "Função",
                HintText = "Ex: Guincho",
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
            submitPanel.Controls.Add(roleField, 2, 1);



            TsbMaskedTextBox telField = new TsbMaskedTextBox
            {
                LabelText = "Telefone",
                Mask = "(99) 99999-9999",
                HintText = "(99) 99999-9999",
                NewValue = true,
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
            submitPanel.Controls.Add(telField, 1, 2);

            TsbMaskedTextBox cnpjField = new TsbMaskedTextBox
            {
                LabelText = "CNPJ",
                Mask = "99,999,999/9999-99",
                HintText = "99.999.999/9999-99",
                NewValue = true,
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
            submitPanel.Controls.Add(cnpjField, 2, 2);


            TsbInput valueField = new TsbInput
            {
                LabelText = "Valor",
                HintText = "Preço por serviço prestado",
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
            submitPanel.Controls.Add(valueField, 1, 3);


            ButtonTsbPrimary submitButton = new ButtonTsbPrimary
            {
                Text = "Cadastrar Terceirizado",
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
                Terceirizado terceirizado = new Terceirizado
                (
                    nome: nameField.Text,
                    funcao: roleField.Text,
                    cnpj: cnpjField.Text,
                    telefone: telField.Text,
                    valor: double.Parse(valueField.Text),
                    status: true
                );
                await PostOutsourced(terceirizado, null);
            };
            submitPanel.Controls.Add(submitButton, 2, 4);


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

        private async Task SubmitPanelSetup(string id)
        {

            EngineInterpreterResponse response;

            string address = $"https://tsb-api-policy-engine.herokuapp.com/terceirizado/{id}";

            response = await engineInterpreter.Request<Terceirizado>(address, "GET", null);

            Terceirizado requestResponse = response.Body;

            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = $"Editar {requestResponse.nome}",
                subtitleText = "Editar terceirizado. ",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(titlebox, 0, 1);


            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                selectedOutsourced.Clear();
                submitPanel.Dispose();
            };


            TsbInput nameField = new TsbInput
            {
                LabelText = "Nome",
                Text = requestResponse.nome,
                ForeColor = TsbColor.neutral,
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


            TsbInput roleField = new TsbInput
            {
                LabelText = "Função",
                Text = requestResponse.funcao,
                ForeColor = TsbColor.neutral,
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
            submitPanel.Controls.Add(roleField, 2, 1);



            TsbMaskedTextBox telField = new TsbMaskedTextBox
            {
                LabelText = "Telefone",
                Mask = "(99) 99999-9999",
                Text = requestResponse.telefone,
                ForeColor = TsbColor.neutral,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(telField, 1, 2);

            TsbMaskedTextBox cnpjField = new TsbMaskedTextBox
            {
                LabelText = "CNPJ",
                Mask = "99,999,999/9999-99",
                Text = requestResponse.cnpj,
                ForeColor = TsbColor.neutral,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(cnpjField, 2, 2);


            TsbInput valueField = new TsbInput
            {
                LabelText = "Valor",
                Text = requestResponse.valor.ToString(),
                ForeColor = TsbColor.neutral,
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
            submitPanel.Controls.Add(valueField, 1, 3);


            ButtonTsbPrimary submitButton = new ButtonTsbPrimary
            {
                Text = "Editar Terceirizado",
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
                Terceirizado terceirizado = new Terceirizado
                (
                    nome: nameField.Text,
                    funcao: roleField.Text,
                    cnpj: cnpjField.Text,
                    telefone: telField.Text,
                    valor: double.Parse(valueField.Text),
                    status: true
                );
                await PutOutsourced(terceirizado, id);
            };
            submitPanel.Controls.Add(submitButton, 2, 4);


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

        protected async Task PutOutsourced(Terceirizado terceirizadoData, string id)
        {
            await outsourcedDataTable.Put<Terceirizado>(terceirizadoData, id);
            Controls.Remove(outsourcedDataTable);
            GetOutsourced();
        }

        protected async Task PostOutsourced(Terceirizado outsourcedData, EventHandler? e)
        {
            

            var engineInterpreter = new EngineInterpreter(token);

            var json = JsonConvert.SerializeObject(outsourcedData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await engineInterpreter.Request<Terceirizado>("https://tsb-api-policy-engine.herokuapp.com/terceirizado/", "POST", data);

            Controls.Remove(outsourcedDataTable);

            GetOutsourced();
        }

        private void PutButton_Click(object? sender, EventArgs? e)
        {
            SubmitPanelSetup();
        }

        public Outsourced(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }

    public class OutsourcedResponse
    {
        public Terceirizado? terceirizado { get; set; }
        public string? message { get; set; }
    }

}
