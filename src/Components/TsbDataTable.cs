using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text;
using Top_Seguros_Brasil_Desktop.Properties;
using Top_Seguros_Brasil_Desktop.src.Models;
using Top_Seguros_Brasil_Desktop.src.Panels;
using Top_Seguros_Brasil_Desktop.Utils;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbDataTable : DataGridView
    {

        private string address { get; set; }

        public static ArrayList selectedRowValues = new ArrayList();
        public static string? selectedId { get; set; }

        public static bool tableCreated { get; set; }

        public DataTable dataTable = new DataTable();

        public BindingSource bindingSource = new BindingSource();

        EngineInterpreter engineInterpreter = new EngineInterpreter(BasePanel.token);
        private readonly PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        public TsbDataTable()
        {

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
            DataSource = bindingSource;
            SetupDataTable();
            InitializeComponent();
            this.EndEdit();
            this.Refresh();
        }
        
        public void ActionColumnSetup()
        {

            DataGridViewButtonColumn edit = new DataGridViewButtonColumn();
            edit.Name = "Editar";
            edit.HeaderText = "Editar";
            edit.Text = "Editar";
            edit.UseColumnTextForButtonValue = true;
            edit.FlatStyle = FlatStyle.Flat;
            edit.DefaultCellStyle.ForeColor = TsbColor.neutralGray;
            edit.DefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);

            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
            delete.Name = "Deletar";
            delete.HeaderText = "Deletar";
            delete.Text = "Deletar";
            delete.FlatStyle = FlatStyle.Flat;
            delete.DefaultCellStyle.ForeColor = TsbColor.neutralGray;
            delete.DefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
            delete.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn details = new DataGridViewButtonColumn();
            details.HeaderText = "Detalhes";
            details.Text = "Detalhes";
            details.Name = "Detalhes";
            details.FlatStyle = FlatStyle.Flat;
            details.UseColumnTextForButtonValue = true;


            DataBindingComplete += (sender, e) =>
            {
                if (Columns.Contains("Editar") && Columns.Contains("Deletar") && Columns.Contains("Detalhes")) ActionButtonsSetup();
                else
                {
                    this.Columns.Add(details);
                    this.Columns.Add(edit);
                    this.Columns.Add(delete);
                    ActionButtonsSetup();
                }
            };


        }
        public void ActionButtonsSetup()
        {
            if (Columns.Contains("Editar") && Columns.Contains("Deletar"))
            {
                Columns["Editar"].DisplayIndex = this.Columns.Count - 1;
                Columns["Deletar"].DisplayIndex = this.Columns.Count - 1;
                CellClick += new DataGridViewCellEventHandler(DeleteButton_Click);
            }
            else
            {
                return;
            }
        }

        
        public async Task SearchData<Type>(string value)
        {

            DataTable dataTable = new DataTable();

            var response = await engineInterpreter.Request<IEnumerable<Type>>($"{address}?search={value}", "GET", null);
            IEnumerable<Type> responseBody = response.Body;


            if (responseBody.Count() != 0)
            {
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                await LoadData(dataTable);

                this.Refresh();
            }
            else
            {
                MessageBox.Show("Não há mais registros!");
            }

        }
        
        public async Task ChangeToPage<Type>(int page)
        {
            DataTable dataTable = new DataTable();

            var response = await engineInterpreter.Request<IEnumerable<Type>>($"{address}?pageNumber={page}", "GET", null);
            IEnumerable<Type> responseBody = response.Body;


            if (responseBody.Count() != 0)
            {
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                await LoadData(dataTable);

                this.Refresh();
            }
            else
            {
                MessageBox.Show("Não há mais registros!");
            }

        }

        public void SetupDataTable()
        {
            this.ColumnHeadersDefaultCellStyle.BackColor = TsbColor.secondary;
            this.ColumnHeadersDefaultCellStyle.ForeColor = TsbColor.neutralGray;
            this.ColumnHeadersDefaultCellStyle.Font = new Font(LoadFont(Resources.Roboto_Black), 10);

            this.DefaultCellStyle.ForeColor = TsbColor.neutralGray;
            this.DefaultCellStyle.Font = new Font(LoadFont(Resources.Roboto_Light), 10);

            this.Name = "Tabela padrão";
            this.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            this.GridColor = TsbColor.neutralWhite;
            this.RowHeadersVisible = false;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.AllowUserToResizeColumns = false;
            this.AllowUserToResizeRows = false;
            this.ColumnHeadersHeight = 52;
            this.RowTemplate.Height = 52;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ReadOnly = true;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.MultiSelect = false;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.ForeColor = TsbColor.neutralGray;
            this.Margin = new Padding(32);
            this.CellBorderStyle = DataGridViewCellBorderStyle.None;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.AllowUserToAddRows = false;
            this.BackgroundColor = TsbColor.surface;
            this.Height = 368;
            this.Dock = DockStyle.Top;

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

            int rowIndex = this.CurrentCell.RowIndex;
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == this.Columns["Deletar"].Index && e.RowIndex >= 0)
            {

                selectedId = this.SelectedRows[0].Cells[0].Value.ToString();
                if (MessageBox.Show(
                    "Deseja realmente deletar o registro " + selectedId + "?",
                    "Deletar registro",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2
                    ) == DialogResult.Yes)
                {

                    await Delete<Type>(selectedId);

                    foreach (DataGridViewCell oneCell in this.SelectedCells)
                    {
                        if (oneCell.Selected)
                            this.Rows.RemoveAt(oneCell.RowIndex);
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