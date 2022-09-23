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
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Top_Seguros_Brasil_Desktop.src.Screens.Management_Stage;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Users : BasePanel
    {
        string token = BasePanel.token;

        private static readonly HttpClient client = new HttpClient();

        ButtonTsb submit = new ButtonTsb();
        TextBox nameBox = new TextBox();
        TextBox emailBox = new TextBox();
        TextBox typeBox = new TextBox();

        TextBox idBox = new TextBox();
        ButtonTsb Deletar = new ButtonTsb();
        ButtonTsb Adicionar = new ButtonTsb();
        formCadastro formCadastro = new formCadastro();


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

            this.Controls.Add(Adicionar);
            Adicionar.Location = new Point(921, 715);
            Adicionar.changeButtonText("Adicionar usuário");
            Adicionar.Click += new EventHandler(Adicionar_usuario_click);

            formCadastro.Hide();

            this.Controls.Add(formCadastro);
        }


        private void Delete_OnClick(object sender, EventArgs e)
        {
            Delete();
        }

        private void Submit_OnClick(object sender, EventArgs e)
        {
            Post();
        }

        private void Adicionar_usuario_click(object sender, EventArgs e)
        {
            //MessageBox.Show(Controls.GetChildIndex(formCadastro).ToString());

            formCadastro.Show();
            
            Controls.SetChildIndex(formCadastro, 0);
        }

        protected async void Get()
        {   
            int page = 2;
            Usuario usuario = new Usuario();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var rawResponse = await client.GetAsync("https://tsb-api-policy-engine.herokuapp.com/usuario/?pageNumber=" + page);
            var stringRespose = await rawResponse.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<JArray>(stringRespose).ToList();

            List<Usuario> users = new List<Usuario>();

            json.ForEach(ele =>
            {
                JObject usu = ele as JObject;

                Usuario usuario = new Usuario(usu["nome_completo"].ToString(), usu["email"].ToString(), usu["tipo"].ToString(), "");
                usuario.id_usuario = int.Parse(usu["id_usuario"].ToString());

                users.Add(usuario);

            });

            submit.changeButtonText("Cadastrar");

            this.Controls.Add(new TsbDataTable(users));
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
