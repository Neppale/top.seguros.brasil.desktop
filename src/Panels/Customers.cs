using MaterialSkin.Controls;
using System.Collections;
using System.ComponentModel;
using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.Models;
using Top_Seguros_Brasil_Desktop.Utils;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Customers : BasePanel
    {
        private static readonly HttpClient client = new HttpClient();
        public ArrayList selectedCustomer = new ArrayList();
        TsbDataTable customersDataTable = new TsbDataTable();
        EngineInterpreter engineInterpreter = new EngineInterpreter(token);
        public Customers()
        {
        }

        public Customers(string pageTitle, string subtitle)
        {
            ButtonTsb putButton = new ButtonTsb();
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
                    //await SubmitPanelSetup(selectedCustomer);
                }

            };

            this.Controls.Add(customerSearchBox, 0, 5);
            putButton.Dock = DockStyle.Top;
            putButton.Margin = new Padding(32);
            putButton.changeButtonText("Adicionar Cliente");
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


            submitPanel.Controls.Add(titlebox, 0, 0);


            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                submitPanel.Dispose();
            };


            MaterialSingleLineTextField nameField = new MaterialSingleLineTextField
            {
                Hint = "Nome Completo",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Bottom,
                Margin = new Padding
                {
                    Top = 52,
                    Bottom = 32,
                    Left = 32,
                    Right = 32
                }
            };

            submitPanel.Controls.Add(nameField, 1, 0); ;


            MaskedTextBox dateBirthField = new MaskedTextBox
            {
                Mask = "00/00/0000",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Bottom,
                Margin = new Padding
                {
                    Top = 52,
                    Bottom = 32,
                    Left = 32,
                    Right = 32
                }
            };

            submitPanel.Controls.Add(dateBirthField, 2, 0);

            MaskedTextBox cpfField = new MaskedTextBox
            {
                TextMaskFormat = MaskFormat.ExcludePromptAndLiterals,
                
                Mask = "000.000.000-00",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Bottom,
                Margin = new Padding
                {
                    Top = 52,
                    Bottom = 32,
                    Left = 32,
                    Right = 32
                }
            };

            submitPanel.Controls.Add(cpfField, 1, 1);

            MaterialSingleLineTextField cnhField = new MaterialSingleLineTextField
            {
                Hint = "CNH",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Bottom,
                Margin = new Padding
                {
                    Top = 52,
                    Bottom = 32,
                    Left = 32,
                    Right = 32
                }
            };

            submitPanel.Controls.Add(cnhField, 1, 1);

            ButtonTsb submitButton = new ButtonTsb
            {
                Text = "Cadastrar Cliente",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 52,
                    Bottom = 32,
                    Left = 32,
                    Right = 32
                }
            };

            //submitButton.Click += async (sender, e) =>
            //{
            //    Usuario usuario = new Usuario(nomeCompleto: nameField.Text, email: emailField.Text, tipo: userTypeField.Text, senha: passwordField.Text, status: true);
            //    await PostUser(usuario, null);
            //};


            submitPanel.Controls.Add(submitButton, 3, 2);


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
