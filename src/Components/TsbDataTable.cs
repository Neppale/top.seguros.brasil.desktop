namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbDataTable : TableLayoutPanel
    {

        private string address { get; set; }

        public static ArrayList selectedRowValues = new ArrayList();
        public static string? selectedId { get; set; }
        public static bool tableCreated { get; set; }
        public static Type current_type { get; set; }

        public int maxPages { get; set; }

        public DataTable dataTable = new DataTable();

        public DataGridView dataGridView = new DataGridView();

        public event DataGridViewBindingCompleteEventHandler DataBindingComplete
        {
            add
            {
                dataGridView.DataBindingComplete += value;
            }
            remove
            {
                dataGridView.DataBindingComplete -= value;
            }
        }

        public DataGridViewColumnCollection Columns
        {
            get
            {
                return dataGridView.Columns;
            }
        }

        public DataGridViewSelectedRowCollection SelectedRows
        {
            get
            {
                return dataGridView.SelectedRows;
            }
        }

        public TsbPaginationRow paginationRow = new TsbPaginationRow
        {
            Dock = DockStyle.Top
        };

        public TsbSearchBox searchBox = new TsbSearchBox
        {
            Margin = new Padding(16, 0, 0, 0)
        };

        public event DataGridViewCellEventHandler CellClick
        {
            add
            {
                dataGridView.CellClick += value;
            }
            remove
            {
                dataGridView.CellClick -= value;
            }

        }

        public BindingSource bindingSource = new BindingSource();

        EngineInterpreter engineInterpreter = new EngineInterpreter(BasePanel.token);

        private readonly PrivateFontCollection privateFontCollection = new PrivateFontCollection();
        public new string Text
        {
            get
            {
                return searchBox.Text;
            }
            set
            {
                searchBox.Text = value;
            }
        }

        public event EventHandler SearchClick
        {
            add
            {
                searchBox.SearchClick += value;
            }
            remove
            {
                searchBox.SearchClick -= value;

            }
        }

        public event KeyPressEventHandler KeyPress
        {
            add
            {
                searchBox.KeyPress += value;
            }
            remove
            {
                searchBox.KeyPress -= value;
            }
        }

        public event EventHandler Next
        {
            add
            {
                paginationRow.ClickNext += value;
            }
            remove
            {
                paginationRow.ClickNext -= value;
                
            }
        }

        public event EventHandler Previous
        {
            add
            {
                paginationRow.ClickPrevious += value;
            }
            remove
            {
                paginationRow.ClickPrevious -= value;

            }
        }

        public DataGridViewRowCollection Rows
        {
            get
            {
                return dataGridView.Rows;
            }
        }

        public TsbDataTable()
        {
            this.AutoSize = true;
            this.Dock = DockStyle.Top;
            this.BackColor = TsbColor.surface;
            this.Padding = new Padding(1, 0, 1, 0);
            this.Margin = new Padding(32, 0, 32, 0);

            this.Paint += (sender, e) =>
            {
                Rectangle borderRectangle = this.ClientRectangle;
                ControlPaint.DrawBorder(e.Graphics, borderRectangle,
                    TsbColor.neutralWhite, ButtonBorderStyle.Solid);

            };

            searchBox.SearchClick += async (sender, e) =>
            {
                await Get<PaginatedResponse<dynamic>>(this.address, null, searchBox.Text);
            };

            searchBox.KeyPress += async (sender, e) =>
            {
                if (e.KeyChar == (char)13)
                {
                    await Get<PaginatedResponse<dynamic>>(this.address, null, searchBox.Text);
                }
            };
             
            int page = 1;
            
            paginationRow.ClickNext += async (sender, e) =>
            {
                page += 1;
                await Get<PaginatedResponse<dynamic>>(this.address, page, null);
            };

            this.paginationRow.ClickPrevious += async (sender, e) =>
            {
                page--;
                await Get<PaginatedResponse<dynamic>>(this.address, page, null);
            };

            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 16));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 72 + 16));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 336));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 56));


            paginationRow.PreviousEnabled = false;


            this.Controls.Add(paginationRow, 0, 3);

            this.Controls.Add(searchBox, 0, 1);

            this.Controls.Add(dataGridView, 0, 2);



            InitializeComponent();
        }

        public TsbDataTable(string adress, Type type)
        {
            SetupDataTable();
            this.address = adress;

        }

        public async Task Get<type>(string address, int? page, string? searchData)
        {
            dataTable.Clear();
            this.address = address;
            paginationRow.NextEnabled = false;
            
            if (page == null) { page = 1; };
            if (page > 1)     { paginationRow.PreviousEnabled = true; }
            if (page == 1)    { paginationRow.PreviousEnabled = false; }
            if (page <= 0)    { page = 1; };
            
            if (searchData == null) { address = address + "?pageNumber=" + page; }
            if (searchData != null) { address = address + $"?search={searchData}&" + "?pageNumber=" + page; }


            var response = await engineInterpreter.Request<type>(address, "GET", null);


            IEnumerable<object> responseBody = response.Body.data;


            if (responseBody.Count() != 0)
            {
                try
                {
                    foreach (var columnName in (JObject)responseBody.First())
                    {
                        if (!dataTable.Columns.Contains(columnName.Key))
                            dataTable.Columns.Add(columnName.Key);

                    }

                    foreach (var item in responseBody)
                    {
                        DataRow row = dataTable.NewRow();
                        foreach (var columnName in (JObject)item)
                        {
                            row[columnName.Key] = columnName.Value;
                        }
                        dataTable.Rows.Add(row);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }

                if (response.TotalPages > page)
                {
                    paginationRow.NextEnabled = true;
                }

                
                paginationRow.PageNumberText = $"Página: {page}/{response.TotalPages.ToString()}";
                await LoadData(dataTable);
            }
            else
            {
                dataTable.Columns.Add("Nenhum dado cadastrado.");

                bindingSource.DataSource = dataTable;
                dataGridView.DataSource = bindingSource;

                paginationRow.NextEnabled = false;
                paginationRow.PreviousEnabled = false;

                SetupDataTable();
                InitializeComponent();
                dataGridView.EndEdit();
                this.Refresh();
            }

        }


        public async Task Post<Type>(object body)
        {

            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await engineInterpreter.Request<Type>($"{this.address}", "POST", data);

            try
            {
                if (response.StatusCode == 201) MessageBox.Show("Cadastrado com sucesso!");
                else if (response.Body == null) MessageBox.Show("Erro ao cadastrar!");
                else MessageBox.Show("Erro ao cadastrar! " + response.Body.message);

                await Get<PaginatedResponse<dynamic>>(address, 1, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public async Task Put<Type>(object body, string id)
        {
            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await engineInterpreter.Request<Type>($"{this.address}{id}", "PUT", data);

            if (response.StatusCode == 200) MessageBox.Show("Atualizado com sucesso!");
            else if (response.Body == null) MessageBox.Show("Erro ao atualizar!");
            else MessageBox.Show("Erro ao atualizar! " + response.Body.message);

            await Get<PaginatedResponse<dynamic>>(address, 1, null);
        }

        public async Task Delete<Type>(string id)
        {
            var response = await engineInterpreter.Request<Type>($"{this.address}{id}", "DELETE", null);

            if (response.StatusCode == 204) MessageBox.Show("Deletado com sucesso!");
            else if (response.Body == null) MessageBox.Show("Erro ao deletar!");
            else MessageBox.Show("Erro ao deletar! " + response.Body.message);
        }

        public TsbDataTable(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        public async Task LoadData(DataTable source)
        {
            bindingSource.ResetBindings(false);
            bindingSource.DataSource = source;
            dataGridView.DataSource = bindingSource;

            dataGridView.DataBindingComplete += async (sender, e) =>
            {

                foreach (DataGridViewColumn column in dataGridView.Columns)
                {

                    if (column.HeaderText.Contains("_"))
                    {
                        column.HeaderText = column.HeaderText.Replace("_", " ");
                    }

                    if (!string.IsNullOrEmpty(column.HeaderText))
                    {
                        column.HeaderText = column.HeaderText.Substring(0, 1).ToUpper() + column.HeaderText.Substring(1);
                    }
                }

                ActionColumnSetup();

                ActionButtonsSetup();

            };

            SetupDataTable();
            ActionColumnSetup();
            InitializeComponent();
            dataGridView.EndEdit();

            this.Refresh();
        }

        public async Task ActionColumnSetup()
        {

            DataGridViewButtonColumn edit = new DataGridViewButtonColumn();

            edit.Name = "Editar";
            edit.HeaderText = "";
            edit.Text = "✏ EDITAR";
            edit.UseColumnTextForButtonValue = true;
            edit.FlatStyle = FlatStyle.Flat;
            edit.DefaultCellStyle.ForeColor = TsbColor.neutral;
            edit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            edit.DefaultCellStyle.Font = new Font(LoadFont(Resources.Roboto_Regular), 10, FontStyle.Regular);
            edit.Width = 150;
            edit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
            delete.Name = "Deletar";
            delete.HeaderText = "";
            delete.Text = "🗑 DELETAR";
            delete.FlatStyle = FlatStyle.Flat;
            delete.DefaultCellStyle.ForeColor = TsbColor.neutral;
            delete.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            delete.DefaultCellStyle.Font = new Font(LoadFont(Resources.Roboto_Regular), 10, FontStyle.Regular);
            delete.UseColumnTextForButtonValue = true;
            delete.Width = 150;
            delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;




            DataGridViewButtonColumn details = new DataGridViewButtonColumn();
            details.HeaderText = "";
            details.Text = "📝 Detalhes";
            details.Name = "DETALHES";
            details.FlatStyle = FlatStyle.Flat;
            details.UseColumnTextForButtonValue = true;
            details.DefaultCellStyle.ForeColor = TsbColor.neutral;
            details.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            details.DefaultCellStyle.Font = new Font(LoadFont(Resources.Roboto_Regular), 10, FontStyle.Regular);
            details.Width = 150;
            details.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


            dataGridView.DataBindingComplete += (sender, e) =>
            {
                if (dataGridView.Columns.Contains("Editar") && dataGridView.Columns.Contains("Deletar") && dataGridView.Columns.Contains("Detalhes")) ActionButtonsSetup();
                else
                {
                    dataGridView.Columns.Add(details);
                    dataGridView.Columns.Add(edit);
                    dataGridView.Columns.Add(delete);
                    ActionButtonsSetup();
                }
            };

            return;
        }

        public async Task ActionButtonsSetup()
        {

            if (dataGridView.Columns.Contains("Editar") && dataGridView.Columns.Contains("Deletar") && dataGridView.Columns.Contains("Detalhes"))
            {
                dataGridView.Columns["Detalhes"].DisplayIndex = dataGridView.Columns.Count - 1;
                dataGridView.Columns["Editar"].DisplayIndex = dataGridView.Columns.Count - 1;
                dataGridView.Columns["Deletar"].DisplayIndex = dataGridView.Columns.Count - 1;


                dataGridView.CellClick += new DataGridViewCellEventHandler(DeleteButton_Click);
            }
            else
            {
                return;
            }
        }
        
        public void RemoveColumns(string[] columns)
        {

            foreach (string column in columns)
            {
                if (dataGridView.Columns.Contains(column))
                {
                    dataGridView.Columns[column].Visible = false;
                }
            }

        }

        public void SetupDataTable()
        {
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = TsbColor.surface;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = TsbColor.neutral;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = TsbColor.neutral;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = TsbColor.surface;


            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(LoadFont(Resources.Roboto_Bold), 13.5F, FontStyle.Bold);
            dataGridView.EnableHeadersVisualStyles = false;

            dataGridView.DefaultCellStyle.ForeColor = TsbColor.neutralGray;
            dataGridView.DefaultCellStyle.Font = new Font(LoadFont(Resources.Roboto_Medium), 10, FontStyle.Regular);
            dataGridView.DefaultCellStyle.SelectionBackColor = TsbColor.neutralGrayDarker;
            dataGridView.DefaultCellStyle.SelectionForeColor = TsbColor.neutral;
            dataGridView.DefaultCellStyle.Padding = new Padding(16, 0, 0, 0);


            dataGridView.CellMouseEnter += (sender, e) =>
            {

                if (e.RowIndex >= 0)
                {
                    dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = TsbColor.neutralGrayDarker;
                    dataGridView.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(LoadFont(Resources.Roboto_Medium), 10, FontStyle.Regular);
                }

            };

            dataGridView.CellMouseLeave += (sender, e) =>
            {

                if (e.RowIndex >= 0)
                {
                    dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = TsbColor.surface;
                    dataGridView.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(LoadFont(Resources.Roboto_Regular), 10, FontStyle.Regular);
                }

            };


            dataGridView.DataBindingComplete += (sender, e) =>
            {
                dataGridView.ClearSelection();
            };


            dataGridView.Name = "Tabela padrão";
            dataGridView.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView.GridColor = TsbColor.neutralWhite;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.ColumnHeadersHeight = 56;
            dataGridView.RowTemplate.Height = 56;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView.ReadOnly = true;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView.AllowUserToAddRows = false;
            dataGridView.BackgroundColor = TsbColor.surface;
            dataGridView.Height = 56 * 6;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Margin = new Padding(0, 0, 0, 0);
            dataGridView.ScrollBars = ScrollBars.None;

        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pvd, [In] ref uint pcFonts);

        private FontFamily LoadFont(byte[] fontResource)
        {
            int num = fontResource.Length;
            IntPtr intPtr = Marshal.AllocCoTaskMem(num);
            Marshal.Copy(fontResource, 0, intPtr, num);
            uint pcFonts = 0u;
            AddFontMemResourceEx(intPtr, (uint)fontResource.Length, IntPtr.Zero, ref pcFonts);
            privateFontCollection.AddMemoryFont(intPtr, num);
            return privateFontCollection.Families.Last();
        }

        private async void DeleteButton_Click(object? sender, DataGridViewCellEventArgs e)
        {

            int rowIndex = dataGridView.CurrentCell.RowIndex;
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == dataGridView.Columns["Deletar"].Index && e.RowIndex >= 0)
            {

                //select the first value of the selected row 

                selectedId = dataGridView.Rows[rowIndex].Cells[0].Value.ToString();

                //selectedId = dataGridView.Rows[rowIndex].Cells.Cast<DataGridViewCell>().First(c => c.OwningColumn.HeaderText.ToLower().Contains("id")).Value.ToString();

                if (MessageBox.Show(
                    "Deseja realmente deletar o registro " + selectedId + "?",
                    "Deletar registro",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2
                    ) == DialogResult.Yes)
                {

                    await Delete<Type>(selectedId);

                    foreach (DataGridViewCell oneCell in dataGridView.SelectedCells)
                    {
                        if (oneCell.Selected)
                            dataGridView.Rows.RemoveAt(oneCell.RowIndex);
                    }
                };
            }
        }

    }
}