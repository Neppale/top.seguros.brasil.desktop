using Top_Seguros_Brasil_Desktop.src.Components;
using Top_Seguros_Brasil_Desktop.src.Panels;
using Top_Seguros_Brasil_Desktop.src.Screens.Components;

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

        private void ManagementStage_Load(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            this.Controls.Add(new appBar(userName, userType));
            this.Controls.Add(new SideNav());
            
            
            
        }
        
    }
}
