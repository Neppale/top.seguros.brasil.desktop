using MaterialSkin.Controls;
using Newtonsoft.Json;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;
using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.Models;
using Top_Seguros_Brasil_Desktop.Utils;
using Windows.UI.Xaml.Controls;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Users : BasePanel
    {
        private static readonly HttpClient client = new HttpClient();
        public ArrayList selectedUser = new ArrayList();
        TsbDataTable usersDataTable = new TsbDataTable();
        EngineInterpreter engineInterpreter = new EngineInterpreter(token);

        public Users()
        {

        }

        public Users(string pageTitle, string subtitle)
        {
            ButtonTsbPrimary putButton = new ButtonTsbPrimary();
            this.Controls.Add(putButton, 2, 9);

            this.usersDataTable.CellClick += async (sender, e) =>
            {

                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == usersDataTable.Columns["Editar"].Index && e.RowIndex >= 0)
                {
                    for (int i = 0; i < usersDataTable.Columns.Count; i++)
                    {
                        selectedUser.Add(usersDataTable.SelectedRows[0].Cells[i].Value.ToString());
                    }
                    await SubmitPanelSetup(selectedUser);
                }
                
            };

            

            putButton.Dock = DockStyle.Top;
            putButton.Margin = new Padding(32);
            putButton.Text = "+ ADICIONAR USUÁRIO";
            putButton.Click += PutButton_Click;

            SubTitle(subtitle);
            Title(pageTitle);
            InitializeComponent();

            GetUsers();
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
                LabelText = "Nome completo",
                HintText = "Nome Completo do usuário aqui",
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


            TsbInput emailField = new TsbInput
            {
                LabelText = "Email",
                HintText = "Email do usuário aqui",
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

            submitPanel.Controls.Add(emailField, 2, 1);


            TsbComboBox userTypeField = new TsbComboBox
            {
                LabelText = "Tipo",
                HintText = "Tipo de usuário",
                Items = { "Administrador", "Corretor" },
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

            submitPanel.Controls.Add(userTypeField, 1, 2);


            TsbInput passwordField = new TsbInput
            {
                LabelText = "Senha padrão",
                Text = "Senha123-",
                Enabled = false,
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

            submitPanel.Controls.Add(passwordField, 2, 2); ;


            ButtonTsbPrimary submitButton = new ButtonTsbPrimary
            {
                Text = "Finalizar Cadastro do Usuário",
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
                Usuario usuario = new Usuario(nomeCompleto: nameField.Text, email: emailField.Text, tipo: userTypeField.SelectedItem.ToString(), senha: passwordField.Text, status: true);
                await PostUser(usuario, null);
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

        private Task SubmitPanelSetup(ArrayList arrayList)
        {
            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Editar Usuário",
                subtitleText = "",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(titlebox, 0, 1);


            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                selectedUser.Clear();
                submitPanel.Dispose();
            };


            TsbInput nameField = new TsbInput
            {
                LabelText = "Nome completo",
                Text = arrayList[1].ToString(),
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


            TsbInput emailField = new TsbInput
            {
                LabelText = "Email",
                Text = arrayList[2].ToString(),
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

            submitPanel.Controls.Add(emailField, 2, 1);


            TsbComboBox userTypeField = new TsbComboBox
            {
                LabelText = "Tipo",
                HintText = "Tipo de usuário",
                Text = arrayList[3].ToString(),
                Items = { "Administrador", "Corretor" },
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

            submitPanel.Controls.Add(userTypeField, 1, 2);


            TsbInput passwordField = new TsbInput
            {
                LabelText = "Senha",
                Text = "*******",
                ForeColor = TsbColor.neutralGray,
                Enabled = false,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };

            submitPanel.Controls.Add(passwordField, 2, 2); ;


            ButtonTsbPrimary submitButton = new ButtonTsbPrimary
            {
                Text = "Editar Usuário",
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
                Usuario usuario = new Usuario(nomeCompleto: nameField.Text, email: emailField.Text, tipo: userTypeField.Text, senha: "Senha123-", status: true);
                await PutUser(usuario, selectedUser[0].ToString());
            };


            submitPanel.Controls.Add(submitButton, 3, 3);


            if (Controls.OfType<SubmitPanel>().Count() != 0)
            {
                return Task.CompletedTask;
            }


            FindForm().Controls.Add(submitPanel);
            submitPanel.BringToFront();
            submitPanel.Show();
            submitPanel.Visible = true;

            return Task.CompletedTask;
        }
        
        private void PutButton_Click(object? sender, EventArgs? e)
        {
            SubmitPanelSetup();
        }

        protected async void GetUsers()
        {

            await usersDataTable.Get<PaginatedResponse<dynamic>>("https://tsb-api-policy-engine.herokuapp.com/usuario/", null, null);

            usersDataTable.DataBindingComplete += (sender, e) =>
            {
                string[] columns = { "Senha", "Status", "Detalhes" };
                usersDataTable.RemoveColumns(columns);
            };

            Controls.Add(usersDataTable, 0, 5);
            SetColumnSpan(usersDataTable, 3);
            SetRowSpan(usersDataTable, 4);
        }

        protected async Task PostUser(Usuario userData, EventHandler? e)
        {

            await usersDataTable.Post<UserInsertResponse>(userData);

        }

        protected async Task PutUser(Usuario userData, string id)
        {
            await usersDataTable.Put<Usuario>(userData, id, null);
        }

        public Users(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
    }
}
public class UserInsertResponse
{
    public Usuario? usuario { get; set; }
    public string? message { get; set; }
}

