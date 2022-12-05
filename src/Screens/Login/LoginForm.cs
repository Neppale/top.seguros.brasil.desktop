using Newtonsoft.Json;
using System.Text;
using Top_Seguros_Brasil_Desktop.src.Models;
using Top_Seguros_Brasil_Desktop.src.Panels;
using Top_Seguros_Brasil_Desktop.src.Screens.Management_Stage;
using Top_Seguros_Brasil_Desktop.Utils;

namespace Top_Seguros_Brasil_Desktop
{
    public partial class LoginForm : Form
    {
        public class LoginAccess
        {
            public string email { get; set; }
            public string senha { get; set; }
            public string token { get; set; }


            public LoginAccess(string email, string senha)
            {
                this.email = email;
                this.senha = senha;
            }
        }

        public LoginForm()
        {
            InitializeComponent();
            this.ActiveControl = pictureBoxLogo;

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            emailInput.Location = new System.Drawing.Point(555, 584);
            passwordInput.Location = new System.Drawing.Point(555, 672);
            illustrationLogin.Location = new System.Drawing.Point(592, 280);
            buttonLogin.Text = "ENTRAR";
            buttonLogin.Enabled = false;
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {

            //emailInput.Text = "jonathan.santos@topseguros.br";
            //passwordInput.Text = "Senha123-";
            
            LoginAccess loginAccess = new LoginAccess(email: emailInput.Text, senha: passwordInput.Text);
            var json = JsonConvert.SerializeObject(loginAccess);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var engineInterpreter = new EngineInterpreter();
            var response = await engineInterpreter.Request<UserLoginResponse>("https://tsb-api-policy-engine.herokuapp.com/usuario/login", "POST", data);
            UserLoginResponse responseBody = response.Body;

            if (response.StatusCode != 200)
            {
                MessageBox.Show(responseBody.message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

                ManagementStage managementStage = new ManagementStage(responseBody.user.nome_completo, responseBody.user.tipo, responseBody.token);

            BasePanel basePanel = new BasePanel(responseBody.user.nome_completo, responseBody.user.tipo, responseBody.user.id_usuario, responseBody.user.email);
            BasePanel.token = responseBody.token;
            BasePanel.userId = responseBody.user.id_usuario;
            
            managementStage.Show();
            this.Hide();

        }

        private void passwordInput_Enter(object sender, EventArgs e)
        {
            buttonLogin.Enabled = true;
        }
    }
}


public class UserLoginResponse
{

    public Usuario? user { get; set; }
    public string? message { get; set; }
    public string? token { get; set; }

}

