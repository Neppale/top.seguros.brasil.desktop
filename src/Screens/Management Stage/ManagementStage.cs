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

namespace Top_Seguros_Brasil_Desktop.src.Screens.Management_Stage
{
    public partial class ManagementStage : BaseForm.BaseForm
    {
        public ManagementStage()
        {
            InitializeComponent();
        } 

        public ManagementStage(string token)
        {
            this.token = token;
            InitializeComponent();
        }

        private static readonly HttpClient client = new HttpClient();

        private async void ManagementStage_Load(object sender, EventArgs e)
        {

            string token = this.token;

            HttpClient client = new HttpClient();
            Usuario usuario = new Usuario();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var rawResponse = await client.GetAsync("https://tsb-api-policy-engine.herokuapp.com/usuario/");
            var stringRespose = await rawResponse.Content.ReadAsStringAsync();

            IEnumerable<Usuario> response = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(stringRespose);

            DataTable dataTable = new DataTable();

            dataGridView1.DataSource = response;

        }
       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
