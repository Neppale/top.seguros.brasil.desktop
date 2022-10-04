using MaterialSkin.Controls;
using Newtonsoft.Json;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;
using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.Models;
using Top_Seguros_Brasil_Desktop.Utils;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Users : BasePanel
    {

        private static readonly HttpClient client = new HttpClient();
        public static string deleteMessage;
        public ArrayList selectedUser = new ArrayList();
        TsbDataTable usersDataTable = new TsbDataTable();
        EngineInterpreter engineInterpreter = new EngineInterpreter(token);
        
        public Users()
        {

        }

        public Users(string pageTitle, string subTitle)
        {
            ButtonTsb putButton = new ButtonTsb();
            this.Controls.Add(putButton, 2, 9);

            MaterialSingleLineTextField userSearchBox = new MaterialSingleLineTextField
            {
                Hint = "🔎 | Buscar usuário: ",
                SelectionStart = 6,

                Dock = DockStyle.Top,
                Margin = new Padding(32),
                BackgroundImage = searchIconPath,
                BackgroundImageLayout = ImageLayout.Zoom,


            };
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
                    //string selectedId = usersDataTable.SelectedRows[0].Cells[1].Value.ToString();

                    await SubmitPanelSetup(selectedUser);
                }
                
            };

            this.Controls.Add(userSearchBox, 0, 5); ;
            putButton.Dock = DockStyle.Top;
            putButton.Margin = new Padding(32);
            putButton.changeButtonText("Adicionar Usuário");
            putButton.Click += PutButton_Click;

            SubTitle(subTitle);
            Title(pageTitle);
            InitializeComponent();

            Get();
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


            MaterialSingleLineTextField emailField = new MaterialSingleLineTextField
            {
                Hint = "Email",
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

            submitPanel.Controls.Add(emailField, 2, 0);


            TsbComboBox userTypeField = new TsbComboBox
            {
                Text = "Tipo de usuário",
                Items = { "Administrador", "Corretor" },
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

            submitPanel.Controls.Add(userTypeField, 1, 1);


            MaterialSingleLineTextField passwordField = new MaterialSingleLineTextField
            {
                Hint = "Senha",
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

            submitPanel.Controls.Add(passwordField, 2, 1); ;


            ButtonTsb submitButton = new ButtonTsb
            {
                Text = "Cadastrar Usuário",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 52,
                    Bottom = 32,
                    Left = 32,
                    Right = 32
                }
            };

            submitButton.Click += async (sender, e) =>
            {
                Usuario usuario = new Usuario(nomeCompleto: nameField.Text, email: emailField.Text, tipo: userTypeField.Text, senha: passwordField.Text, status: true);
                await Post(usuario, null);
            };


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

        private Task SubmitPanelSetup(ArrayList arrayList)
        {
            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Editar Usuário",
                Margin = new Padding(32),

            };


            submitPanel.Controls.Add(titlebox, 0, 0);


            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                selectedUser.Clear();
                submitPanel.Dispose();
            };


            MaterialSingleLineTextField nameField = new MaterialSingleLineTextField
            {
                Text = arrayList[1].ToString(),
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


            MaterialSingleLineTextField emailField = new MaterialSingleLineTextField
            {
                Text = arrayList[2].ToString(),
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

            submitPanel.Controls.Add(emailField, 2, 0);


            TsbComboBox userTypeField = new TsbComboBox
            {
                Text = arrayList[3].ToString(),
                Items = { "Administrador", "Corretor" },
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

            submitPanel.Controls.Add(userTypeField, 1, 1);


            MaterialSingleLineTextField passwordField = new MaterialSingleLineTextField
            {
                Text = "Senha",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Bottom,
                PasswordChar = '*',
                Enabled = false,
                Margin = new Padding
                {
                    Top = 52,
                    Bottom = 32,
                    Left = 32,
                    Right = 32
                }
            };

            submitPanel.Controls.Add(passwordField, 2, 1); ;


            ButtonTsb submitButton = new ButtonTsb
            {
                Text = "Editar Usuário",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 52,
                    Bottom = 32,
                    Left = 32,
                    Right = 32
                }
            };

            submitButton.Click += async (sender, e) =>
            {
                Usuario usuario = new Usuario(nomeCompleto: nameField.Text, email: emailField.Text, tipo: userTypeField.Text, senha: "Senha123-", status: true);
                await Put(usuario, selectedUser[0].ToString());
            };


            submitPanel.Controls.Add(submitButton, 3, 2);


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

        protected async void Get()
        {

            await usersDataTable.Get<Usuario>("https://tsb-api-policy-engine.herokuapp.com/usuario/");

            usersDataTable.DataBindingComplete += (sender, e) =>
            {
                usersDataTable.Columns["id_usuario"].HeaderText = "ID";
                usersDataTable.Columns["nome_completo"].HeaderText = "Nome";
                usersDataTable.Columns["email"].HeaderText = "Email";
                usersDataTable.Columns["tipo"].HeaderText = "Tipo";
                usersDataTable.Columns["senha"].Visible = false;
                usersDataTable.Columns["status"].Visible = false;
            };

            Controls.Add(usersDataTable, 0, 7);
            SetColumnSpan(usersDataTable, 3);
        }

        protected async Task Post(Usuario userData, EventHandler? e)
        {

            await usersDataTable.Post<UserInsertResponse>(userData);

            Controls.Remove(usersDataTable);

            Get();
        }
        
        protected async Task Put(Usuario userData, string id)
        {
            await usersDataTable.Put<Usuario>(userData, id);
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