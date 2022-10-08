using MaterialSkin.Controls;
using System.Collections;
using System.ComponentModel;
using System.Net;
using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.font;
using Top_Seguros_Brasil_Desktop.src.Models;
using Top_Seguros_Brasil_Desktop.Utils;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Customers : BasePanel
    {
        private static readonly HttpClient client = new HttpClient();
        public static ArrayList selectedCustomer = new ArrayList();
        TsbDataTable customersDataTable = new TsbDataTable();
        EngineInterpreter engineInterpreter = new EngineInterpreter(token);
        public Customers()
        {
        }

        public Customers(string pageTitle, string subtitle)
        {
            ButtonTsbPrimary putButton = new ButtonTsbPrimary();
            this.Controls.Add(putButton, 2, 9);

            MaterialSingleLineTextField customerSearchBox = new MaterialSingleLineTextField
            {
                Hint = "🔎 | Buscar cliente: ",
                SelectionStart = 6,
                Dock = DockStyle.Top,
                Margin = new Padding(32),
            };

            this.customersDataTable.CellClick += async (sender, e) =>
            {

                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == customersDataTable.Columns["Editar"].Index && e.RowIndex >= 0)
                {                    
                    for (int i = 0; i < customersDataTable.Columns.Count; i++)
                    {
                        selectedCustomer.Add(customersDataTable.SelectedRows[0].Cells[i].Value.ToString());
                    }
                    await SubmitPanelSetup<Cliente>(selectedCustomer[0].ToString());
                }

            };

            this.Controls.Add(customerSearchBox, 0, 5);
            putButton.Dock = DockStyle.Top;
            putButton.Margin = new Padding(32);
            putButton.Text = "Adicionar Cliente";
            putButton.Click += PutButton_Click;

            SubTitle(subtitle);
            Title(pageTitle);
            InitializeComponent();

            GetCustomers();
        }

        protected async void GetCustomers()
        {

            await customersDataTable.Get<Cliente>("https://tsb-api-policy-engine.herokuapp.com/cliente/");

            customersDataTable.DataBindingComplete += (sender, e) =>
            {
                customersDataTable.Columns["id_cliente"].HeaderText = "ID";
                customersDataTable.Columns["nome_completo"].HeaderText = "Nome";
                customersDataTable.Columns["email"].HeaderText = "Email";
                customersDataTable.Columns["cpf"].HeaderText = "CPF";
                customersDataTable.Columns["telefone1"].HeaderText = "Telefone 1";
                customersDataTable.Columns["Status"].HeaderText = "Status";

                customersDataTable.Columns["senha"].Visible = false;
                customersDataTable.Columns["cnh"].Visible = false;
                customersDataTable.Columns["cep"].Visible = false;
                customersDataTable.Columns["data_nascimento"].Visible = false;
                customersDataTable.Columns["telefone2"].Visible = false;
                customersDataTable.Columns["message"].Visible = false;
                customersDataTable.Columns["status"].Visible = false;

            };

            Controls.Add(customersDataTable, 0, 7);
            SetColumnSpan(customersDataTable, 3);
        }
        
        private void SubmitPanelSetup()
        {
            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Cadastrar Cliente",
                subtitleText = "Cadastre um novo cliente. ",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(titlebox, 0, 1);


            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                submitPanel.Dispose();
            };


            TsbInput nameField = new TsbInput
            {
                LabelText =  "Nome",
                HintText = "Nome Completo do cliente",
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

            DateTime dateTime = DateTime.Now;
            

            TsbMaskedTextBox dateBirthField = new TsbMaskedTextBox
            {
                LabelText = "Data de Nascimento",
                Mask = "00/00/0000",
                HintText = dateTime.Day.ToString() + "/" + dateTime.Month.ToString() + "/" + dateTime.Year.ToString(),
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
            submitPanel.Controls.Add(dateBirthField, 2, 1);
            

            TsbMaskedTextBox cpfField = new TsbMaskedTextBox
            {
                LabelText = "CPF",
                Mask = "000,000,000-00",
                HintText = "000,000,000-00",
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
            submitPanel.Controls.Add(cpfField, 1, 2);
            

            TsbInput cnhField = new TsbInput
            {
                LabelText = "CNH",
                HintText = "CNH",
                MaxLength = 11,
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
            submitPanel.Controls.Add(cnhField, 2, 2);


            TsbMaskedTextBox cepField = new TsbMaskedTextBox
            {
                LabelText = "CEP",
                Mask = "00000-000",
                HintText = "00000-000",
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
            submitPanel.Controls.Add(cepField, 1, 3);


            TsbInput emailField = new TsbInput
            {
                LabelText = "Email",
                HintText = "Email do cliente aqui",
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
            submitPanel.Controls.Add(emailField, 2, 3);
            

            TsbMaskedTextBox tel1Field = new TsbMaskedTextBox
            {
                LabelText = "Telefone 1",
                Mask = "(00) 00000-0000",
                HintText = "(00) 00000-0000",
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
            submitPanel.Controls.Add(tel1Field, 1, 4);

            TsbMaskedTextBox tel2Field = new TsbMaskedTextBox
            {
                LabelText = "Telefone 2",
                Mask = "(00) 00000-0000",
                HintText = "(00) 00000-0000",
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
            submitPanel.Controls.Add(tel2Field, 2, 4);

            ButtonTsbSecondary submitButton = new ButtonTsbSecondary
            {
                Text = "CADASTRAR CLIENTE SEM APÓLICE",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(submitButton, 2, 5);
            
            submitButton.Click += async (sender, e) =>
            {
                Cliente customer = new Cliente
                (
                    nomeCompleto: nameField.Text,
                    email: emailField.Text,
                    senha: "Senha123-",
                    cpf: cpfField.Text,
                    cnh: cnhField.Text,
                    cep: cepField.Text,
                    dataNascimento: dateBirthField.Text,
                    telefone1: tel1Field.Text,
                    telefone2: tel2Field.Text,
                    status: true
                );

                await PostCustomer(customer, null);
            };
            

            Panel divider = new Panel();
            divider.Height = 1;
            divider.BackColor = TsbColor.neutralWhite;
            divider.Dock = DockStyle.Top;
            submitPanel.Controls.Add(divider, 0, 6);
            submitPanel.SetColumnSpan(divider, 3);

            TitleBox titleboxContinue = new TitleBox
            {
                Parent = submitPanel,
                GoBackable = false,
                titleText = "Continuar Cadastrando",
                subtitleText = "",
                Margin = new Padding
                {
                    Top = 48,
                    Bottom = 48,
                    Left = 32,
                    Right = 32
                }

            };
            submitPanel.Controls.Add(titleboxContinue, 0, 7);

            TsbFont tsbFont = new TsbFont();

            Label or = new Label
            {
                Text = "ou",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = TsbColor.neutralGray,
                Font = new Font(TsbFont.TsbFonts.Families[0], 10, FontStyle.Bold),
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 48,
                    Bottom = 48,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.SetColumnSpan(or, 2);
            submitPanel.Controls.Add(or, 1, 7);

            ButtonTsbPrimary continueSubmit = new ButtonTsbPrimary
            {
                Text = "CADASTRAR VEÍCULO",
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


            string address = "https://tsb-api-policy-engine.herokuapp.com/cliente/";

            response = await engineInterpreter.Request<Type>($"{address}{id}", "GET", null);

            Cliente responseBody = response.Body;

            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = $"Editar {responseBody.nome_completo}",
                subtitleText = "Editar cliente. ",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(titlebox, 0, 1);


            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                selectedCustomer.Clear();
                submitPanel.Dispose();
            };


            TsbInput nameField = new TsbInput
            {
                LabelText = "Nome",
                HintText = responseBody.nome_completo,
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

            DateTime dateTime = DateTime.Now;


            TsbMaskedTextBox dateBirthField = new TsbMaskedTextBox
            {
                LabelText = "Data de Nascimento",
                Mask = "00/00/0000",
                HintText = responseBody.data_nascimento,
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
            submitPanel.Controls.Add(dateBirthField, 2, 1);


            TsbMaskedTextBox cpfField = new TsbMaskedTextBox
            {
                LabelText = "CPF",
                Mask = "000,000,000-00",
                Text = responseBody.cpf,
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
            submitPanel.Controls.Add(cpfField, 1, 2);


            TsbInput cnhField = new TsbInput
            {
                LabelText = "CNH",
                Text = responseBody.cnh,
                MaxLength = 11,
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
            submitPanel.Controls.Add(cnhField, 2, 2);


            TsbMaskedTextBox cepField = new TsbMaskedTextBox
            {
                LabelText = "CEP",
                Mask = "00000-000",
                Text = responseBody.cep,
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
            submitPanel.Controls.Add(cepField, 1, 3);


            TsbInput emailField = new TsbInput
            {
                LabelText = "Email",
                Text = responseBody.email,
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
            submitPanel.Controls.Add(emailField, 2, 3);


            TsbMaskedTextBox tel1Field = new TsbMaskedTextBox
            {
                LabelText = "Telefone 1",
                Mask = "(00) 00000-0000",
                Text = responseBody.telefone1,
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
            submitPanel.Controls.Add(tel1Field, 1, 4);

            TsbMaskedTextBox tel2Field = new TsbMaskedTextBox
            {
                LabelText = "Telefone 2",
                Mask = "(00) 00000-0000",
                Text = responseBody.telefone2,
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
            submitPanel.Controls.Add(tel2Field, 2, 4);

            ButtonTsbPrimary submitButton = new ButtonTsbPrimary
            {
                Text = "EDITAR CLIENTE",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(submitButton, 2, 5);

            submitButton.Click += async (sender, e) =>
            {
                Cliente customer = new Cliente
                (
                    nomeCompleto: nameField.Text,
                    email: emailField.Text,
                    senha: "Senha123-",
                    cpf: cpfField.Text,
                    cnh: cnhField.Text,
                    cep: cepField.Text,
                    dataNascimento: dateBirthField.Text,
                    telefone1: tel1Field.Text,
                    telefone2: tel2Field.Text,
                    status: true
                );

                await PutCustomer(customer, id);
                GetCustomers();
            };


            Panel divider = new Panel();
            divider.Height = 1;
            divider.BackColor = TsbColor.neutralWhite;
            divider.Dock = DockStyle.Top;
            submitPanel.Controls.Add(divider, 0, 6);
            submitPanel.SetColumnSpan(divider, 3);
            

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

        protected async Task PutCustomer(Cliente customerData, string id)
        {
            await customersDataTable.Put<Cliente>(customerData, id);
        }

        protected async Task PostCustomer(Cliente customerData, EventHandler? e)
        {
            await customersDataTable.Post<CustomerInsertResponse>(customerData);
            Controls.Remove(customersDataTable);
            GetCustomers();
        }

        public Customers(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void PutButton_Click(object? sender, EventArgs? e)
        {
            SubmitPanelSetup();
        }
    }
}

public class CustomerInsertResponse
{
    public Cliente? cliente { get; set; }
    public string? message { get; set; }
}
