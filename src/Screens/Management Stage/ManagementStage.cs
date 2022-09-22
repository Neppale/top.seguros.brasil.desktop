using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Top_Seguros_Brasil_Desktop.src.Screens;
using Top_Seguros_Brasil_Desktop.src.Screens.BaseForm;
using Top_Seguros_Brasil_Desktop.src.Models;
using System.Text.Json;
using System.Runtime.CompilerServices;
using System.Reflection;
using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.Screens.Components;
using Top_Seguros_Brasil_Desktop.src.Panels;

namespace Top_Seguros_Brasil_Desktop.src.Screens.Management_Stage
{
    public partial class ManagementStage : BaseForm.BaseForm
    {

        public ManagementStage()
        {
            InitializeComponent();
        } 

        public ManagementStage(string name, string type, string token)
        {
            this.userName = name;
            this.userType = type;
            this.token = token;

            InitializeComponent();
        }

        private async void ManagementStage_Load(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            this.Controls.Add(new SideNav());
            this.Controls.Add(new appBar(userName, userType));
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
