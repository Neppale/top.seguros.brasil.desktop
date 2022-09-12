using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Mime;

namespace Top_Seguros_Brasil_Desktop
{
    public partial class Login : UserControl
    {

        public class LoginAccess
        {
            public string email { get; set; }
            public string senha { get; set; }

        }

        public Login()
        {
            InitializeComponent();

        }

        private static readonly HttpClient client = new HttpClient();

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void emailInput_Click(object sender, EventArgs e)
        {

        }

        private async void loginBtn_Click(object sender, EventArgs e)
        {

            LoginAccess loginAccess = new LoginAccess();

            try
            {

                var httpClient = new HttpClient();

                var request = new HttpRequestMessage();


                //loginAccess.email = emailInput.Text;
                //loginAccess.senha = passwordInput.Text;


                var json = JsonConvert.SerializeObject(loginAccess);
                var data = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await httpClient.PostAsync("https://tsb-api-policy-engine.herokuapp.com/usuario/login", data);

                //textBox1.Text = response.ToString() + json + data;

                if (!response.IsSuccessStatusCode)
                {
                    //textBox1.Text = await response.Content.ReadAsStringAsync();
                }
                
            }catch(Exception x)
            {
                //textBox1.Text = x.Message;
            }

        }

        private void Password_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
