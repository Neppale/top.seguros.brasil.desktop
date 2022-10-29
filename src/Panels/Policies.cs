namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Policies : BasePanel
    {
        private static readonly HttpClient client = new HttpClient();
        private static int selectedItemId;
        public ArrayList selectedPolicy = new ArrayList();
        TsbDataTable policiesDataTable = new TsbDataTable();
        EngineInterpreter engineInterpreter = new EngineInterpreter(token);

        public Policies()
        {

        }

        public Policies(string pageTitle, string subtitle)
        {

            this.policiesDataTable.CellClick += async (sender, e) =>
            {

                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == policiesDataTable.Columns["Detalhes"].Index && e.RowIndex >= 0)
                {

                    if (policiesDataTable.Columns["Detalhes"].Index == 0)
                    {
                        selectedPolicy.Add(policiesDataTable.SelectedRows[0].Cells[3].Value.ToString());
                        await SubmitPanelSetup<Apolice>(selectedPolicy[0].ToString());
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < policiesDataTable.Columns.Count; i++)
                        {
                            selectedPolicy.Add(policiesDataTable.SelectedRows[0].Cells[i].Value.ToString());
                        }
                        await SubmitPanelSetup<Apolice>(selectedPolicy[0].ToString());
                        return;
                    }

                }

            };


            InitializeComponent();

            SubTitle(subtitle);
            Title(pageTitle);

            GetPolicies();
        }

        protected async void GetPolicies()
        {

            await policiesDataTable.Get<PaginatedResponse<Apolice>>($"https://tsb-api-policy-engine.herokuapp.com/apolice/usuario/{userId}");


            policiesDataTable.DataBindingComplete += (sender, e) =>
            {
                string[] columns = { "data_inicio", "data_fim", "premio", "indenizacao", "id_cobertura", "id_usuario", "id_cliente", "id_veiculo", "message", "Deletar", "Editar" };
                policiesDataTable.RemoveColumns(columns);


                if (policiesDataTable.Rows.Count != 1)
                {
                    foreach (DataGridViewRow row in policiesDataTable.Rows)
                    {
                        if (row.Cells["status"].Value.ToString() == "Ativa")
                        {
                            row.Cells["status"].Style.ForeColor = TsbColor.success;
                        }

                        if (row.Cells["status"].Value.ToString() == "Em Análise")
                        {
                            row.Cells["status"].Value = "Aguardando análise";
                            row.Cells["status"].Style.ForeColor = TsbColor.wating;
                        }

                        if (row.Cells["status"].Value.ToString() == "Aguardando análise")
                        {
                            row.Cells["status"].Style.ForeColor = TsbColor.wating;
                        }

                        if (row.Cells["status"].Value.ToString() == "Rejeitada")
                        {
                            row.Cells["status"].Style.ForeColor = TsbColor.error;
                        }
                    }
                }



            };

            Controls.Add(policiesDataTable, 0, 5);
            SetColumnSpan(policiesDataTable, 3);
        }

        private async void SubmitPanelSetup(string idCliente, string idVeiculo)
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

                await PostPolicy(policy, null);
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
                selectedPolicy.Clear();
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

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var uri = new Uri("https://tsb-api-policy-engine.herokuapp.com/apolice/documento/" + requestResponse.id_apolice);

                await using var s = await client.GetStreamAsync(uri);

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = requestResponse.cliente.nome_completo + "-apolice.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    await using var file = File.Create(saveFileDialog.FileName);
                    await s.CopyToAsync(file);
                }
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

        protected async Task PostPolicy(Apolice policyData, EventHandler? e)
        {
            var engineInterpreter = new EngineInterpreter(token);

            var json = JsonConvert.SerializeObject(policyData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await engineInterpreter.Request<Apolice>("https://tsb-api-policy-engine.herokuapp.com/apolice/", "POST", data);


            Controls.Remove(policiesDataTable);
            GetPolicies();
        }

        public Policies(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }

    public class PolicyRequestResponse
    {
        public int id_apolice { get; set; }
        public string? data_inicio { get; set; }
        public string? data_fim { get; set; }
        public double? premio { get; set; }
        public double? indenizacao { get; set; }

        public EnrichedPolicy? enrichedPolicy { get; set; }

        public Cobertura? cobertura { get; set; }
        public Usuario? usuario { get; set; }
        public Cliente? cliente { get; set; }
        public Veiculo? veiculo { get; set; }

        public string? status { get; set; }

    }

    public class PolicyToGenerate
    {
        public int id_cliente { get; set; }
        public int id_veiculo { get; set; }
        public int id_cobertura { get; set; }

    }
}