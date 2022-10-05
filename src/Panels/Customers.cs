using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.Models;
using Top_Seguros_Brasil_Desktop.Utils;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
  public partial class Customers : BasePanel
  {

    EngineInterpreter engineInterpreter = new EngineInterpreter(token);

    TextBox nomePost = new TextBox();
    TextBox emailPost = new TextBox();
    TextBox senhaPost = new TextBox();
    TextBox cpfPost = new TextBox();
    TextBox cnhPost = new TextBox();
    TextBox cepPost = new TextBox();
    TextBox dataNsPost = new TextBox();
    TextBox tel1Post = new TextBox();
    TextBox tel2Post = new TextBox();
    ButtonTsb postButton = new ButtonTsb();


    TextBox idPut = new TextBox();
    TextBox nomePut = new TextBox();
    TextBox emailPut = new TextBox();
    TextBox senhaPut = new TextBox();
    TextBox cpfPut = new TextBox();
    TextBox cnhPut = new TextBox();
    TextBox cepPut = new TextBox();
    TextBox dataNsPut = new TextBox();
    TextBox tel1Put = new TextBox();
    TextBox tel2Put = new TextBox();
    TextBox statusPut = new TextBox();
    ButtonTsb putButton = new ButtonTsb();

    Panel editPanel = new Panel();

    public Customers()
    {

      editPanel.Controls.Add(idPut);
      idPut.Location = new Point(0, 0);
      idPut.PlaceholderText = "id";
      idPut.Enabled = false;

      editPanel.Controls.Add(nomePut);
      nomePut.Location = new Point(179, 0);
      nomePut.PlaceholderText = "nome";

      editPanel.Controls.Add(emailPut);
      emailPut.Location = new Point(358, 0);
      emailPut.PlaceholderText = "email";

      editPanel.Controls.Add(senhaPut);
      senhaPut.Location = new Point(537, 0);
      senhaPut.PlaceholderText = "senha";

      editPanel.Controls.Add(cpfPut);
      cpfPut.Location = new Point(716, 0);
      cpfPut.PlaceholderText = "cpf";

      editPanel.Controls.Add(cnhPut);
      cnhPut.Location = new Point(895, 0);
      cnhPut.PlaceholderText = "cnh";



      editPanel.Controls.Add(cepPut);
      cepPut.Location = new Point(0, 79);
      cepPut.PlaceholderText = "cep";

      editPanel.Controls.Add(dataNsPut);
      dataNsPut.Location = new Point(179, 79);
      dataNsPut.PlaceholderText = "data de nascimento";

      editPanel.Controls.Add(tel1Put);
      tel1Put.Location = new Point(358, 79);
      tel1Put.PlaceholderText = "telefone 1";

      editPanel.Controls.Add(tel2Put);
      tel2Put.Location = new Point(537, 79);
      tel2Put.PlaceholderText = "telefone 2";

      editPanel.Controls.Add(statusPut);
      statusPut.Location = new Point(715, 79);
      statusPut.Text = "Status";

      editPanel.Controls.Add(putButton);
      putButton.Location = new Point(716, 170);
      putButton.Text = "Atualizar";
      putButton.Click += new EventHandler(PutCustomerOnClick);

      editPanel.Location = new Point(95, 29);
      editPanel.Height = 197;
      editPanel.Width = 1053;

      nomePut.Click += new EventHandler(PanelControlCount);

      this.Controls.Add(editPanel);



      this.Controls.Add(nomePost);
      nomePost.Location = new Point(253, 666);
      nomePost.PlaceholderText = "nome";

      this.Controls.Add(emailPost);
      emailPost.Location = new Point(432, 666);
      emailPost.PlaceholderText = "email";

      this.Controls.Add(senhaPost);
      senhaPost.Location = new Point(611, 666);
      senhaPost.PlaceholderText = "senha";

      this.Controls.Add(cpfPost);
      cpfPost.Location = new Point(790, 666);
      cpfPost.PlaceholderText = "cpf";

      this.Controls.Add(cnhPost);
      cnhPost.Location = new Point(969, 666);
      cnhPost.PlaceholderText = "cnh";



      this.Controls.Add(cepPost);
      cepPost.Location = new Point(253, 745);
      cepPost.PlaceholderText = "cep";

      this.Controls.Add(dataNsPost);
      dataNsPost.Location = new Point(432, 745);
      dataNsPost.PlaceholderText = "data de nascimento";

      this.Controls.Add(tel1Post);
      tel1Post.Location = new Point(611, 745);
      tel1Post.PlaceholderText = "telefone 1";


      this.Controls.Add(tel2Post);
      tel2Post.Location = new Point(790, 745);
      tel2Post.PlaceholderText = "telefone 2";

      this.Controls.Add(postButton);
      postButton.Location = new Point(962, 745);
      postButton.Text = "Cadastrar cliente";
      postButton.Click += new EventHandler(SubmitCustomerOnClick);


      InitializeComponent();
      GetCustomers();

    }


    private void PanelControlCount(object sender, EventArgs e)
    {
      ArrayList values = TsbDataTable.selectedRowValues;
      int controlNumber = editPanel.Controls.Count;

      try
      {
        if (values == null)
        {
          return;
        }

        for (int i = 0; i <= values.Count - 1; i++)
        {

          if (i == controlNumber)
          {
            break;
          }
          else
          {
            editPanel.Controls[i].Text = values[i].ToString();
          }

        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        return;
      }

    }


    private async void GetCustomers()
    {

      TsbDataTable customersTable = new TsbDataTable("https://tsb-api-policy-engine.herokuapp.com/cliente/", typeof(Cliente));

      customersTable.Refresh();

      //customersTable.Get<Cliente>();

      this.Controls.Add(customersTable);


      customersTable.DataBindingComplete += (sender, e) =>
      {

        customersTable.Columns["id_cliente"].HeaderText = "ID";
        customersTable.Columns["nome_completo"].HeaderText = "Nome";
        customersTable.Columns["email"].HeaderText = "Email";
        customersTable.Columns["telefone2"].HeaderText = "Telefone 2";
        customersTable.Columns["telefone1"].HeaderText = "Telefone 1";

        customersTable.Columns["senha"].Visible = false;
        customersTable.Columns["cnh"].Visible = false;
        customersTable.Columns["cep"].Visible = false;
        customersTable.Columns["status"].Visible = false;
        customersTable.Columns["data_nascimento"].Visible = false;

      };

    }

    private void PostCustomer()
    {
      TsbDataTable customersTable = new TsbDataTable("https://tsb-api-policy-engine.herokuapp.com/cliente/", typeof(Cliente));
      Cliente cliente = new Cliente(
          nomeCompleto: nomePost.Text,
          email: emailPost.Text,
          senha: senhaPost.Text,
          cpf: cpfPost.Text,
          cnh: cnhPost.Text,
          cep: cepPost.Text,
          dataNascimento: dataNsPost.Text,
          telefone1: tel1Post.Text,
          telefone2: tel2Post.Text,
          status: false
          );

      customersTable.Post<CustomerInsertResponse>(cliente);

      //customersTable.Get<Cliente>();
    }


    private void PutCustomer()
    {
      TsbDataTable customersTable = new TsbDataTable($"https://tsb-api-policy-engine.herokuapp.com/cliente/{idPut.Text}", typeof(Cliente));

      Cliente cliente = new Cliente(
          nomeCompleto: nomePut.Text,
          email: emailPut.Text,
          senha: senhaPut.Text,
          cpf: cpfPut.Text,
          cnh: cnhPut.Text,
          cep: cepPut.Text,
          dataNascimento: dataNsPut.Text,
          telefone1: tel1Put.Text,
          telefone2: tel2Put.Text,
          status: false
          );

      //customersTable.Put<CustomerInsertResponse>(cliente);
    }


    private void SubmitCustomerOnClick(object sender, EventArgs e)
    {

      nomePost.Text = "Teste Table Post";
      emailPost.Text = "testedasd@mail.com";
      senhaPost.Text = "Senhar123-";
      cpfPost.Text = "752.338.950-30";
      cepPost.Text = "88353-452";
      cnhPost.Text = "04254361404";
      dataNsPost.Text = "12/08/1979";
      tel1Post.Text = "(66) 99999-9912";
      tel2Post.Text = "(77) 99999-4592";

      PostCustomer();

      //to do: time interval
      //GetCustomers();

    }

    private void PutCustomerOnClick(object sender, EventArgs e)
    {
      PutCustomer();
    }

    public Customers(IContainer container)
    {
      container.Add(this);

      InitializeComponent();
    }
  }
}

public class CustomerInsertResponse
{
  public Cliente? cliente { get; set; }
  public string? message { get; set; }
}
