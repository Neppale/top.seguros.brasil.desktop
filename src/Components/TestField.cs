namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TestField : Form
    {
        public TestField()
        {
            this.Controls.Add(new ButtonTsbPrimary
            {
                Text = "Primario",
                Location = new Point(10, 10)
            });

            this.Controls.Add(new ButtonTsbSecondary
            {
                Text = "Secundário",
                Location = new Point(10, 80)
            });

            this.Controls.Add(new ButtonTsbTertiary
            {
                Text = "Terciário",
                Location = new Point(10, 160)
            });

        }

        private void TestField_Load(object sender, EventArgs e)
        {

        }

        private void tsbSearchBox1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
