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
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Users : BasePanel
    {

        private static readonly HttpClient client = new HttpClient();
        private static bool IsLoaded = false;

        EngineInterpreter engineInterpreter = new EngineInterpreter(token);

        ButtonTsb submit = new ButtonTsb();
        TextBox nameBox = new TextBox();
        TextBox emailBox = new TextBox();
        TextBox typeBox = new TextBox();

        TextBox idBox = new TextBox();
        ButtonTsb Deletar = new ButtonTsb();

        static DataTable dataTable = new DataTable();
        TsbDataTable userDataTable = new TsbDataTable(dataTable);

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

            this.Controls.Add(idBox);
            idBox.Location = new Point(900, 0);
            idBox.PlaceholderText = "id";

            Deletar.Click += new EventHandler(Delete_OnClick);
            this.Controls.Add(Deletar);
            Deletar.Location = new Point(600, 0);
            Deletar.changeButtonText("Deletar usuário");
            
            submit.changeButtonText("Cadastrar");



        }


        private void Delete_OnClick(object sender, EventArgs e)
        {
            Delete(userDataTable.getSelectedId());
        }

        private void Submit_OnClick(object sender, EventArgs e)
        {
            Post();
        }
        

        protected async void Get()
        {

            int page = 2;
            var response = await engineInterpreter.Request<IEnumerable<Usuario>>($"https://tsb-api-policy-engine.herokuapp.com/usuario/?pageNumber={page}", "GET", null);

            
            IEnumerable<Usuario> responseBody = response.Body;

            string[] properties = responseBody.First().GetType().GetProperties().Select(x => x.Name).ToArray();
            


            if(IsLoaded == false)
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

                IsLoaded = true;
                
            }

            this.Controls.Add(userDataTable);
        }

        protected async void Post()
        {
            Usuario usuario = new Usuario(nomeCompleto: nameBox.Text, email: emailBox.Text, tipo: typeBox.Text, senha: "Senha123-");

            //string[] newUserRow = new string[] { nameBox.Text, emailBox.Text, typeBox.Text };
            
            //userDataTable.Rows.Add(newUserRow);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(usuario);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://tsb-api-policy-engine.herokuapp.com/usuario/", data);
            
            var responseString = await response.Content.ReadAsStringAsync();


            MessageBox.Show(responseString);

            this.Controls.Remove(userDataTable);
            Get();
        }


        protected async void Put()
        {
        }
        protected async void Delete(string id)
        {
            var response = await engineInterpreter.Request <IEnumerable<Usuario>>($"https://tsb-api-policy-engine.herokuapp.com/usuario/{id}", "DELETE", null);

            userDataTable.removeRow(int.Parse(id));
            Get();
        }
        

        public Users(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
    }
}
