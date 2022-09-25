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

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbDataTable : DataGridView
    {
        private string? selectedId { get; set; }
        
        public TsbDataTable()
        {
            SetupDataTable();
            ActionColumnSetup();
            OrganizeColumns();
            
        }
        
        public TsbDataTable(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        //public void selectId(object sender, DataGridViewCellEventArgs e)
        //{
        //    var grid = (DataGridView)sender;

        //    if (e.RowIndex < 0)
        //    {
        //        return;
        //    }

        //    if (e.ColumnIndex == this.Columns["Deletar"].Index && e.RowIndex >= 0)
        //    {
        //        selectedId = this.SelectedRows[0].Cells[2].Value.ToString();
        //        MessageBox.Show(selectedId);
        //    }      
        //}

        public string getSelectedId()
        {
            return selectedId;
        }

        public void removeRow(int id)
        {
            
            foreach (DataGridViewRow row in this.Rows)
            {
                if (row.Cells[0].Value.ToString() == id.ToString())
                {
                    this.Rows.Remove(row);
                    MessageBox.Show("Removido com sucesso!");
                }
            }

        }

        public void LoadData(DataTable source)
        {
            this.DataSource = source;
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
    
            this.Columns.Add(edit);
            this.Columns.Add(delete);
        }

        private void OrganizeColumns()
        {
            this.Columns["Editar"].Visible = true;

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
            this.RowTemplate.Height = 52;
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
    }
}