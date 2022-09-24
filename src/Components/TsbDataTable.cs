using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.Models;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbDataTable : DataGridView
    {

        DataGridView dataGridView = new DataGridView();
        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();

        private string? selectedId { get; set; }
        
        public TsbDataTable()
        {
            InitializeComponent();
        }

        public TsbDataTable(DataTable datasource)
        {
            SetupDataTable(datasource);
        }

        public TsbDataTable(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        
        public string getSelectedId() 
        {
            MessageBox.Show(selectedId);
            return selectedId;
        }

        public void removeRow(int id)
        {
            
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value.ToString() == id.ToString())
                {
                    dataGridView.Rows.Remove(row);
                    MessageBox.Show("Removido com sucesso");
                }
            }

        }

        private async void SetupDataTable(DataTable datasource)
        {
            this.Controls.Add(dataGridView);

            dataGridView.DataSource = datasource;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = TsbColor.secondary;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = TsbColor.neutralGray;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView.Font, FontStyle.Bold);

            dataGridView.Name = "Tabela padrão";
            this.Size = new Size(1109, 316);
            dataGridView.Location = new Point(0, 0);
            

            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView.GridColor = Color.Black;
            dataGridView.RowHeadersVisible = false;

            dataGridView.Size = this.Size;
            this.Location = new Point(32, 239);

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            buttonColumn.Name = "Acoes";
            buttonColumn.Text = "Deletar";
            
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView.AllowUserToResizeRows = false;

            dataGridView.ColumnHeadersHeight = 52;

            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            
            dataGridView.ReadOnly = true;


            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            //make every line height of the table 58 pixels
            dataGridView.RowTemplate.Height = 52;

            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.Dock = DockStyle.Fill;

            //get the first column cell value on select
            dataGridView.CellClick += (sender, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                    string cellValue = row.Cells[0].Value.ToString();
                    this.selectedId = cellValue;
                }
            };

            dataGridView.Height = (dataGridView.RowCount * 52) - 52;
            dataGridView.BorderStyle = BorderStyle.Fixed3D;

            
            dataGridView.ForeColor = TsbColor.neutralGray;
            
            dataGridView.Margin = new Padding(16);

            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView.AllowUserToAddRows = false;

            //dataGridView.DataBindingComplete += (sender, e) =>
            //{


            //};

            dataGridView.BackgroundColor = TsbColor.surface;
        }
    }
}