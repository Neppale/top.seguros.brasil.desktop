using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Top_Seguros_Brasil_Desktop.src.Models;
using Top_Seguros_Brasil_Desktop.Utils;
using Top_Seguros_Brasil_Desktop.src.Panels;
using Newtonsoft.Json;
using System.Collections;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbDataTable : DataGridView
    {

        private string address { get; set; }
        private string? selectedId { get; set; }

        public static ArrayList selectedRowValues = new ArrayList();

        EngineInterpreter engineInterpreter = new EngineInterpreter(BasePanel.token);

        public TsbDataTable()
        {
           
        }

        public TsbDataTable(string adress, Type type)
        {
            this.address = adress;
        }
        
        public async void Get<Type>()
        {
            DataTable dataTable = new DataTable();

            string page = "?pageNumber=1";
            var response = await engineInterpreter.Request<IEnumerable<Type>>($"{this.address}", "GET", null);
            IEnumerable<Type> responseBody = response.Body;


            string[] properties = responseBody.First().GetType().GetProperties().Select(x => x.Name).ToArray();
            try
            {
                foreach(var property in properties)
                {
                    dataTable.Columns.Add(property);
                }

                foreach (var item in responseBody)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (var property in properties)
                    {
                        row[property] = item.GetType().GetProperty(property).GetValue(item);
                    }
                    dataTable.Rows.Add(row);
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            LoadData(dataTable);
        }

        public async void Post<Type>(object body)
        {
            
            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await engineInterpreter.Request<Type>($"{this.address}", "POST", data);

            try
            {
                if (response.StatusCode == 201)
                {
                    MessageBox.Show("Cadastrado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar! " + response.Body.message);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            //Get<Type>();

        }

        public async void Put<Type>(object body)
        {
            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await engineInterpreter.Request<Type>($"{this.address}", "PUT", data);

            if(response.StatusCode == 201)
            {
                MessageBox.Show("Atualizado com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao atualizar! " + response.Body.message);
            }
        }
        
        public async void Delete<Type>(string id)
        {
            var response = await engineInterpreter.Request<Type>($"{this.address}{id}", "DELETE", null);   

            if (response.StatusCode == 204)
            {
                MessageBox.Show("Deletado com sucesso!");
            }
            else
            {
                if (response.Body == null)
                {
                    MessageBox.Show("Erro ao deletar!");
                }
                else
                {
                    MessageBox.Show("Erro ao deletar! " + response.Body.message);
                }
            }

            //Get<Type>();
        }

        public TsbDataTable(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        public string getSelectedId()
        {
            return selectedId;
        }

        public void LoadData(DataTable source)
        {
            this.DataSource = source;
            ActionColumnSetup();
            OrganizeColumns();
            SetupDataTable();
            InitializeComponent();
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

            this.CellClick += new DataGridViewCellEventHandler(DeleteButton_Click);
            this.CellClick += new DataGridViewCellEventHandler(EditButton_Click);

            this.Columns.Add(edit);
            this.Columns.Add(delete);
        }

        private void OrganizeColumns()
        {

            this.DataBindingComplete += (sender, e) =>
            {
                this.Columns["Editar"].DisplayIndex = this.Columns.Count - 1;
                this.Columns["Deletar"].DisplayIndex = this.Columns.Count - 1;
            };

        }
        
        public void SetupDataTable()
        {
            this.ColumnHeadersDefaultCellStyle.BackColor = TsbColor.secondary;
            this.ColumnHeadersDefaultCellStyle.ForeColor = TsbColor.neutralGray;
            this.ColumnHeadersDefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
            this.Name = "Tabela padrão";
            this.Size = new Size(1109, 316);
            this.Location = new Point(0, 0);
            this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            this.GridColor = Color.Black;
            this.RowHeadersVisible = false;
            this.Location = new Point(32, 239);
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.AllowUserToResizeRows = false;
            this.ColumnHeadersHeight = 52;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ReadOnly = true;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.MultiSelect = false;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.ForeColor = TsbColor.neutralGray;
            this.Margin = new Padding(16);
            this.CellBorderStyle = DataGridViewCellBorderStyle.None;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.AllowUserToAddRows = false;
            this.BackgroundColor = TsbColor.surface;
        }

        private void DeleteButton_Click(object? sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == this.Columns["Deletar"].Index && e.RowIndex >= 0)
            {
                string selectedId = this.SelectedRows[0].Cells[0].Value.ToString();
                if(MessageBox.Show(
                    "Deseja realmente deletar o registro " + selectedId + "?",
                    "Deletar registro",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2
                    ) == DialogResult.Yes )
                    { Delete<Type>(selectedId); };

            }
        }

        private void EditButton_Click(object? sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == Columns["Editar"].Index && e.RowIndex >= 0)
            {

                for (int i = 0; i < Columns.Count; i++)
                {
                    selectedRowValues.Add(SelectedRows[0].Cells[i].Value.ToString());
                }

                string selectedId = SelectedRows[0].Cells[0].Value.ToString();
                MessageBox.Show(selectedId);
            }

        }

    }
}