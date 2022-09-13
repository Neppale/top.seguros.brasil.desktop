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
using System.Net.Mime;
using Top_Seguros_Brasil_Desktop.src.Screens.Components;

namespace Top_Seguros_Brasil_Desktop
{
  public partial class LoginForm : Form
  {
    public class LoginAccess
    {
      public string email { get; set; }
      public string senha { get; set; }
      public LoginAccess(string email, string senha)
      {
        this.email = email;
        this.senha = senha;
      }
    }

    public LoginForm()
    {
      InitializeComponent();


    }

        public static string TestData = "Teste de string de outro form.";



    private void LoginForm_Load(object sender, EventArgs e)
    {
      
            emailInput.Location = new System.Drawing.Point(555, 584);
      passwordInput.Location = new System.Drawing.Point(555, 672);
      illustrationLogin.Location = new System.Drawing.Point(592, 280);
      buttonLogin.changeButtonText("Entrar");
            pictureBoxLogo.Hide();
            SideNav sideNav = new SideNav();
            this.Controls.Add(sideNav);
            
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

      /*
       *  THIS METHOD IS DEPRECATED.
       * 
       * LoginAccess loginAccess = new LoginAccess(email: emailInput.Text, senha: passwordInput.Text);

      Button clickedButton = (Button)sender;

          var httpClient = new HttpClient();

          var request = new HttpRequestMessage();

          var json = JsonConvert.SerializeObject(loginAccess);
          var data = new StringContent(json, Encoding.UTF8, "application/json");


          var rawResponse = await httpClient.PostAsync("https://tsb-api-policy-engine.herokuapp.com/usuario/login", data);
          var stringResponse = await rawResponse.Content.ReadAsStringAsync();

          var response = JsonConvert.DeserializeObject<UserLoginResponse>(stringResponse); 

          if (!rawResponse.IsSuccessStatusCode)
          {
              emailInput.Text = response?.message;
              return;
          }

          Home home = new Home();
          home.Show();
          this.Hide();*/
    }

    private async void buttonLogin_Click(object sender, EventArgs e)
    {
      LoginAccess loginAccess = new LoginAccess(email: emailInput.Text, senha: passwordInput.Text);

      var httpClient = new HttpClient();

      var request = new HttpRequestMessage();

      var json = JsonConvert.SerializeObject(loginAccess);
      var data = new StringContent(json, Encoding.UTF8, "application/json");


      var rawResponse = await httpClient.PostAsync("https://tsb-api-policy-engine.herokuapp.com/usuario/login", data);
      var stringResponse = await rawResponse.Content.ReadAsStringAsync();

      var response = JsonConvert.DeserializeObject<UserLoginResponse>(stringResponse);

      if (!rawResponse.IsSuccessStatusCode)
      {
        emailInput.Text = response?.message;
        return;
      }

      Home home = new Home();
      home.Show();
      this.Hide();
    }

    private void pictureBox2_Click(object sender, EventArgs e)
    {

    }

        private void pictureBoxLogo_Click(object sender, EventArgs e)
        {

        }
    }
}

public class Usuario
{
  public int id_usuario { get; set; }
  public string nome_completo { get; set; }
  public string email { get; set; }
  public string tipo { get; set; }
  public bool status { get; set; }

}

public class UserLoginResponse
{
  public Usuario? user { get; set; }
  public string? message { get; set; }
  public string? token { get; set; }
}

