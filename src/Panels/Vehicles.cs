namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Vehicles : BasePanel
    {

        private static readonly HttpClient client = new HttpClient();
        public static ArrayList selectedVehicle = new ArrayList();
        TsbDataTable vehiclesDataTable = new TsbDataTable();
        EngineInterpreter engineInterpreter = new EngineInterpreter(token);

        public Vehicles()
        {
            InitializeComponent();
        }

        public Vehicles(string pageTitle, string subtitle)
        {


            ButtonTsbTertiary continueSubmit = new ButtonTsbTertiary
            {
                Text = "Buscar",
                Dock = DockStyle.Left,
                Width = 60,
                Margin = new Padding
                {
                    Top = 0,
                    Bottom = 0,
                    Left = 0,
                    Right = 8
                }
            };
            
            

            this.vehiclesDataTable.CellClick += async (sender, e) =>
            {

                if (e.RowIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex == vehiclesDataTable.Columns["Detalhes"].Index && e.RowIndex >= 0)
                {

                    if (vehiclesDataTable.Columns["Detalhes"].Index == 0)
                    {
                        selectedVehicle.Add(vehiclesDataTable.SelectedRows[0].Cells[3].Value.ToString());
                        await SubmitPanelSetup<Veiculo>(selectedVehicle[0].ToString());
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < vehiclesDataTable.Columns.Count; i++)
                        {
                            selectedVehicle.Add(vehiclesDataTable.SelectedRows[0].Cells[i].Value.ToString());
                        }
                        await SubmitPanelSetup<Veiculo>(selectedVehicle[0].ToString());
                        return;
                    }

                }

            };



            SubTitle(subtitle);
            Title(pageTitle);
            InitializeComponent();

            GetVehicles();

            

            InitializeComponent();
        }
        
        protected async void GetVehicles()
        {

            await vehiclesDataTable.Get<PaginatedResponse<dynamic>>("https://tsb-api-policy-engine.herokuapp.com/veiculo/", null, null);

            vehiclesDataTable.DataBindingComplete += (sender, e) =>
            {
                
                string[] columns = { "editar", "deletar", "ano", "renavam", "uso", "sinistrado", "id_cliente" };

                vehiclesDataTable.RemoveColumns(columns);

            };

            Controls.Add(vehiclesDataTable, 0, 5);
            SetColumnSpan(vehiclesDataTable, 3);
            SetRowSpan(vehiclesDataTable, 4);
        }

        private async Task SubmitPanelSetup<Type>(string id)
        {
            EngineInterpreterResponse response;


            string address = "https://tsb-api-policy-engine.herokuapp.com/veiculo/";

            response = await engineInterpreter.Request<Type>($"{address}{id}", "GET", null);
            Veiculo vehicleBody = response.Body;


            SubmitPanel submitPanel = new SubmitPanel();

            TitleBox titlebox = new TitleBox
            {
                Parent = submitPanel,
                titleText = $"Editar Veículo {vehicleBody.marca}",
                subtitleText = "Editar veículo. ",
                Margin = new Padding(32),
            };
            submitPanel.Controls.Add(titlebox, 0, 1);

            submitPanel.Controls.OfType<TitleBox>().First().GoBack += (sender, e) =>
            {
                selectedVehicle.Clear();
                submitPanel.Dispose();
            };

            var brandsResponse = await engineInterpreter.Request<IEnumerable<Marca>>("https://tsb-api-policy-engine.herokuapp.com/fipe/marcas", "GET", null);
            IEnumerable<Marca> brandsBody = brandsResponse.Body;

            TsbComboBox brandField = new TsbComboBox
            {
                LabelText = "Marca",
                HintText = $"{vehicleBody.marca}",
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

            // on click to brand field, model field should be enabled
            brandField.Click += async (sender, e) =>
            {
                var modelsResponse = await engineInterpreter.Request<IEnumerable<Modelo>>($"https://tsb-api-policy-engine.herokuapp.com/fipe/marcas/{brandField.Text}/modelos", "GET", null);
                IEnumerable<Modelo> modelsBody = modelsResponse.Body;

                TsbComboBox modelField = new TsbComboBox
                {
                    LabelText = "Modelo",
                    HintText = $"{vehicleBody.modelo}",
                    Dock = DockStyle.Top,
                    Margin = new Padding
                    {
                        Top = 0,
                        Bottom = 24,
                        Left = 32,
                        Right = 32
                    }
                };

                foreach (Modelo model in modelsBody)
                {
                    modelField.Items.Add(model.nome);
                }
                submitPanel.Controls.Add(modelField, 1, 2);

                // on click to model field, year field should be enabled
                modelField.Click += async (sender, e) =>
          {
              var yearsResponse = await engineInterpreter.Request<IEnumerable<Ano>>($"https://tsb-api-policy-engine.herokuapp.com/fipe/marcas/{brandField.Text}/modelos/{modelField.Text}/anos", "GET", null);
              IEnumerable<Ano> yearsBody = yearsResponse.Body;

              TsbComboBox yearField = new TsbComboBox
              {
                  LabelText = "Ano",
                  HintText = $"{vehicleBody.ano}",
                  Dock = DockStyle.Top,
                  Margin = new Padding
                  {
                      Top = 0,
                      Bottom = 24,
                      Left = 32,
                      Right = 32
                  }
              };

              foreach (Ano year in yearsBody)
              {
                  yearField.Items.Add(year.nome);
              }
              submitPanel.Controls.Add(yearField, 1, 3);
          };
            };

            TsbComboBox modelField = new TsbComboBox
            {
                LabelText = "Modelo",
                HintText = $"{vehicleBody.modelo}",
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
                HintText = $"{vehicleBody.ano}",
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
                HintText = $"{vehicleBody.placa}",
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
                Text = $"{vehicleBody.renavam}",
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
                Enabled = true,
                Checked = (bool)vehicleBody.sinistrado,
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
                Text = $"{vehicleBody.uso}",
                Dock = DockStyle.Top,
                Enabled = true,
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
                    marca: brandField.HintText,
                    modelo: modelField.HintText,
                    ano: yearField.HintText,
                    renavam: renavamField.Text,
                    placa: plateField.Text,
                    uso: usageField.Text,
                    sinistrado: checkBox.Checked,
                    idCliente: vehicleBody.id_cliente
                );

                await PutVehicle(vehicle, vehicleBody.id_veiculo.ToString());
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

        protected async Task PutVehicle(Veiculo vehicleData, string id)
        {
            await vehiclesDataTable.Put<Veiculo>(vehicleData, id, null);
            
        }

        public Vehicles(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
