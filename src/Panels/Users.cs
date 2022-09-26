using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.Models;
using Top_Seguros_Brasil_Desktop.src.Screens.Management_Stage;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Top_Seguros_Brasil_Desktop.Utils;
using Microsoft.VisualBasic.ApplicationServices;
using System.Data.Common;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Collections;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Users : BasePanel
    {

        private static readonly HttpClient client = new HttpClient();
        

        EngineInterpreter engineInterpreter = new EngineInterpreter(token);

        ButtonTsb submit = new ButtonTsb();
        TextBox nameBox = new TextBox();
        TextBox emailBox = new TextBox();
        TextBox typeBox = new TextBox();

        TextBox idBox = new TextBox();
        ButtonTsb Deletar = new ButtonTsb();

        TextBox nomePut = new TextBox();
        TextBox senhaPut = new TextBox();
        TextBox emailPut = new TextBox();
        TextBox tipoPut = new TextBox();
        TextBox idPut = new TextBox();
        ButtonTsb putButton = new ButtonTsb();


        DataTable dataTable = new DataTable();
        TsbDataTable userDataTable = new TsbDataTable();
        BindingSource source = new BindingSource();

        public Users()
        {
            Get();

            submit.Click += new EventHandler(Submit_OnClick);
            this.Controls.Add(submit);

            this.Controls.Add(nameBox);
            nameBox.Location = new Point(342, 700);
            nameBox.PlaceholderText = "Nome";

            this.Controls.Add(emailBox);
            emailBox.Location = new Point(600, 700);
            emailBox.PlaceholderText = "email";

            this.Controls.Add(typeBox);
            typeBox.Location = new Point(900, 700);
            typeBox.PlaceholderText = "Tipo";


            this.Controls.Add(nomePut);
            nomePut.Location = new Point(32, 112);
            nomePut.PlaceholderText = "Nome";


            this.Controls.Add(senhaPut);
            senhaPut.Location = new Point(321, 112);
            senhaPut.PlaceholderText = "Senha";

            this.Controls.Add(emailPut);
            emailPut.Location = new Point(601, 112);
            emailPut.PlaceholderText = "Email";

            this.Controls.Add(tipoPut);
            tipoPut.Location = new Point(885, 112);
            tipoPut.PlaceholderText = "Tipo";

            this.Controls.Add(idPut);
            idPut.Location = new Point(32, 176);
            idPut.PlaceholderText = "id";
            idPut.Enabled = false;

            this.Controls.Add(putButton);
            putButton.Location = new Point(601, 176);
            putButton.changeButtonText("Editar");
            putButton.Click += PutButton_Click;

            userDataTable.CellClick += new DataGridViewCellEventHandler(DeleteButton_Click);
            userDataTable.CellClick += new DataGridViewCellEventHandler(EditButton_Click);

            submit.changeButtonText("Cadastrar");
            this.Controls.Add(userDataTable);

            InitializeComponent();
        }


        private void DeleteButton_Click(object? sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == userDataTable.Columns["Deletar"].Index && e.RowIndex >= 0)
            {
                string selectedId = userDataTable.SelectedRows[0].Cells[2].Value.ToString();
                Delete(selectedId);
            }
        }

        private void EditButton_Click(object? sender, DataGridViewCellEventArgs e)
        {
            ArrayList rowValues = new ArrayList();

            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == userDataTable.Columns["Editar"].Index && e.RowIndex >= 0)
            {
                idPut.Text = "";
                nomePut.Text = "";
                emailBox.Text = "";
                tipoPut.Text = "";
                senhaPut.Text = "";

                for (int i = 0; i < userDataTable.Columns.Count; i++)
                {
                    rowValues.Add(userDataTable.SelectedRows[0].Cells[i].Value.ToString());
                }

                nomePut.Text = rowValues[3].ToString();
                senhaPut.Text = "Senha123-";
                emailPut.Text = rowValues[4].ToString();
                tipoPut.Text = rowValues[5].ToString();
                idPut.Text = rowValues[2].ToString();
            }
        }

        private void PutButton_Click(object? sender, EventArgs e)
        {
            Put();
        }

        private void Delete_OnClick(object sender, EventArgs e)
        {
            Delete(userDataTable.getSelectedId());
            Get();
        }

        private void Submit_OnClick(object sender, EventArgs e)
        {
            Post();
        }
        

        public async void Get()
        {
            int page = 2;
            var response = await engineInterpreter.Request<IEnumerable<Usuario>>($"https://tsb-api-policy-engine.herokuapp.com/usuario/?pageNumber={page}", "GET", null);

            IEnumerable<Usuario> responseBody = response.Body;

            string[] properties = responseBody.First().GetType().GetProperties().Select(x => x.Name).ToArray();

            try
            {
                foreach (var property in properties)
                {

                    if (property != "senha" && property != "status")
                    {
                        dataTable.Columns.Add(property);
                    }
                }

                foreach (var item in responseBody)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (var property in properties)
                    {
                        if (property != "senha" && property != "status")
                        {
                            row[property] = item.GetType().GetProperty(property).GetValue(item, null);
                        }
                    }
                    dataTable.Rows.Add(row);
                }

                dataTable.Columns["id_usuario"].ColumnName = "Identificação";
                dataTable.Columns["nome_completo"].ColumnName = "Nome";
                dataTable.Columns["email"].ColumnName = "Email";
                dataTable.Columns["tipo"].ColumnName = "Tipo";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            userDataTable.LoadData(dataTable);
        }


        public async void ReloadUserTable()
        {

            dataTable.Columns["Identificação"].ColumnName = "id_usuario";
            dataTable.Columns["Nome"].ColumnName = "nome_completo";
            dataTable.Columns["Email"].ColumnName = "email";
            dataTable.Columns["Tipo"].ColumnName = "tipo";

            dataTable.Clear();
            int page = 2;
            var response = await engineInterpreter.Request<IEnumerable<Usuario>>($"https://tsb-api-policy-engine.herokuapp.com/usuario/?pageNumber={page}", "GET", null);
            IEnumerable<Usuario> responseBody = response.Body;
            string[] properties = responseBody.First().GetType().GetProperties().Select(x => x.Name).ToArray();

            try
            {

                foreach (var item in responseBody)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (var property in properties)
                    {
                        if (property != "senha" && property != "status")
                        {
                            row[property] = item.GetType().GetProperty(property).GetValue(item, null);
                        }
                    }
                    dataTable.Rows.Add(row);
                }

                dataTable.Columns["id_usuario"].ColumnName = "Identificação";
                dataTable.Columns["nome_completo"].ColumnName = "Nome";
                dataTable.Columns["email"].ColumnName = "Email";
                dataTable.Columns["tipo"].ColumnName = "Tipo";
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        

        protected async void Post()
        {
            Usuario usuario = new Usuario(nomeCompleto: nameBox.Text, email: emailBox.Text, tipo: typeBox.Text, senha: "Senha123-");
            var json = JsonConvert.SerializeObject(usuario);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response =  await engineInterpreter.Request<Usuario>("https://tsb-api-policy-engine.herokuapp.com/usuario/", "POST", data);
            Usuario responseUser = response.Body;
            ReloadUserTable();
        }
        
        protected async void Put()
        {
            Usuario usuario = new Usuario(nomeCompleto: nomePut.Text, email: emailPut.Text, tipo: tipoPut.Text, senha: "Senha123-");
            var json = JsonConvert.SerializeObject(usuario);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await engineInterpreter.Request<Usuario>($"https://tsb-api-policy-engine.herokuapp.com/usuario/{idPut.Text}", "PUT", data);
            Usuario responseUser = response.Body;
            ReloadUserTable();

            idPut.Text = "";
            nomePut.Text = "";
            emailBox.Text = "";
            tipoPut.Text = "";
            senhaPut.Text = "";

        }
        
        protected async void Delete(string id)
        {
            var response = await engineInterpreter.Request <IEnumerable<Usuario>>($"https://tsb-api-policy-engine.herokuapp.com/usuario/{id}", "DELETE", null);
            dataTable.Dispose();
            ReloadUserTable();
            
        }
        

        public Users(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

    }
}