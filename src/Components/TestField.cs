namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TestField : Form
    {
        public TestField()
        {

            this.Controls.Add(new TsbInput
            {
                Location = new Point(0, 50),
                HintText = "Email do usuário aqui",
                LabelText = "Email",
                ForeColor = TsbColor.neutralGray,
                Dock = DockStyle.Top
            });


            this.Controls.Add(new TsbInput
            {
                Location = new Point(0, 50),
                HintText = "Nome completo do usuário aqui",
                LabelText = "Nome completo",
                Dock = DockStyle.Top
            });

            this.Controls.Add(new TsbComboBox
            {
                Location = new Point(0, 50),
                HintText = "Itens",
                LabelText = "Teste combobox",
                
                Dock = DockStyle.Top
            });
            InitializeComponent();
        }

        private void TestField_Load(object sender, EventArgs e)
        {

        }
    }
}
