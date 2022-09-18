using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Top_Seguros_Brasil_Desktop.src.Screens;
using Top_Seguros_Brasil_Desktop.src.Screens.BaseForm;
using Top_Seguros_Brasil_Desktop.src.Models;
using System.Text.Json;
using System.Runtime.CompilerServices;
using System.Reflection;
using Top_Seguros_Brasil_Desktop.src.Components;

namespace Top_Seguros_Brasil_Desktop.src.Screens.Management_Stage
{
    public partial class ManagementStage : BaseForm.BaseForm
    {
        public ManagementStage()
        {
            InitializeComponent();
        } 

        public ManagementStage(string name, string type, string token)
        {
            this.userName = name;
            this.userType = type;
            this.token = token;
            InitializeComponent();
        }

        private static readonly HttpClient client = new HttpClient();

        private async void ManagementStage_Load(object sender, EventArgs e)
        {
            string token = this.token;
            int page = 1;

            HttpClient client = new HttpClient();
            Usuario usuario = new Usuario();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var rawResponse = await client.GetAsync("https://tsb-api-policy-engine.herokuapp.com/usuario/?pageNumber=" + page);
            var stringRespose = await rawResponse.Content.ReadAsStringAsync();

            IEnumerable<Usuario> response = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(stringRespose);

            DataTable dataTable = new DataTable();

            dataGridView1.DataSource = response;


            Usuario usuarioPost = new Usuario(nomeCompleto: "Andre Gonçalvez", email: "ag@topsegurosbrasil.br", tipo: "Corretor", status: true, senha: "Senha123-");
         
            var json = JsonConvert.SerializeObject(usuarioPost);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //var content = new FormUrlEncodedContent(usuarioPost);
            var postResponse = await client.PostAsync("https://tsb-api-policy-engine.herokuapp.com/usuario/", data);

            var postResponseString = await postResponse.Content.ReadAsStringAsync();

            if (postResponse.IsSuccessStatusCode)
            {
                MessageBox.Show(postResponse.ReasonPhrase);
            }

            this.Controls.Add(new appBar(userName, userType));
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
