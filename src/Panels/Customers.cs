namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Customers : BasePanel
    {
        private static readonly HttpClient client = new HttpClient();
        private static int selectedItemId;
        public static ArrayList selectedCustomer = new ArrayList();
        public Apolice policyDetails;
        TsbDataTable customersDataTable = new TsbDataTable();
        EngineInterpreter engineInterpreter = new EngineInterpreter(token);

        public Customers()
        {
        }

        public Customers(string pageTitle, string subtitle)
        {
            ButtonTsbPrimary putButton = new ButtonTsbPrimary();
            this.Controls.Add(putButton, 2, 9);

            this.customersDataTable.CellClick += async (sender, e) =>
            {

                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == customersDataTable.Columns["Detalhes"].Index && e.RowIndex >= 0)
                {

                    if (customersDataTable.Columns["Detalhes"].Index == 0)
                    {
                        selectedCustomer.Add(customersDataTable.SelectedRows[0].Cells[3].Value.ToString());
                        await SubmitPanelSetup<Cliente>(selectedCustomer[0].ToString());
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < customersDataTable.Columns.Count; i++)
                        {
                            selectedCustomer.Add(customersDataTable.SelectedRows[0].Cells[i].Value.ToString());
                        }
                        await SubmitPanelSetup<Cliente>(selectedCustomer[0].ToString());
                        return;
                    }

                }

            };

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

            await customersDataTable.Get<PaginatedResponse<Cliente>>("https://tsb-api-policy-engine.herokuapp.com/cliente/", null);

            customersDataTable.DataBindingComplete += (sender, e) =>
            {

                string[] columns = { "senha", "cnh", "cep", "data_nascimento", "telefone 2", "message", "status", "deletar", "editar", "type", "item" };

                customersDataTable.RemoveColumns(columns);

            };

            Controls.Add(customersDataTable, 0, 5);
            SetColumnSpan(customersDataTable, 3);
            SetRowSpan(customersDataTable, 4);
            this.Refresh();
            customersDataTable.Refresh();
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
                LabelText = "Nome",
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


            plateField.TextChanged += (sender, e) =>
            {
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

                PolicyToGenerate policyToGenerate = new PolicyToGenerate
                {
                    id_cliente = (int)responseBody.policy.id_cliente,
                    id_cobertura = (int)responseBody.policy.id_cobertura,
                    id_veiculo = (int)responseBody.policy.id_veiculo,
                };

                var json = JsonConvert.SerializeObject(policyToGenerate);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var policyCreateResponse = await engineInterpreter.Request<EnrichedPolicy>("https://tsb-api-policy-engine.herokuapp.com/apolice/gerar/", "POST", data);

                EnrichedPolicy enrichedPolicy = policyCreateResponse.Body;

                MessageBox.Show(responseBody.message);
                MessageBox.Show(enrichedPolicy.message);

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

        private async void PolicyDetails(string id)
        {
            EngineInterpreterResponse response;

            string address = $"https://tsb-api-policy-engine.herokuapp.com/apolice/{id}";

            response = await engineInterpreter.Request<PolicyRequestResponse>(address, "GET", null);

            PolicyRequestResponse requestResponse = response.Body;

            SubmitPanel submitPanel = new SubmitPanel();

            TableLayoutPanel customerInfoPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 72,
            };
            submitPanel.Controls.Add(customerInfoPanel, 1, 3);
            submitPanel.SetColumnSpan(customerInfoPanel, 2);

            customerInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            customerInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            customerInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            customerInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));


            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Solicitações de apólice",
                subtitleText = "Gerenciamento de solicitações de apólice.",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(titlebox, 0, 1);

            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                submitPanel.Dispose();
            };

            Panel dividerTitle = new Panel();
            dividerTitle.Height = 1;
            dividerTitle.BackColor = TsbColor.neutralWhite;
            dividerTitle.Dock = DockStyle.Top;
            submitPanel.Controls.Add(dividerTitle, 0, 2);
            submitPanel.SetColumnSpan(dividerTitle, 3);


            TsbDataBox customerDataTitle = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Dados do cliente",
                FontSize = 16,
                subtitleText = "Dados gerais sobre o cliente",
                Margin = new Padding(24),
                Dock = DockStyle.Top

            };
            submitPanel.Controls.Add(customerDataTitle, 0, 3);

            TsbDataBox nameField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Nome completo",
                FontSize = 12,
                subtitleText = requestResponse.cliente.nome_completo,
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            customerInfoPanel.Controls.Add(nameField, 0, 0);

            TsbDataBox birthDateField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Data de nascimento",
                FontSize = 12,
                subtitleText = requestResponse.cliente.data_nascimento,
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            customerInfoPanel.Controls.Add(birthDateField, 1, 0);

            TsbDataBox cpfField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "CPF",
                FontSize = 12,
                subtitleText = requestResponse.cliente.cpf,
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            customerInfoPanel.Controls.Add(cpfField, 2, 0);

            TsbDataBox cnhField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "CNH",
                FontSize = 12,
                subtitleText = requestResponse.cliente.cnh,
                Margin = new Padding(24),
                Dock = DockStyle.Top

            };
            customerInfoPanel.Controls.Add(cnhField, 3, 0);


            TableLayoutPanel customerInfoPanelBottom = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 72,
            };
            submitPanel.Controls.Add(customerInfoPanelBottom, 1, 4);
            submitPanel.SetColumnSpan(customerInfoPanelBottom, 2);

            customerInfoPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            customerInfoPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            customerInfoPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            customerInfoPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));


            TsbDataBox cepField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "CEP",
                FontSize = 12,
                subtitleText = requestResponse.cliente.cep,
                Margin = new Padding(24),
                Dock = DockStyle.Top

            };
            customerInfoPanelBottom.Controls.Add(cepField, 0, 1);

            TsbDataBox tel1Field = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Telefone 1",
                FontSize = 12,
                subtitleText = requestResponse.cliente.telefone1,
                Margin = new Padding(24),
                Dock = DockStyle.Top

            };
            customerInfoPanelBottom.Controls.Add(tel1Field, 1, 1);

            TsbDataBox tel2Field = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Telefone 2",
                FontSize = 12,
                subtitleText = requestResponse.cliente.telefone2,
                Margin = new Padding(24),
                Dock = DockStyle.Top

            };
            customerInfoPanelBottom.Controls.Add(tel2Field, 2, 1);

            TsbDataBox emailField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Email",
                FontSize = 12,
                subtitleText = requestResponse.cliente.email,
                Margin = new Padding(24),
                Dock = DockStyle.Top

            };
            customerInfoPanelBottom.Controls.Add(emailField, 3, 1);

            Panel dividerCustomer = new Panel();
            dividerCustomer.Height = 1;
            dividerCustomer.BackColor = TsbColor.neutralWhite;
            dividerCustomer.Dock = DockStyle.Top;
            submitPanel.Controls.Add(dividerCustomer, 0, 5);
            submitPanel.SetColumnSpan(dividerCustomer, 3);




            TsbDataBox vehicleDataTitle = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Dados do veículo",
                FontSize = 16,
                subtitleText = "Dados gerais sobre o veículo",
                Margin = new Padding(24),
                Dock = DockStyle.Top

            };
            submitPanel.Controls.Add(vehicleDataTitle, 0, 6);


            TableLayoutPanel vehicleInfoPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 72,
            };
            submitPanel.Controls.Add(vehicleInfoPanel, 1, 6);
            submitPanel.SetColumnSpan(vehicleInfoPanel, 2);

            vehicleInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            vehicleInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            vehicleInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            vehicleInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));


            TsbDataBox brandField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Marca",
                FontSize = 12,
                subtitleText = requestResponse.veiculo.marca,
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            vehicleInfoPanel.Controls.Add(brandField, 0, 0);

            TsbDataBox modelField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Modelo",
                FontSize = 12,
                subtitleText = requestResponse.veiculo.modelo,
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            vehicleInfoPanel.Controls.Add(modelField, 1, 0);

            TsbDataBox yearField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Ano",
                FontSize = 12,
                subtitleText = requestResponse.veiculo.ano,
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            vehicleInfoPanel.Controls.Add(yearField, 2, 0);

            TsbDataBox plateField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Placa",
                FontSize = 12,
                subtitleText = requestResponse.veiculo.placa,
                Margin = new Padding(24),
                Dock = DockStyle.Top

            };
            vehicleInfoPanel.Controls.Add(plateField, 3, 0);


            TableLayoutPanel vehicleInfoPanelBottom = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 72,
            };
            submitPanel.Controls.Add(vehicleInfoPanelBottom, 1, 7);
            submitPanel.SetColumnSpan(vehicleInfoPanelBottom, 2);

            vehicleInfoPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            vehicleInfoPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            vehicleInfoPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            vehicleInfoPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            TsbDataBox renavamField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Renavam",
                FontSize = 12,
                subtitleText = requestResponse.veiculo.renavam,
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            vehicleInfoPanelBottom.Controls.Add(renavamField, 0, 1);

            TsbDataBox useField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Uso",
                FontSize = 12,
                subtitleText = requestResponse.veiculo.uso,
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            vehicleInfoPanelBottom.Controls.Add(useField, 1, 1);

            TsbDataBox incidentField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Sinistrado",
                FontSize = 12,
                subtitleText = requestResponse.veiculo.sinistrado.ToString() == "1" ? "Sim" : "Não",
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            vehicleInfoPanelBottom.Controls.Add(incidentField, 2, 1);

            Panel blankCell = new Panel
            {
                Dock = DockStyle.Top,
                BackColor = TsbColor.background
            };
            vehicleInfoPanelBottom.Controls.Add(blankCell, 3, 1);

            Panel dividerVehicle = new Panel();
            dividerVehicle.Height = 1;
            dividerVehicle.BackColor = TsbColor.neutralWhite;
            dividerVehicle.Dock = DockStyle.Top;
            submitPanel.Controls.Add(dividerVehicle, 0, 8);
            submitPanel.SetColumnSpan(dividerVehicle, 3);



            TsbDataBox policyDataTitle = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Dados da apólice",
                FontSize = 16,
                subtitleText = "Dados gerais sobre a apólice",
                Margin = new Padding(24),
                Dock = DockStyle.Top

            };
            submitPanel.Controls.Add(policyDataTitle, 0, 9);


            TableLayoutPanel policyInfoPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 72,
            };
            submitPanel.Controls.Add(policyInfoPanel, 1, 9);
            submitPanel.SetColumnSpan(policyInfoPanel, 2);

            policyInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            policyInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            policyInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            policyInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));



            TsbDataBox prizeField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Prêmio",
                FontSize = 12,
                subtitleText = "R$ " + requestResponse.premio.ToString(),
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            policyInfoPanel.Controls.Add(prizeField, 0, 0);

            TsbDataBox indenmField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Indenização",
                FontSize = 12,
                subtitleText = requestResponse.indenizacao.ToString(),
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            policyInfoPanel.Controls.Add(indenmField, 1, 0);

            TsbDataBox startDate = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Data de início",
                FontSize = 12,
                subtitleText = requestResponse.data_inicio,
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            policyInfoPanel.Controls.Add(startDate, 2, 0);

            TsbDataBox endDate = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Data de fim",
                FontSize = 12,
                subtitleText = requestResponse.data_fim,
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            policyInfoPanel.Controls.Add(endDate, 3, 0);


            TableLayoutPanel policyInfoPanelBottom = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 72,
            };
            submitPanel.Controls.Add(policyInfoPanelBottom, 1, 10);
            submitPanel.SetColumnSpan(policyInfoPanelBottom, 2);

            policyInfoPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            policyInfoPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            policyInfoPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            policyInfoPanelBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));



            TsbDataBox coverageField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Cobertura",
                FontSize = 12,
                subtitleText = requestResponse.cobertura.nome,
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            policyInfoPanelBottom.Controls.Add(coverageField, 0, 0);

            TsbDataBox priceField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Valor cobertura",
                FontSize = 12,
                subtitleText = "R$ " + requestResponse.cobertura.valor.ToString() + "/mês",
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            policyInfoPanelBottom.Controls.Add(priceField, 1, 0);

            TsbDataBox descriptionField = new TsbDataBox
            {
                Parent = submitPanel,
                titleText = "Descrição",
                FontSize = 12,
                subtitleText = requestResponse.cobertura.descricao,
                Margin = new Padding(24),
                Dock = DockStyle.Top
            };
            policyInfoPanelBottom.Controls.Add(descriptionField, 2, 0);

            Panel blankCellCoverage = new Panel
            {
                Dock = DockStyle.Top,
                BackColor = TsbColor.background
            };
            policyInfoPanelBottom.Controls.Add(blankCellCoverage, 3, 0);

            Panel dividerPolicy = new Panel();
            dividerPolicy.Height = 1;
            dividerPolicy.BackColor = TsbColor.neutralWhite;
            dividerPolicy.Dock = DockStyle.Top;
            submitPanel.Controls.Add(dividerPolicy, 0, 11);
            submitPanel.SetColumnSpan(dividerPolicy, 3);

            ButtonTsbSecondary submitButton = new ButtonTsbSecondary
            {
                Text = "BAIXAR APÓLICE",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 24,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };

            submitPanel.Controls.Add(submitButton, 1, 12);
            submitPanel.SetColumnSpan(submitButton, 2);


            submitButton.Click += async (sender, e) =>
            {

                PolicyToGenerate policy = new PolicyToGenerate
                {
                    id_cliente = requestResponse.cliente.id_cliente,
                    id_cobertura = requestResponse.cobertura.id_cobertura,
                    id_veiculo = requestResponse.veiculo.id_veiculo,
                };

                var json = JsonConvert.SerializeObject(policy);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                EngineInterpreter engineInterpreter = new EngineInterpreter(token);
                var policyCreateResponse = await engineInterpreter.Request<EnrichedPolicy>("https://tsb-api-policy-engine.herokuapp.com/apolice/gerar/", "POST", data);

                EnrichedPolicy enriched = policyCreateResponse.Body;

                MessageBox.Show(enriched.message);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var uri = new Uri("https://tsb-api-policy-engine.herokuapp.com/apolice/documento/" + requestResponse.id_apolice);

                await using var s = await client.GetStreamAsync(uri);

                await using var file = File.Create($"{requestResponse.cliente.nome_completo}-apolice.pdf");
                await s.CopyToAsync(file);


            };


            if (requestResponse.status == "Em Análise")
            {

                ButtonTsbPrimary policyAproveBtn = new ButtonTsbPrimary
                {
                    Text = "APROVAR APÓLICE",
                    Dock = DockStyle.Top,
                    Margin = new Padding
                    {
                        Top = 0,
                        Bottom = 24,
                        Left = 32,
                        Right = 32
                    }
                };
                submitPanel.Controls.Add(policyAproveBtn, 2, 13);

                policyAproveBtn.Click += async (sender, e) =>
                {

                    EngineInterpreter engineInterpreterAprove = new EngineInterpreter(token);

                    var response = await engineInterpreterAprove.Request<EnrichedPolicy>($"https://tsb-api-policy-engine.herokuapp.com/apolice/{requestResponse.id_apolice}/ativa", "PUT", null);

                    if (response.StatusCode == 200)
                    {
                        MessageBox.Show("Apolice aprovada!");
                        return;
                    }

                    MessageBox.Show("Erro ao aprovar apolice");

                };

                ButtonTsbTertiary policylRejectBtn = new ButtonTsbTertiary
                {
                    Text = "REJEITAR APÓLICE",
                    Dock = DockStyle.Top,
                    Margin = new Padding
                    {
                        Top = 0,
                        Bottom = 24,
                        Left = 32,
                        Right = 32
                    }
                };
                submitPanel.Controls.Add(policylRejectBtn, 1, 13);


                policylRejectBtn.Click += async (sender, e) =>
                {

                    EngineInterpreter engineInterpreterAprove = new EngineInterpreter(token);

                    var response = await engineInterpreterAprove.Request<EnrichedPolicy>($"https://tsb-api-policy-engine.herokuapp.com/apolice/{requestResponse.id_apolice}/rejeitada", "PUT", null);

                    if (response.StatusCode == 200)
                    {
                        MessageBox.Show("Apolice rejeitada.");
                        return;
                    }

                    MessageBox.Show("Erro ao rejeitar apolice.");

                };

            }

            if (requestResponse.status == "Ativa")
            {

                ButtonTsbPrimary policyAproveBtn = new ButtonTsbPrimary
                {
                    Text = "DESATIVAR APÓLICE",
                    Dock = DockStyle.Top,
                    Margin = new Padding
                    {
                        Top = 0,
                        Bottom = 24,
                        Left = 32,
                        Right = 32
                    }
                };
                submitPanel.Controls.Add(policyAproveBtn, 2, 13);
                submitPanel.SetColumn(policyAproveBtn, 1);
                submitPanel.SetColumnSpan(policyAproveBtn, 2);

                policyAproveBtn.Click += async (sender, e) =>
                {

                    EngineInterpreter engineInterpreterAprove = new EngineInterpreter(token);

                    var response = await engineInterpreterAprove.Request<EnrichedPolicy>($"https://tsb-api-policy-engine.herokuapp.com/apolice/{requestResponse.id_apolice}/inativa", "PUT", null);

                    if (response.StatusCode == 200)
                    {
                        MessageBox.Show("Apolice aprovada!");
                        return;
                    }

                    MessageBox.Show("Erro ao aprovar apolice");

                };

            }


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

public class PaginatedResponse<type>
{
    public type[]? data { get; set; }
    public int? totalPages { get; set; }
}
