using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Top_Seguros_Brasil_Desktop.src.Screens;
using Top_Seguros_Brasil_Desktop.src.Screens.BaseForm;

namespace Top_Seguros_Brasil_Desktop.src.Screens.Management_Stage
{
    public partial class ManagementStage : BaseForm.BaseForm
    {
        public ManagementStage()
        {
            InitializeComponent();
        }

        private static readonly HttpClient client = new HttpClient();

        private async void ManagementStage_Load(object sender, EventArgs e)
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJ0b3BzZWd1cm9zLmJyIiwiYXVkIjoidG9wc2VndXJvcy5iciJ9.BlgdXdY_wv06AbGtlBPRpeXs-EyGryp-20iK3lN0HG8";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var responseString = await client.GetStringAsync("https://tsb-api-policy-engine.herokuapp.com/usuario/");

            MessageBox.Show(responseString);

        }

        

    }
}
