using MaterialSkin.Controls;
using Newtonsoft.Json;
using System.Collections;
using System.ComponentModel;
using System.Net;
using System.Text;
using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.font;
using Top_Seguros_Brasil_Desktop.src.Models;
using Top_Seguros_Brasil_Desktop.Utils;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Customers : BasePanel
    {
        private static readonly HttpClient client = new HttpClient();
        private static int selectedItemId;
        public static ArrayList selectedCustomer = new ArrayList();
        TsbDataTable customersDataTable = new TsbDataTable();
        EngineInterpreter engineInterpreter = new EngineInterpreter(token);
        public Customers()
        {
        }

        public Customers(string pageTitle, string subtitle)
        {
            ButtonTsbPrimary putButton = new ButtonTsbPrimary();
            this.Controls.Add(putButton, 2, 9);

            MaterialSingleLineTextField customerSearchBox = new MaterialSingleLineTextField
            {
                Hint = "🔎 | Buscar cliente: ",
                SelectionStart = 6,
                Dock = DockStyle.Top,
                Margin = new Padding(32),
            };

            this.customersDataTable.CellClick += async (sender, e) =>
            {

                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == customersDataTable.Columns["Editar"].Index && e.RowIndex >= 0)
                {                    
                    for (int i = 0; i < customersDataTable.Columns.Count; i++)
                    {
                        selectedCustomer.Add(customersDataTable.SelectedRows[0].Cells[i].Value.ToString());
                    }
                    await SubmitPanelSetup<Cliente>(selectedCustomer[0].ToString());
                }

            };

            this.Controls.Add(customerSearchBox, 0, 5);
            putButton.Dock = DockStyle.Top;
            putButton.Margin = new Padding(32);
            putButton.Text = "Adicionar Cliente";
            putButton.Click += PutButton_Click;

            SubTitle(subtitle);
            Title(pageTitle);
            InitializeComponent();

            GetCustomers();
        }

        protected async void GetCustomers()
        {

            await customersDataTable.Get<Cliente>("https://tsb-api-policy-engine.herokuapp.com/cliente/");

            customersDataTable.DataBindingComplete += (sender, e) =>
            {
                customersDataTable.Columns["id_cliente"].HeaderText = "ID";
                customersDataTable.Columns["nome_completo"].HeaderText = "Nome";
                customersDataTable.Columns["email"].HeaderText = "Email";
                customersDataTable.Columns["cpf"].HeaderText = "CPF";
                customersDataTable.Columns["telefone1"].HeaderText = "Telefone 1";
                customersDataTable.Columns["Status"].HeaderText = "Status";

                customersDataTable.Columns["senha"].Visible = false;
                customersDataTable.Columns["cnh"].Visible = false;
                customersDataTable.Columns["cep"].Visible = false;
                customersDataTable.Columns["data_nascimento"].Visible = false;
                customersDataTable.Columns["telefone2"].Visible = false;
                customersDataTable.Columns["message"].Visible = false;
                customersDataTable.Columns["status"].Visible = false;

            };

            Controls.Add(customersDataTable, 0, 7);
            SetColumnSpan(customersDataTable, 3);
        }
        
        private void SubmitCustomerPanelSetup()
        {
            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Cadastrar Cliente",
                subtitleText = "Cadastre um novo cliente. ",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(titlebox, 0, 1);


            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                submitPanel.Dispose();
            };


            TsbInput nameField = new TsbInput
            {
                LabelText =  "Nome",
                HintText = "Nome Completo do cliente",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(nameField, 1, 1); ;

            DateTime dateTime = DateTime.Now;
            

            TsbMaskedTextBox dateBirthField = new TsbMaskedTextBox
            {
                LabelText = "Data de Nascimento",
                Mask = "00/00/0000",
                HintText = dateTime.Day.ToString() + "/" + dateTime.Month.ToString() + "/" + dateTime.Year.ToString(),
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(dateBirthField, 2, 1);
            

            TsbMaskedTextBox cpfField = new TsbMaskedTextBox
            {
                LabelText = "CPF",
                Mask = "000,000,000-00",
                HintText = "000,000,000-00",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(cpfField, 1, 2);
            

            TsbInput cnhField = new TsbInput
            {
                LabelText = "CNH",
                HintText = "CNH",
                MaxLength = 11,
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(cnhField, 2, 2);


            TsbMaskedTextBox cepField = new TsbMaskedTextBox
            {
                LabelText = "CEP",
                Mask = "00000-000",
                HintText = "00000-000",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(cepField, 1, 3);


            TsbInput emailField = new TsbInput
            {
                LabelText = "Email",
                HintText = "Email do cliente aqui",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(emailField, 2, 3);
            

            TsbMaskedTextBox tel1Field = new TsbMaskedTextBox
            {
                LabelText = "Telefone 1",
                Mask = "(00) 00000-0000",
                HintText = "(00) 00000-0000",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(tel1Field, 1, 4);

            TsbMaskedTextBox tel2Field = new TsbMaskedTextBox
            {
                LabelText = "Telefone 2",
                Mask = "(00) 00000-0000",
                HintText = "(00) 00000-0000",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(tel2Field, 2, 4);

            ButtonTsbSecondary submitButton = new ButtonTsbSecondary
            {
                Text = "CADASTRAR CLIENTE SEM VEÍCULO",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(submitButton, 2, 5);
            
            submitButton.Click += async (sender, e) =>
            {
                Cliente customer = new Cliente
                (
                    nomeCompleto: nameField.Text,
                    email: emailField.Text,
                    senha: "Senha123-",
                    cpf: cpfField.Text,
                    cnh: cnhField.Text,
                    cep: cepField.Text,
                    dataNascimento: dateBirthField.Text,
                    telefone1: tel1Field.Text,
                    telefone2: tel2Field.Text,
                    status: true
                );

                await PostCustomer(customer, null);
            };
            

            Panel divider = new Panel();
            divider.Height = 1;
            divider.BackColor = TsbColor.neutralWhite;
            divider.Dock = DockStyle.Top;
            submitPanel.Controls.Add(divider, 0, 6);
            submitPanel.SetColumnSpan(divider, 3);

            TitleBox titleboxContinue = new TitleBox
            {
                Parent = submitPanel,
                GoBackable = false,
                titleText = "Continuar Cadastrando",
                subtitleText = "",
                Margin = new Padding
                {
                    Top = 48,
                    Bottom = 48,
                    Left = 32,
                    Right = 32
                }

            };
            submitPanel.Controls.Add(titleboxContinue, 0, 7);

            TsbFont tsbFont = new TsbFont();

            Label or = new Label
            {
                Text = "ou",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = TsbColor.neutralGray,
                Font = new Font(TsbFont.TsbFonts.Families[0], 10, FontStyle.Bold),
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 48,
                    Bottom = 48,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.SetColumnSpan(or, 2);
            submitPanel.Controls.Add(or, 1, 7);

            ButtonTsbPrimary continueSubmit = new ButtonTsbPrimary
            {
                Text = "CADASTRAR VEÍCULO",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(continueSubmit, 2, 8);
            
            continueSubmit.Click += async (sender, e) =>
            {
                Cliente customer = new Cliente
                (
                    nomeCompleto: nameField.Text,
                    email: emailField.Text,
                    senha: "Senha123-",
                    cpf: cpfField.Text,
                    cnh: cnhField.Text,
                    cep: cepField.Text,
                    dataNascimento: dateBirthField.Text,
                    telefone1: tel1Field.Text,
                    telefone2: tel2Field.Text,
                    status: true
                );

                var json = JsonConvert.SerializeObject(customer);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await engineInterpreter.Request<CustomerInsertResponse>("https://tsb-api-policy-engine.herokuapp.com/cliente/", "POST", data);

                CustomerInsertResponse responseBody = response.Body;

                SubmitVehiclePanelSetup(responseBody.client.id_cliente.ToString());

                submitPanel.Dispose();

                
            };


            ButtonTsbTertiary cancelSubmit = new ButtonTsbTertiary
            {
                Text = "CANCELAR",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(cancelSubmit, 1, 8);


            

            if (Controls.OfType<SubmitPanel>().Count() != 0)
            {
                return;
            }


            FindForm().Controls.Add(submitPanel);
            submitPanel.BringToFront();
            submitPanel.Show();
            submitPanel.Visible = true;

            return;
        }

        public async void SubmitVehiclePanelSetup(string id)
        {
            SubmitPanel submitPanel = new SubmitPanel();

            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Cadastrar Veículo",
                subtitleText = "Cadastre um novo veículo. ",
                Margin = new Padding(32),
            };
            submitPanel.Controls.Add(titlebox, 0, 1);

            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                submitPanel.Dispose();
            };

            var engineInterpreter = new EngineInterpreter(token);

            var brandsResponse = await engineInterpreter.Request<IEnumerable<Marca>>("https://tsb-api-policy-engine.herokuapp.com/fipe/marcas", "GET", null);
            IEnumerable<Marca> brandsBody = brandsResponse.Body;

            TsbComboBox brandField = new TsbComboBox
            {
                LabelText = "Marca",
                HintText = "Marca veículo",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };

            foreach (Marca brand in brandsBody)
            {
                brandField.Items.Add(brand.nome);
            }
            submitPanel.Controls.Add(brandField, 1, 1);

            TsbComboBox modelField = new TsbComboBox
            {
                LabelText = "Modelo",
                HintText = "Modelo do veículo",
                Dock = DockStyle.Top,
                Enabled = false,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(modelField, 2, 1);


            brandField.SelectedValueChanged += async (sender, e) =>
            {

                modelField.Enabled = true;

                modelField.Items.Clear();

                var selectedBrand = brandsBody.Where(brand => brand.nome == brandField.SelectedItem.ToString()).First();
                var models = await engineInterpreter.Request<IEnumerable<Modelo>>($"https://tsb-api-policy-engine.herokuapp.com/fipe/marcas/{selectedBrand.codigo}/modelos", "GET", null);

                IEnumerable<Modelo> modelsBody = models.Body;

                foreach (Modelo model in modelsBody)
                {
                    modelField.Items.Add(model.nome);
                }

                modelField.SelectedItem = modelField.Items[0];

            };

            TsbComboBox yearField = new TsbComboBox
            {
                LabelText = "Ano",
                HintText = "Ano do veículo",
                Dock = DockStyle.Top,
                Enabled = false,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(yearField, 1, 2);


            modelField.SelectedValueChanged += async (sender, e) =>
            {

                yearField.Enabled = true;
                yearField.Items.Clear();
                var selectedBrand = brandsBody.Where(brand => brand.nome == brandField.SelectedItem.ToString()).First();
                var models = await engineInterpreter.Request<IEnumerable<Modelo>>($"https://tsb-api-policy-engine.herokuapp.com/fipe/marcas/{selectedBrand.codigo}/modelos", "GET", null);

                IEnumerable<Modelo> modelsBody = models.Body;

                var selectedModel = modelsBody.Where(model => model.nome == modelField.SelectedItem.ToString()).First();

                var yearsResponse = await engineInterpreter.Request<IEnumerable<Ano>>($"https://tsb-api-policy-engine.herokuapp.com/fipe/marcas/{selectedBrand.codigo}/modelos/{selectedModel.codigo}/anos", "GET", null);
                IEnumerable<Ano> yearsBody = yearsResponse.Body;

                foreach (Ano year in yearsBody)
                {
                    yearField.Items.Add(year.nome);
                }

                yearField.SelectedItem = yearField.Items[0];

            };

            TsbMaskedTextBox plateField = new TsbMaskedTextBox
            {
                LabelText = "Placa do veículo",
                Mask = "AAA-0000",
                HintText = "AAA0000",
                Dock = DockStyle.Top,
                Enabled = false,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(plateField, 2, 2);

            yearField.SelectedValueChanged += (sender, e) =>
            {
                plateField.Enabled = true;
            };


            TsbInput renavamField = new TsbInput
            {
                LabelText = "RENAVAM",
                HintText = "RENAVAM do veículo",
                MaxLength = 11,
                Dock = DockStyle.Top,
                Enabled = false,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };

            submitPanel.Controls.Add(renavamField, 1, 3);

            MaterialCheckBox checkBox = new MaterialCheckBox
            {
                Text = "Sinistrado",
                Dock = DockStyle.Fill,
                Enabled = false,
                Anchor = (AnchorStyles.Left | AnchorStyles.Bottom),
                Location = new Point(0, 0),
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(checkBox, 2, 3);

            TsbInput usageField = new TsbInput
            {
                LabelText = "Uso do veículo",
                HintText = "Ex: Trabalho",
                Dock = DockStyle.Top,
                Enabled = false,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(usageField, 1, 4);
            this.SetColumnSpan(usageField, 2);


            plateField.TextChanged += (sender, e) => {
                if (plateField.Text.Length == plateField.Mask.Length)
                {
                    renavamField.Enabled = true;
                    usageField.Enabled = true;
                    checkBox.Enabled = true;
                }
            };

            ButtonTsbSecondary submitButton = new ButtonTsbSecondary
            {
                Text = "CADASTRAR VEÍCULO SEM APÓLICE",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(submitButton, 1, 5);
            this.SetColumnSpan(submitButton, 2);

            submitButton.Click += async (sender, e) =>
            {
                Veiculo vehicle = new Veiculo
                (
                    marca: brandField.SelectedItem.ToString(),
                    modelo: modelField.SelectedItem.ToString(),
                    ano: yearField.SelectedItem.ToString(),
                    renavam: renavamField.Text,
                    placa: plateField.Text,
                    uso: usageField.Text,
                    sinistrado: checkBox.Checked,
                    idCliente: int.Parse(id)
                );

                var json = JsonConvert.SerializeObject(vehicle);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await engineInterpreter.Request<VehicleInsertResponse>("https://tsb-api-policy-engine.herokuapp.com/veiculo/", "POST", data);

                VehicleInsertResponse responseBody = response.Body;

                submitPanel.Dispose();

            };



            TitleBox titleboxContinue = new TitleBox
            {
                Parent = submitPanel,
                GoBackable = false,
                titleText = "Continuar Cadastrando",
                subtitleText = "",
                Margin = new Padding
                {
                    Top = 48,
                    Bottom = 48,
                    Left = 32,
                    Right = 32
                }

            };

            Panel divider = new Panel();
            divider.Height = 1;
            divider.BackColor = TsbColor.neutralWhite;
            divider.Dock = DockStyle.Top;
            submitPanel.Controls.Add(divider, 0, 6);
            submitPanel.SetColumnSpan(divider, 3);

            submitPanel.Controls.Add(titleboxContinue, 0, 7);

            Label or = new Label
            {
                Text = "ou",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = TsbColor.neutralGray,
                Font = new Font(TsbFont.TsbFonts.Families[0], 10, FontStyle.Bold),
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 48,
                    Bottom = 48,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.SetColumnSpan(or, 2);
            submitPanel.Controls.Add(or, 1, 7);

            ButtonTsbPrimary continueSubmit = new ButtonTsbPrimary
            {
                Text = "CADASTRAR APÓLICE",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(continueSubmit, 2, 8);

            continueSubmit.Click += async (sender, e) =>
            {
                Veiculo vehicle = new Veiculo
                (
                    marca: brandField.SelectedItem.ToString(),
                    modelo: modelField.SelectedItem.ToString(),
                    ano: yearField.SelectedItem.ToString(),
                    renavam: renavamField.Text,
                    placa: plateField.Text,
                    uso: usageField.Text,
                    sinistrado: checkBox.Checked,
                    idCliente: int.Parse(id)

                );

                var vehicleJson = JsonConvert.SerializeObject(vehicle);
                var vehicleData = new StringContent(vehicleJson, Encoding.UTF8, "application/json");

                var vehicleResponse = await engineInterpreter.Request<VehicleInsertResponse>("https://tsb-api-policy-engine.herokuapp.com/veiculo/", "POST", vehicleData);

                VehicleInsertResponse responseBody = vehicleResponse.Body;

                SubmitPolicyPanelSetup(responseBody.vehicle.id_cliente.ToString(), responseBody.vehicle.id_veiculo.ToString());

                submitPanel.Dispose();

            };


            ButtonTsbTertiary cancelSubmit = new ButtonTsbTertiary
            {
                Text = "CANCELAR",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(cancelSubmit, 1, 8);


            if (Controls.OfType<SubmitPanel>().Count() != 0)
            {
                return;
            }


            FindForm().Controls.Add(submitPanel);
            submitPanel.BringToFront();
            submitPanel.Show();
            submitPanel.Visible = true;

            return;

        }

        private async void SubmitPolicyPanelSetup(string idCliente, string idVeiculo)
        {
            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Cadastro de apólice",
                subtitleText = "Dados gerais da apólice do veículo.",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(titlebox, 0, 1);

            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                submitPanel.Dispose();
            };

            TsbMaskedTextBox rewardField = new TsbMaskedTextBox
            {
                LabelText = "Prêmio",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(rewardField, 1, 1);

            TsbMaskedTextBox indemnField = new TsbMaskedTextBox
            {
                LabelText = "Indenização",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(indemnField, 2, 1);

            var engineInterpreter = new EngineInterpreter(token);
            var coverages = await engineInterpreter.Request<IEnumerable<Cobertura>>("https://tsb-api-policy-engine.herokuapp.com/cobertura/", "GET", null);

            IEnumerable<Cobertura> coveragesList = coverages.Body;


            TsbComboBox coverageField = new TsbComboBox
            {
                LabelText = "Cobertura",
                HintText = "Tipo de cobertura",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            foreach (Cobertura coverage in coveragesList)
            {
                coverageField.Items.Add(coverage.nome + " - " + coverage.valor + "/mês");
            }
            submitPanel.Controls.Add(coverageField, 1, 2);
            this.SetColumnSpan(coverageField, 2);


            TsbInput descriptionField = new TsbInput
            {
                LabelText = "Descrição da cobertura",
                HintText = $"",
                MaxLength = 11,
                Dock = DockStyle.Top,
                Enabled = false,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };



            coverageField.SelectedValueChanged += async (sender, e) =>
            {

                string itemNameSplited = coverageField.SelectedItem.ToString().Split()[0];

                var selectedCoverage = coveragesList.Where(brand => brand.nome == itemNameSplited);

                selectedItemId = coveragesList.First().id_cobertura;

                descriptionField.Text = selectedCoverage.First().descricao;

            };
            submitPanel.Controls.Add(descriptionField, 1, 3);
            this.SetColumnSpan(descriptionField, 2);

            DateTime dateTime = DateTime.Now;

            TsbMaskedTextBox startDateField = new TsbMaskedTextBox
            {
                LabelText = "Data de Início",
                Mask = "00/00/0000",
                HintText = dateTime.Day.ToString() + dateTime.Month.ToString() + dateTime.Year.ToString(),
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(startDateField, 1, 4);

            TsbMaskedTextBox endDateField = new TsbMaskedTextBox
            {
                LabelText = "Data de Fim",
                Mask = "00/00/0000",
                HintText = dateTime.Day.ToString() + dateTime.Month.ToString() + dateTime.Year.ToString(),
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(endDateField, 2, 4);

            ButtonTsbPrimary continueSubmit = new ButtonTsbPrimary
            {
                Text = "CADASTRAR APÓLICE",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(continueSubmit, 2, 8);


            continueSubmit.Click += async (sender, e) =>
            {
                Apolice policy = new Apolice
                (
                    dataInicio: startDateField.Text,
                    dataFim: endDateField.Text,
                    premio: int.Parse(rewardField.Text),
                    indenizacao: int.Parse(indemnField.Text),
                    idCobertura: selectedItemId,
                    idUsuario: userId,
                    idCliente: int.Parse(idCliente),
                    idVeiculo: int.Parse(idVeiculo),
                    status: "Em análise"
                );

                var policyJson = JsonConvert.SerializeObject(policy);
                var policyData = new StringContent(policyJson, Encoding.UTF8, "application/json");

                var policyResponse = await engineInterpreter.Request<PolicyInsertResponse>("https://tsb-api-policy-engine.herokuapp.com/apolice/", "POST", policyData);

                PolicyInsertResponse responseBody = policyResponse.Body;

                MessageBox.Show(responseBody.message);

                submitPanel.Dispose();
            };



            ButtonTsbTertiary cancelSubmit = new ButtonTsbTertiary
            {
                Text = "CANCELAR",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(cancelSubmit, 1, 8);


            if (Controls.OfType<SubmitPanel>().Count() != 0)
            {
                return;
            }


            FindForm().Controls.Add(submitPanel);
            submitPanel.BringToFront();
            submitPanel.Show();
            submitPanel.Visible = true;

            return;

        }
        
        private async Task SubmitPanelSetup<Type>(string id)
        {

            EngineInterpreterResponse response;


            string address = "https://tsb-api-policy-engine.herokuapp.com/cliente/";

            response = await engineInterpreter.Request<Type>($"{address}{id}", "GET", null);

            Cliente responseBody = response.Body;

            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = $"Editar {responseBody.nome_completo}",
                subtitleText = "Editar cliente. ",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(titlebox, 0, 1);


            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                selectedCustomer.Clear();
                submitPanel.Dispose();
            };


            TsbInput nameField = new TsbInput
            {
                LabelText = "Nome",
                HintText = responseBody.nome_completo,
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(nameField, 1, 1); ;

            DateTime dateTime = DateTime.Now;


            TsbMaskedTextBox dateBirthField = new TsbMaskedTextBox
            {
                LabelText = "Data de Nascimento",
                Mask = "00/00/0000",
                HintText = responseBody.data_nascimento,
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(dateBirthField, 2, 1);


            TsbMaskedTextBox cpfField = new TsbMaskedTextBox
            {
                LabelText = "CPF",
                Mask = "000,000,000-00",
                Text = responseBody.cpf,
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(cpfField, 1, 2);


            TsbInput cnhField = new TsbInput
            {
                LabelText = "CNH",
                Text = responseBody.cnh,
                MaxLength = 11,
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(cnhField, 2, 2);


            TsbMaskedTextBox cepField = new TsbMaskedTextBox
            {
                LabelText = "CEP",
                Mask = "00000-000",
                Text = responseBody.cep,
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(cepField, 1, 3);


            TsbInput emailField = new TsbInput
            {
                LabelText = "Email",
                Text = responseBody.email,
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(emailField, 2, 3);


            TsbMaskedTextBox tel1Field = new TsbMaskedTextBox
            {
                LabelText = "Telefone 1",
                Mask = "(00) 00000-0000",
                Text = responseBody.telefone1,
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(tel1Field, 1, 4);

            TsbMaskedTextBox tel2Field = new TsbMaskedTextBox
            {
                LabelText = "Telefone 2",
                Mask = "(00) 00000-0000",
                Text = responseBody.telefone2,
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(tel2Field, 2, 4);

            ButtonTsbPrimary submitButton = new ButtonTsbPrimary
            {
                Text = "EDITAR CLIENTE",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(submitButton, 2, 5);

            submitButton.Click += async (sender, e) =>
            {
                Cliente customer = new Cliente
                (
                    nomeCompleto: nameField.Text,
                    email: emailField.Text,
                    senha: "Senha123-",
                    cpf: cpfField.Text,
                    cnh: cnhField.Text,
                    cep: cepField.Text,
                    dataNascimento: dateBirthField.Text,
                    telefone1: tel1Field.Text,
                    telefone2: tel2Field.Text,
                    status: true
                );

                await PutCustomer(customer, id);
                GetCustomers();
            };


            Panel divider = new Panel();
            divider.Height = 1;
            divider.BackColor = TsbColor.neutralWhite;
            divider.Dock = DockStyle.Top;
            submitPanel.Controls.Add(divider, 0, 6);
            submitPanel.SetColumnSpan(divider, 3);
            

            if (Controls.OfType<SubmitPanel>().Count() != 0)
            {
                return;
            }


            FindForm().Controls.Add(submitPanel);
            submitPanel.BringToFront();
            submitPanel.Show();
            submitPanel.Visible = true;

            return;
        }

        protected async Task PutCustomer(Cliente customerData, string id)
        {
            await customersDataTable.Put<Cliente>(customerData, id);
            Controls.Remove(customersDataTable);
            GetCustomers();
        }

        protected async Task PostCustomer(Cliente customerData, EventHandler? e)
        {
            EngineInterpreter engineInterpreter = new EngineInterpreter(token);

            var json = JsonConvert.SerializeObject(customerData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await engineInterpreter.Request<CustomerInsertResponse>("https://tsb-api-policy-engine.herokuapp.com/cliente/", "POST", data);

            CustomerInsertResponse responseBody = response.Body;

            Controls.Remove(customersDataTable);
            GetCustomers();
        }


        public Customers(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void PutButton_Click(object? sender, EventArgs? e)
        {
            SubmitCustomerPanelSetup();
        }
    }
}

public class CustomerInsertResponse
{
    public Cliente? client { get; set; }
    public string? message { get; set; }
}

public class VehicleInsertResponse
{
    public string? message { get; set; }
    public Veiculo? vehicle { get; set; }
    
}

public class PolicyInsertResponse
{
    public Apolice? policy { get; set; }
    public string? message { get; set; }

}
