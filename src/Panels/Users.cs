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
            Delete();
        }

        private void Submit_OnClick(object sender, EventArgs e)
        {
            Post();
        }
        

        protected async void Get()
        {
           
            int page = 2;
            var response = await engineInterpreter.Request<IEnumerable<Usuario>>($"https://tsb-api-policy-engine.herokuapp.com/usuario/?pageNumber={page}", "GET", null);

            DataTable dataTable = new DataTable();

            IEnumerable<Usuario> responseBody = response.Body;

            string[] properties = responseBody.First().GetType().GetProperties().Select(x => x.Name).ToArray();

            
            
            //add dataTable columns according to the properties of the object, except the id, senha and tipo
            foreach (var property in properties)
            {
                if (property != "id_usuario" && property != "senha" && property != "status" )
                {
                    dataTable.Columns.Add(property);
                }
            }

            //fill the dataTable with the values of the object
            foreach (var item in responseBody)
            {
                DataRow row = dataTable.NewRow();
                foreach (var property in properties)
                {
                    if (property != "id_usuario" && property != "senha" && property != "status")
                    {
                        row[property] = item.GetType().GetProperty(property).GetValue(item, null);
                    }
                }
                dataTable.Rows.Add(row);
            }
                
            this.Controls.Add(new TsbDataTable(dataTable));

        }

        protected async void Post()
        {
            Usuario usuario = new Usuario(nomeCompleto: nameBox.Text, email: emailBox.Text, tipo: typeBox.Text, senha: "Senha123-");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(usuario);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://tsb-api-policy-engine.herokuapp.com/usuario/", data);

            

            var responseString = await response.Content.ReadAsStringAsync();

            Get();

            MessageBox.Show(responseString);
        }


        protected async void Put()
        {

        }

        protected async void Delete()
        {
            var response = await client.DeleteAsync($"https://tsb-api-policy-engine.herokuapp.com/usuario/{idBox.Text}");
            var responseString = await response.Content.ReadAsStringAsync();
            MessageBox.Show(responseString);
        }
        

        public Users(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
    }
}
