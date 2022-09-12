using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Top_Seguros_Brasil_Desktop.Properties;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Mime;

namespace Top_Seguros_Brasil_Desktop
{
    public partial class LoginForm : Form
    {
        public class LoginAccess
        {
            public string email { get; set; }
            public string senha { get; set; }

        }

        public LoginForm()
        {
            InitializeComponent();

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            emailInput.Location = new System.Drawing.Point(555, 584);
            passwordInput.Location = new System.Drawing.Point(555, 672);
            illustrationLogin.Location = new System.Drawing.Point(592, 280);
            buttonLogin.changeButtonText("Entrar");
        }

        private async void loginBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void emailInput_Click(object sender, EventArgs e)
        {

        }

        private async void loginBtn_Click_1(object sender, EventArgs e)
        {
            
        }

        private void tsbButton1_Load(object sender, EventArgs e)
        {
            
        }
        

        private async void tsbButton1_Click(object sender, EventArgs e)
        {

            LoginAccess loginAccess = new LoginAccess();

            Button clickedButton = (Button)sender;

            MessageBox.Show("Funcionou porra!");
            
            try
            {

                var httpClient = new HttpClient();

                var request = new HttpRequestMessage();


                loginAccess.email = emailInput.Text;
                loginAccess.senha = passwordInput.Text;


                var json = JsonConvert.SerializeObject(loginAccess);
                var data = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await httpClient.PostAsync("https://tsb-api-policy-engine.herokuapp.com/usuario/login", data);

                Home home = new Home();
                home.Show();
                this.Hide();


                if (response.IsSuccessStatusCode)
                {

                }

            }
            catch (Exception x)
            {
                //textBox1.Text = x.Message;
            }


        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            LoginAccess loginAccess = new LoginAccess();

            try
            {

                var httpClient = new HttpClient();

                var request = new HttpRequestMessage();


                loginAccess.email = emailInput.Text;
                loginAccess.senha = passwordInput.Text;


                var json = JsonConvert.SerializeObject(loginAccess);
                var data = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await httpClient.PostAsync("https://tsb-api-policy-engine.herokuapp.com/usuario/login", data);


                if (response.IsSuccessStatusCode)
                {
                    Home home = new Home();
                    home.Show();
                    this.Hide();
                }

            }
            catch (Exception x)
            {
                //textBox1.Text = x.Message;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
