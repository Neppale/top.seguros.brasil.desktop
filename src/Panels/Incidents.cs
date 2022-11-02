namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Incidents : BasePanel
    {
        private static readonly HttpClient client = new HttpClient();
        public static ArrayList selectedIncident = new ArrayList();
        private Stream stream = new MemoryStream();
        TsbDataTable incidentsDataTable = new TsbDataTable();
        EngineInterpreter engineInterpreter = new EngineInterpreter(token);

        public Incidents()
        {
            InitializeComponent();
        }

        public Incidents(string pageTitle, string subtitle)
        {
            ButtonTsbPrimary putButton = new ButtonTsbPrimary();
            this.Controls.Add(putButton, 2, 9);

            this.incidentsDataTable.CellClick += async (sender, e) =>
            {

                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == incidentsDataTable.Columns["Detalhes"].Index && e.RowIndex >= 0)
                {

                    if (incidentsDataTable.Columns["Detalhes"].Index == 0)
                    {
                        selectedIncident.Add(incidentsDataTable.SelectedRows[0].Cells[3].Value.ToString());
                        await SubmitPanelSetup<Ocorrencia>(selectedIncident[0].ToString());
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < incidentsDataTable.Columns.Count; i++)
                        {
                            selectedIncident.Add(incidentsDataTable.SelectedRows[0].Cells[i].Value.ToString());
                        }
                        await SubmitPanelSetup<Ocorrencia>(selectedIncident[0].ToString());
                        return;
                    }

                }

            };

            putButton.Dock = DockStyle.Top;
            putButton.Margin = new Padding(32);
            putButton.Text = "ADICIONAR OCORRÊNCIA";
            putButton.Click += PutButton_Click;


            SubTitle(subtitle);
            Title(pageTitle);
            InitializeComponent();

            GetIncidents();
        }

        private async void SubmitPanelSetup()
        {
            var customers = await engineInterpreter.Request<IEnumerable<Cliente>>("https://tsb-api-policy-engine.herokuapp.com/cliente/", "GET", null);

            IEnumerable<Cliente> customersBody = customers.Body;

            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Cadastrar Ocorrência",
                subtitleText = "Cadastre uma nova ocorrência. ",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(titlebox, 0, 1);


            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                submitPanel.Dispose();
            };

            TsbComboBox nameField = new TsbComboBox
            {
                LabelText = "Nome",
                HintText = "Selecione o cliente",
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
            submitPanel.Controls.Add(nameField, 1, 1);


            TsbMaskedTextBox cpfField = new TsbMaskedTextBox
            {
                LabelText = "CPF",
                Mask = "000,000,000-00",
                Enabled = false,
                ForeColor = TsbColor.neutral,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(cpfField, 2, 1);


            TsbComboBox vehicleField = new TsbComboBox
            {
                LabelText = "Veículo da ocorrência",
                HintText = "Selecione o veículo",
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
            submitPanel.Controls.Add(vehicleField, 1, 2);
            submitPanel.SetColumnSpan(vehicleField, 2);



            nameField.SelectedValueChanged += async (sender, e) =>
            {

                vehicleField.Items.Clear();
                int id_cliente = 0;

                string customerAddress = $"https://tsb-api-policy-engine.herokuapp.com/cliente/";
                var customer = await engineInterpreter.Request<IEnumerable<Cliente>>(customerAddress, "GET", null);
                IEnumerable<Cliente> customerBody = customer.Body;


                foreach (Cliente c in customerBody)
                {
                    if (c.nome_completo == nameField.HintText)
                    {
                        id_cliente = c.id_cliente;
                    }
                }

                string vehicleAddress = $"https://tsb-api-policy-engine.herokuapp.com/veiculo/cliente/{id_cliente}";
                var vehicle = await engineInterpreter.Request<IEnumerable<Veiculo>>(vehicleAddress, "GET", null);
                IEnumerable<Veiculo> vehicleBody = vehicle.Body;

                foreach (Veiculo v in vehicleBody)
                {
                    vehicleField.Items.Add($"{v.marca} - {v.modelo} - {v.ano} | {v.placa}");
                }

                vehicleField.SelectedItem = vehicleField.Items[0];
            };




            Panel customerDivider = new Panel();
            customerDivider.Height = 1;
            customerDivider.BackColor = TsbColor.neutralWhite;
            customerDivider.Dock = DockStyle.Top;
            submitPanel.Controls.Add(customerDivider, 0, 4);
            submitPanel.SetColumnSpan(customerDivider, 3);

            TitleBox incidentTitlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Dados da ocorrência",
                GoBackable = false,
                subtitleText = "",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(incidentTitlebox, 0, 5);


            ButtonTsbSecondary fileInput = new ButtonTsbSecondary
            {
                Text = "+ ADICIONAR BOLETIM DE OCORRÊNCIA",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 32,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(fileInput, 1, 5);


            TsbComboBox typeField = new TsbComboBox
            {
                LabelText = "Tipo de ocorrência",
                HintText = "Acidente",
                Items = { "Acidente", "Furto", "Roubo" },
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
            submitPanel.Controls.Add(typeField, 2, 5);


            TsbComboBox ufField = new TsbComboBox
            {
                LabelText = "Estado da ocorrência",
                HintText = "São paulo",
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
            submitPanel.Controls.Add(ufField, 1, 6);


            string ufAddress = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
            var ufs = await engineInterpreter.Request<IEnumerable<uf>>(ufAddress, "GET", null);
            IEnumerable<uf> ufBody = ufs.Body;



            foreach (var uf in ufBody)
            {
                ufField.Items.Add(uf.nome);
            }


            foreach (Cliente customer in customersBody)
            {
                nameField.Items.Add(customer.nome_completo);
            }


            TsbComboBox countyField = new TsbComboBox
            {
                LabelText = "Município da ocorrência",
                HintText = "São paulo",
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
            submitPanel.Controls.Add(countyField, 2, 6);


            string incidentDocument = "";

            fileInput.Click += async (sender, e) =>
            {

                incidentDocument = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
                openFileDialog.Title = "Selecione uma imagem";


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    incidentDocument = openFileDialog.FileName;
                }

            };


            ufField.SelectedValueChanged += async (sender, e) =>
            {
                countyField.Items.Clear();

                string ufAddress = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
                var ufs = await engineInterpreter.Request<IEnumerable<uf>>(ufAddress, "GET", null);
                IEnumerable<uf> ufBody = ufs.Body;


                string selectedUf = ufBody.Where(x => x.nome == ufField.HintText).Select(x => x.id.ToString()).FirstOrDefault();
                string countyAddress = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{selectedUf}/municipios";
                var counties = await engineInterpreter.Request<IEnumerable<municipio>>(countyAddress, "GET", null);
                IEnumerable<municipio> countyBody = counties.Body;


                foreach (municipio county in countyBody)
                {
                    countyField.Items.Add(county.nome);
                }

                countyField.SelectedItem = countyField.Items[0];

            };

            TsbInput addressField = new TsbInput
            {
                LabelText = "Local da ocorrência",
                HintText = "Rua, avenida, etc",
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
            submitPanel.Controls.Add(addressField, 1, 7);

            TsbMaskedTextBox dateField = new TsbMaskedTextBox
            {
                LabelText = "Data da ocorrência",
                Mask = "99/99/9999",
                HintText = "12/10/2022",
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
            submitPanel.Controls.Add(dateField, 2, 7);


            TsbInput descriptionField = new TsbInput
            {
                LabelText = "Descrição da ocorrência",
                HintText = "O que aconteceu? O que foi levado?",
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
            submitPanel.Controls.Add(descriptionField, 1, 8);
            submitPanel.SetColumnSpan(descriptionField, 2);



            nameField.SelectedValueChanged += async (sender, e) =>
            {
                var customer = await engineInterpreter.Request<IEnumerable<Cliente>>("https://tsb-api-policy-engine.herokuapp.com/cliente/", "GET", null);

                IEnumerable<Cliente> customerBody = customer.Body;


                foreach (Cliente selectedCustomer in customerBody)
                {
                    if (selectedCustomer.nome_completo == nameField.HintText)
                    {
                        cpfField.HintText = selectedCustomer.cpf;
                    }
                }

            };

            ButtonTsbPrimary continueSubmit = new ButtonTsbPrimary
            {
                Text = "CADASTRAR OCORRÊNCIA",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(continueSubmit, 2, 9);


            continueSubmit.Click += async (sender, e) =>
            {

                int id_veiculo = 0;
                int id_cliente = 0;
                string uf = "";

                var customer = await engineInterpreter.Request<IEnumerable<Cliente>>("https://tsb-api-policy-engine.herokuapp.com/cliente/", "GET", null);

                IEnumerable<Cliente> customerBody = customer.Body;


                foreach (Cliente selectedCustomer in customerBody)
                {
                    if (selectedCustomer.nome_completo == nameField.HintText)
                    {
                        id_cliente = selectedCustomer.id_cliente;
                    }
                }


                string selectedVehiclePlate = vehicleField.HintText.Split("| ")[1];
                var vehicle = await engineInterpreter.Request<IEnumerable<Veiculo>>($"https://tsb-api-policy-engine.herokuapp.com/veiculo/cliente/{id_cliente}", "GET", null);

                IEnumerable<Veiculo> vehicleBody = vehicle.Body;

                foreach (Veiculo v in vehicleBody)
                {
                    if (v.placa == selectedVehiclePlate)
                    {
                        id_veiculo = v.id_veiculo;
                    }
                }

                string ufAddress = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
                var ufs = await engineInterpreter.Request<IEnumerable<uf>>(ufAddress, "GET", null);
                IEnumerable<uf> ufBody = ufs.Body;

                foreach (uf selectedUf in ufBody)
                {
                    if (selectedUf.nome == ufField.HintText)
                    {
                        uf = selectedUf.sigla;
                    }
                }

                Ocorrencia incident = new Ocorrencia
                (
                    data: dateField.Text,
                    local: addressField.Text,
                    uf: uf,
                    municipio: countyField.HintText,
                    descricao: descriptionField.Text,
                    tipo: typeField.HintText,
                    id_cliente: id_cliente,
                    id_veiculo: id_veiculo,
                    status: "Processando"
                );

                var json = JsonConvert.SerializeObject(incident);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await engineInterpreter.Request<IncidentInsertReponse>("https://tsb-api-policy-engine.herokuapp.com/ocorrencia/", "POST", data);

                IncidentInsertReponse responseBody = response.Body;


                if (response.StatusCode == 201)
                {

                    if (incidentDocument != "")
                    {

                        AddDocument(incidentDocument, responseBody.incident.id_ocorrencia.ToString());

                    }
                    else
                    {
                        MessageBox.Show("Ocorrência cadastrada com sucesso!");

                    }

                    return;

                }


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
            submitPanel.Controls.Add(cancelSubmit, 1, 9);




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

        private async Task SubmitPanelSetup<Type>(string incidentId)
        {
            EngineInterpreterResponse incidentsResponse = await engineInterpreter.Request<Ocorrencia>($"https://tsb-api-policy-engine.herokuapp.com/ocorrencia/{incidentId}", "GET", null);

            Ocorrencia incident = incidentsResponse.Body;

            EngineInterpreterResponse customerResponse = await engineInterpreter.Request<Cliente>($"https://tsb-api-policy-engine.herokuapp.com/cliente/{incident.id_cliente}", "GET", null);

            Cliente customer = customerResponse.Body;

            EngineInterpreterResponse vehicleResponse = await engineInterpreter.Request<Veiculo>($"https://tsb-api-policy-engine.herokuapp.com/veiculo/{incident.id_veiculo}", "GET", null);

            Veiculo vehicle = vehicleResponse.Body;

            SubmitPanel submitPanel = new SubmitPanel();
            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = $"Editar ocorrência nº{incident.id_ocorrencia}",
                subtitleText = "Editar ocorrência. ",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(titlebox, 0, 1);


            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                selectedIncident.Clear();
                submitPanel.Dispose();
            };

            TsbComboBox nameField = new TsbComboBox
            {
                LabelText = "Nome do cliente",
                HintText = customer.nome_completo,
                ForeColor = TsbColor.neutralGray,
                Enabled = false,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(nameField, 1, 1);


            TsbMaskedTextBox cpfField = new TsbMaskedTextBox
            {
                LabelText = "CPF",
                Mask = "000,000,000-00",
                HintText = customer.cpf,
                Enabled = false,
                ForeColor = TsbColor.neutral,
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(cpfField, 2, 1);


            TsbComboBox vehicleField = new TsbComboBox
            {
                LabelText = "Veículo da ocorrência",
                HintText = $"{vehicle.marca} - {vehicle.modelo} | {vehicle.placa}",
                Enabled = false,
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
            submitPanel.Controls.Add(vehicleField, 1, 2);
            submitPanel.SetColumnSpan(vehicleField, 2);








            Panel customerDivider = new Panel();
            customerDivider.Height = 1;
            customerDivider.BackColor = TsbColor.neutralWhite;
            customerDivider.Dock = DockStyle.Top;
            submitPanel.Controls.Add(customerDivider, 0, 4);
            submitPanel.SetColumnSpan(customerDivider, 3);

            TitleBox incidentTitlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = "Dados da ocorrência",
                GoBackable = false,
                subtitleText = "",
                Margin = new Padding(32),

            };
            submitPanel.Controls.Add(incidentTitlebox, 0, 5);



            ButtonTsbSecondary fileInput = new ButtonTsbSecondary
            {
                Text = "BAIXAR BOLETIM DE OCORRÊNCIA",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 32,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(fileInput, 1, 5);

            fileInput.Click += async (sender, e) =>
            {

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var uri = new Uri("https://tsb-api-policy-engine.herokuapp.com/ocorrencia/documento/" + incident.id_ocorrencia);



                try
                {
                    await using var s = await client.GetStreamAsync(uri);

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.FileName = incident.id_ocorrencia + "-boletim_ocorrencia";


                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {

                        await using var file = File.Create(saveFileDialog.FileName);
                        await s.CopyToAsync(file);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorrência não possui boletim de ocorrência cadastrado.");
                    fileInput.Enabled = false;

                    return;
                }




            };


            TsbComboBox typeField = new TsbComboBox
            {
                LabelText = "Tipo de ocorrência",
                HintText = incident.tipo,
                Items = { "Acidente", "Furto", "Roubo" },
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
            submitPanel.Controls.Add(typeField, 2, 5);


            TsbComboBox ufField = new TsbComboBox
            {
                LabelText = "Estado da ocorrência",
                HintText = incident.uf,
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
            submitPanel.Controls.Add(ufField, 1, 6);






            TsbComboBox countyField = new TsbComboBox
            {
                LabelText = "Município da ocorrência",
                HintText = incident.municipio,
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
            submitPanel.Controls.Add(countyField, 2, 6);

            ufField.Dropdown += async (sender, e) =>
            {
                countyField.Items.Clear();
                string ufAddress = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
                var ufs = await engineInterpreter.Request<IEnumerable<uf>>(ufAddress, "GET", null);
                IEnumerable<uf> ufBody = ufs.Body;

                foreach (var uf in ufBody)
                {
                    ufField.Items.Add(uf.nome);
                }

            };

            ufField.SelectedValueChanged += async (sender, e) =>
            {
                countyField.Items.Clear();

                string ufAddress = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
                var ufs = await engineInterpreter.Request<IEnumerable<uf>>(ufAddress, "GET", null);
                IEnumerable<uf> ufBody = ufs.Body;


                string selectedUf = ufBody.Where(x => x.nome == ufField.HintText).Select(x => x.id.ToString()).FirstOrDefault();
                string countyAddress = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{selectedUf}/municipios";
                var counties = await engineInterpreter.Request<IEnumerable<municipio>>(countyAddress, "GET", null);
                IEnumerable<municipio> countyBody = counties.Body;


                foreach (municipio county in countyBody)
                {
                    countyField.Items.Add(county.nome);
                }

                countyField.SelectedItem = countyField.Items[0];

            };

            TsbInput addressField = new TsbInput
            {
                LabelText = "Local da ocorrência",
                Text = incident.local,
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
            submitPanel.Controls.Add(addressField, 1, 7);

            TsbMaskedTextBox dateField = new TsbMaskedTextBox
            {
                LabelText = "Data da ocorrência",
                Mask = "99/99/9999",
                Text = incident.data.Split(' ')[0],
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
            submitPanel.Controls.Add(dateField, 2, 7);


            TsbInput descriptionField = new TsbInput
            {
                LabelText = "Descrição da ocorrência",
                Text = incident.descricao,
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
            submitPanel.Controls.Add(descriptionField, 1, 8);
            submitPanel.SetColumnSpan(descriptionField, 2);




            ButtonTsbPrimary continueSubmit = new ButtonTsbPrimary
            {
                Text = "APROVAR OCORRENCIA",
                Dock = DockStyle.Top,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 24,
                    Left = 32,
                    Right = 32
                }
            };
            submitPanel.Controls.Add(continueSubmit, 2, 9);


            continueSubmit.Click += async (sender, e) =>
            {
                string uf = "";

                string ufAddress = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
                var ufs = await engineInterpreter.Request<IEnumerable<uf>>(ufAddress, "GET", null);
                IEnumerable<uf> ufBody = ufs.Body;

                foreach (uf selectedUf in ufBody)
                {
                    if (selectedUf.nome == ufField.HintText)
                    {
                        uf = selectedUf.sigla;
                    }
                }

                Ocorrencia incident = new Ocorrencia
                (
                    data: DateTime
                        .ParseExact(dateField.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        .ToString("yyyy-MM-dd"),
                    local: addressField.Text,
                    uf: uf,
                    municipio: countyField.HintText,
                    descricao: descriptionField.Text,
                    tipo: typeField.HintText,
                    id_cliente: customer.id_cliente,
                    id_veiculo: vehicle.id_veiculo,
                    status: "Andamento"
                );
                await PutIncident(incident, incidentId);

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
            submitPanel.Controls.Add(cancelSubmit, 1, 9);




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

        protected async Task PutIncident(Ocorrencia incidentData, string id)
        {
            try
            {
                await incidentsDataTable.Put<IncidentInsertReponse>(incidentData, id, null);
                Controls.Remove(incidentsDataTable);
                GetIncidents();
            }
            catch (Exception ex)
            {
                return;
            }

        }

        protected void AddDocument(string incidentDocument, string incidentId)
        {

            byte[] fileBytes = File.ReadAllBytes(incidentDocument);
            var client = new RestClient($"https://tsb-api-policy-engine.herokuapp.com/ocorrencia/documento/{incidentId}")
            {
                Authenticator = new JwtAuthenticator(token),
            };
            var request = new RestRequest($"https://tsb-api-policy-engine.herokuapp.com/ocorrencia/documento/{incidentId}", Method.Post);

            request.AddHeader("file", incidentDocument.Split("\\")[incidentDocument.Split("\\").Length - 1]);
            request.AddFile("file", fileBytes, incidentDocument.Split("\\")[incidentDocument.Split("\\").Length - 1], $"image/{incidentDocument.Split(".")[1]}");

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                MessageBox.Show("Boletim de ocorrência enviado!");

            }
            else
            {
                MessageBox.Show("Ocorreu um erro ao enviar o boletim de ocorrência! Tente novamente.");
            }
            
        }

        protected async void GetIncidents()
        {

            await incidentsDataTable.Get<PaginatedResponse<dynamic>>("https://tsb-api-policy-engine.herokuapp.com/ocorrencia/", null, null);

            incidentsDataTable.DataBindingComplete += (sender, e) =>
            {
                string[] columns = { "local", "uf", "municipio", "descricao", "id_veiculo", "id_cliente", "deletar", "editar", "id_terceirizado" };
                incidentsDataTable.RemoveColumns(columns);
            };



            Controls.Add(incidentsDataTable, 0, 5);
            SetColumnSpan(incidentsDataTable, 3);
            SetRowSpan(incidentsDataTable, 4);
        }

        private void PutButton_Click(object? sender, EventArgs? e)
        {
            SubmitPanelSetup();
        }

        public Incidents(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }

    public class IncidentInsertReponse
    {
        public string message { get; set; }
        public Ocorrencia incident { get; set; }
    }

    public class IncidentDataResponse
    {
        public IEnumerable<Ocorrencia[]> data { get; init; }
        public int totalPages { get; set; }

    }
}
