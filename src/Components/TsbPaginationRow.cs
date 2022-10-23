using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbPaginationRow : UserControl
    {

        public string PageNumberText
        {
            get
            {
                return this.pageNumber.Text;
            }
            set
            {
                this.pageNumber.Text = value;
            }
            
        }

        public bool NextEnabled
        {
            set
            {
                nextButton.Enabled = value;
                if (!value)
                {
                    nextButton.Image = Properties.Resources.disable_right_arrow;
                }
                if (value)
                {
                    nextButton.Image = Properties.Resources.enabled_right_arrow;
                }
            }
        }

        public bool PreviousEnabled
        {
            set
            {
                previousButton.Enabled = value;
                
                if (!value)
                {
                    previousButton.Image = Properties.Resources.disable_left_arrow;
                }
                if (value)
                {
                    previousButton.Image = Properties.Resources.enabled_left_arrow;
                }
            }
        }

        public event EventHandler ClickNext
        {
            add
            {
                this.nextButton.Click += value;
            }
            remove
            {
                this.nextButton.Click -= value;
            }
        }

        public event EventHandler ClickPrevious
        {
            add
            {
                this.previousButton.Click += value;
            }
            remove
            {
                this.previousButton.Click -= value;
            }
        }

        public TsbPaginationRow()
        {
            this.Margin = new Padding(0, 0, 0, 0);

            InitializeComponent();
        }

        private void TsbPaginationRow_Load(object sender, EventArgs e)
        {
            nextButton.Cursor = Cursors.Hand;
            previousButton.Cursor = Cursors.Hand;
        }
    }
}
