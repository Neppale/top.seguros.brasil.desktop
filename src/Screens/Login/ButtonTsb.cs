using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.Components;

namespace Top_Seguros_Brasil_Desktop
{
    public partial class ButtonTsb : Button
    {
        Button button;
        
        public ButtonTsb()
        {
            InitializeComponent();
            button = new Button();
            this.Controls.Add(button);  
            
        }

        public void changeButtonText(string text)
        {
            this.Text = text.ToUpper();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            this.BackColor = Color.FromArgb(244, 84, 70);
            this.FlatStyle = FlatStyle.Flat;
            this.ForeColor = Color.FromArgb(255, 255, 255);
            this.Size = new Size(331, 56);
            this.Location = new Point(555, 760);
            base.OnVisibleChanged(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if(Enabled == false)
            {
                this.ForeColor = TsbColor.primaryDarkest;
            }
            base.OnEnabledChanged(e);
        }

        public ButtonTsb(IContainer container)
        {

            this.Text = "funcionou?";
            container.Add(this);

            InitializeComponent();
        }
    }
}
