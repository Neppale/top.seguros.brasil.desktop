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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Collections;
using Top_Seguros_Brasil_Desktop.src.Screens.Components;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
  public partial class Users : BasePanel
  {

    private static readonly HttpClient client = new HttpClient();
    public static string deleteMessage;
    TsbDataTable usersDataTable = new TsbDataTable();
    EngineInterpreter engineInterpreter = new EngineInterpreter(token);
    ButtonTsb submit = new ButtonTsb();
    TextBox nameBox = new TextBox();
    TextBox emailBox = new TextBox();
    TextBox typeBox = new TextBox();
    TextBox idBox = new TextBox();
    ButtonTsb Deletar = new ButtonTsb();

    TextBox nomePut = new TextBox();
    TextBox senhaPut = new TextBox();
    TextBox emailPut = new TextBox();
    TextBox tipoPut = new TextBox();
    TextBox idPut = new TextBox();
    ButtonTsb putButton = new ButtonTsb();
    DataTable dataTable = new DataTable();
    BindingSource source = new BindingSource();

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
    }

    public Users(string pageTitle, string subTitle)
    {
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


      this.Controls.Add(senhaPut);
      senhaPut.Location = new Point(321, 112);
      senhaPut.PlaceholderText = "Senha";

      this.Controls.Add(emailPut);
      emailPut.Location = new Point(601, 112);
      emailPut.PlaceholderText = "Email";

      this.Controls.Add(tipoPut);
      tipoPut.Location = new Point(885, 112);
      tipoPut.PlaceholderText = "Tipo";

      this.Controls.Add(idPut);
      idPut.Location = new Point(32, 176);
      idPut.PlaceholderText = "id";
      idPut.Enabled = false;

      this.Controls.Add(putButton);
      putButton.Location = new Point(601, 176);
      putButton.changeButtonText("Editar");
      putButton.Click += PutButton_Click;

      this.Controls.Add(idPut);
      idPut.Location = new Point(32, 176);
      idPut.PlaceholderText = "id";
      idPut.Enabled = false;

      this.Controls.Add(putButton);
      putButton.Location = new Point(601, 176);
      putButton.changeButtonText("Editar");
      putButton.Click += PutButton_Click;

      submit.changeButtonText("Cadastrar");

      Title(pageTitle);
      SubTitle(subTitle);

      InitializeComponent();

      Get();
    }

    private void PutButton_Click(object? sender, EventArgs e)
    {
      Put();
    }

    private void Submit_OnClick(object sender, EventArgs e)
    {
      Post();
    }


    protected async void Get()
    {

      await usersDataTable.Get<Usuario>("https://tsb-api-policy-engine.herokuapp.com/usuario/");

      usersDataTable.DataBindingComplete += (sender, e) =>
      {
        usersDataTable.Columns["id_usuario"].HeaderText = "ID";
        usersDataTable.Columns["nome_completo"].HeaderText = "Nome";
        usersDataTable.Columns["email"].HeaderText = "Email";
        usersDataTable.Columns["tipo"].HeaderText = "Tipo";
        usersDataTable.Columns["senha"].Visible = false;
        usersDataTable.Columns["status"].Visible = false;
      };

      Controls.Add(usersDataTable);
    }

    protected async Task Post()
    {

      Usuario usuario = new Usuario(nomeCompleto: nameBox.Text, email: emailBox.Text, tipo: typeBox.Text, senha: "Senha123-", status: true);

      await usersDataTable.Post<UserInsertResponse>(usuario);

      Controls.Remove(usersDataTable);

      Get();
    }

    protected async Task Put()
    {


      Usuario usuario = new Usuario(nomeCompleto: nomePut.Text, email: emailPut.Text, tipo: tipoPut.Text, senha: "Senha123-");
      var json = JsonConvert.SerializeObject(usuario);
      var data = new StringContent(json, Encoding.UTF8, "application/json");
      var response = await engineInterpreter.Request<Usuario>($"https://tsb-api-policy-engine.herokuapp.com/usuario/{idPut.Text}", "PUT", data);
      Usuario responseUser = response.Body;

      await usersDataTable.Put<Usuario>(usuario);

      idPut.Text = "";
      nomePut.Text = "";
      emailBox.Text = "";
      tipoPut.Text = "";
      senhaPut.Text = "";

    }
    public Users(IContainer container)
    {
      container.Add(this);
      InitializeComponent();
    }
  }
}
public class UserInsertResponse
{
  public Usuario? usuario { get; set; }
  public string? message { get; set; }
}