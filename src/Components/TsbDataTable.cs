using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text;
using Top_Seguros_Brasil_Desktop.Properties;
using Top_Seguros_Brasil_Desktop.src.Panels;
using Top_Seguros_Brasil_Desktop.Utils;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbDataTable : TableLayoutPanel
    {

        private string address { get; set; }

        public static ArrayList selectedRowValues = new ArrayList();
        public static string? selectedId { get; set; }
        public static bool tableCreated { get; set; }

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
                await SearchData<JObject>(searchBox.Text);
            };

            searchBox.KeyPress += async (sender, e) =>
            {
                if (e.KeyChar == (char)13)
                {
                    await SearchData<JObject>(searchBox.Text);
                }
            };

            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 16));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 72 + 16));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 336));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 56));

            int currentPage = 1;

            paginationRow.PageNumberText = "Página: 1";
            paginationRow.PreviousEnabled = false;

            paginationRow.nextButton.Click += async (sender, e) =>
            {
                currentPage++;
                await ChangeToPage<JObject>(currentPage, "next");
                
                paginationRow.PreviousEnabled = true;
                paginationRow.PageNumberText = "Página: " + currentPage.ToString();
            };

            paginationRow.previousButton.Click += async (sender, e) =>
            {
                currentPage--;

                await ChangeToPage<JObject>(currentPage, "previous");
                paginationRow.PageNumberText = "Página: " + currentPage.ToString();
                if(currentPage < maxPages)
                {
                    paginationRow.NextEnabled = true;
                }
            };


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

        public async Task Get<Type>(string address)
        {
            this.address = address;
            var response = await engineInterpreter.Request<IEnumerable<Type>>($"{address}?pageNumber=1", "GET", null);
            IEnumerable<Type> responseBody = response.Body;


            string[] properties = responseBody.First().GetType().GetProperties().Select(x => x.Name).ToArray();
            try
            {
                foreach (var property in properties) dataTable.Columns.Add(property);
                foreach (var item in responseBody)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (var property in properties) row[property] = item.GetType().GetProperty(property).GetValue(item);
                    dataTable.Rows.Add(row);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            await LoadData(dataTable);


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

        public async Task<Type?> GetSelected<Type>(string id)
        {
            this.address = address;
            var response = await engineInterpreter.Request<IEnumerable<Type>>($"{address}{id}", "GET", null);
            Type responseBody = response.Body;

            return responseBody;
        }

        public async Task LoadData(DataTable source)
        {
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
            InitializeComponent();
            dataGridView.EndEdit();

            this.Refresh();
        }

        public async Task ActionColumnSetup()
        {

            DataGridViewButtonColumn edit = new DataGridViewButtonColumn();

            edit.Name = "Editar";
            edit.HeaderText = "";
            edit.Text = "✏ Editar";
            edit.UseColumnTextForButtonValue = true;
            edit.FlatStyle = FlatStyle.Flat;
            edit.DefaultCellStyle.ForeColor = TsbColor.neutral;
            edit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //edit.DefaultCellStyle.Font = new Font(dataGridView.Font, FontStyle.Bold);
            edit.Width = 150;
            edit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
            delete.Name = "Deletar";
            delete.HeaderText = "";
            delete.Text = "🗑 Deletar";
            delete.FlatStyle = FlatStyle.Flat;
            delete.DefaultCellStyle.ForeColor = TsbColor.neutral;
            delete.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //delete.DefaultCellStyle.Font = new Font(dataGridView.Font, FontStyle.Bold);
            delete.UseColumnTextForButtonValue = true;
            delete.Width = 150;
            delete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;




            DataGridViewButtonColumn details = new DataGridViewButtonColumn();
            details.HeaderText = "";
            details.Text = "📝 Detalhes";
            details.Name = "Detalhes";
            details.FlatStyle = FlatStyle.Flat;
            details.UseColumnTextForButtonValue = true;
            details.DefaultCellStyle.ForeColor = TsbColor.neutral;
            details.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //details.DefaultCellStyle.Font = new Font(dataGridView.Font, FontStyle.Bold);
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

        public async Task SearchData<Type>(string value)
        {
            
            DataTable dataTable = new DataTable();

            var response = await engineInterpreter.Request<IEnumerable<JObject>>($"{address}?search={value}", "GET", null);
            IEnumerable<Type> responseBody = response.Body;

            
            if (responseBody.Count() != 0)
            {
                string[] properties = {""};
                string[] values = {""};

                IEnumerable<JObject> o = (IEnumerable<JObject>)responseBody;

                foreach (JObject j in o)
                {
                    properties = j.Properties().Select(p => p.Name).ToArray();
                }

                try
                {

                    foreach (var property in properties) dataTable.Columns.Add(property);

                    foreach (var item in o)
                    {
                        DataRow row = dataTable.NewRow();
                        for (int i = 0; i < properties.Length; i++)
                        {
                            row[i] = item[properties[i]];
                        }
                        dataTable.Rows.Add(row);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                await LoadData(dataTable);

                dataGridView.Refresh();
            }
            else
            {
                MessageBox.Show($"Nenhum resultado encontrado para {value}");
            }

        }

        public async Task ChangeToPage<Type>(int page, string direction)
        {
            DataTable dataTable = new DataTable();

            var response = await engineInterpreter.Request<IEnumerable<Type>>($"{address}?pageNumber={page}", "GET", null);
            
            IEnumerable<Type> responseBody = response.Body;


            if (responseBody.Count() != 0)
            {
                string[] properties = { "" };
                string[] values = { "" };

                IEnumerable<JObject> objectsList = (IEnumerable<JObject>)responseBody;

                foreach (JObject j in objectsList)
                {
                    properties = j.Properties().Select(p => p.Name).ToArray();
                }

                try
                {

                    foreach (var property in properties) dataTable.Columns.Add(property);

                    foreach (var item in objectsList)
                    {
                        DataRow row = dataTable.NewRow();
                        for (int i = 0; i < properties.Length; i++)
                        {
                            row[i] = item[properties[i]];
                        }
                        dataTable.Rows.Add(row);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
                await LoadData(dataTable);

                dataGridView.Refresh();


                


                if (direction == "next")
                {
                    page++;
                    var nextPageResponse = await engineInterpreter.Request<IEnumerable<Type>>($"{address}?pageNumber={page}", "GET", null);
                    IEnumerable<Type> nextBody = nextPageResponse.Body;

                    if (nextBody.Count() == 0)
                    {
                        paginationRow.PageNumberText = page.ToString();
                        paginationRow.NextEnabled = false;
                        maxPages = page;
                        return;
                    }

                }

                if (direction == "previous")
                {
                    page--;
                    var previousPageResponse = await engineInterpreter.Request<IEnumerable<Type>>($"{address}?pageNumber={page}", "GET", null);
                    IEnumerable<Type> previousBody = previousPageResponse.Body;


                    if (page == 0)
                    {
                        paginationRow.PageNumberText = page.ToString();
                        paginationRow.PreviousEnabled = false;
                        paginationRow.NextEnabled = true;
                        
                        return;
                    }


                    if (previousBody.Count() == 0)
                    {
                        paginationRow.PageNumberText = page.ToString();
                        paginationRow.PreviousEnabled = false;
                        return;
                    }
                }

            }
            else
            {
                MessageBox.Show($"Nenhum resultado encontrado para esse registro");
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
            dataGridView.DefaultCellStyle.Font = new Font(LoadFont(Resources.Roboto_Regular), 10, FontStyle.Regular);
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

            ActionColumnSetup();

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

        public void ReloadTable(string pageName, Control control)
        {
            if (pageName == "users")
            {
                Users usersPage = new Users();
                control.FindForm().Controls.Add(usersPage);
                usersPage.BringToFront();
                return;
            }
        }
    }
}