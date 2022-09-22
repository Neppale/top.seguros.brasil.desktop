using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public TsbDataTable()
        {
            
            InitializeComponent();
        }

        public TsbDataTable(IEnumerable<UsuarioDTO> datasource)
        {
            SetupDataTable(datasource);
        }

        public TsbDataTable(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void SetupDataTable(IEnumerable<UsuarioDTO> datasource)
        {
            this.Controls.Add(dataGridView);

            dataGridView.DataSource = datasource;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = TsbColor.secondary;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = TsbColor.neutralGray;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView.Font, FontStyle.Bold);

            //Receber o nome da tabela por metodo
            dataGridView.Name = "Tabela padrão";

            dataGridView.Location = new Point(0, 0);
            this.Size = new Size(1109, 316);

            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView.GridColor = Color.Black;
            dataGridView.RowHeadersVisible = false;

            dataGridView.Size = this.Size;
            this.Location = new Point(32, 239);


            buttonColumn.Name = "Acoes";
            buttonColumn.Text = "Deletar";


           // dataGridView.CellClick += 

            if (dataGridView.Columns["Acoes"] == null)
            {
                dataGridView.Columns.Insert(dataGridView.Columns.Count, buttonColumn);
            }

            //dataGridView.Columns[0].Name = "Coluna 1";
            //dataGridView.Columns[1].Name = "Coluna 2";
            //dataGridView.Columns[2].Name = "Coluna 3";
            //dataGridView.Columns[3].Name = "Coluna 4";
            //dataGridView.Columns[4].Name = "Coluna 5";
            //dataGridView.Columns[4].DefaultCellStyle.Font = new Font(dataGridView.DefaultCellStyle.Font, FontStyle.Italic);

            dataGridView.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.Dock = DockStyle.Fill;
        }
    }
}
