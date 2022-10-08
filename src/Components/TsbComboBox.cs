using MaterialSkin;
using MaterialSkin.Controls;
using System.ComponentModel;
using Top_Seguros_Brasil_Desktop.src.font;
using static System.Windows.Forms.ComboBox;

namespace Top_Seguros_Brasil_Desktop.src.Components
{
    public partial class TsbComboBox : Panel
    {

        TsbFont tsbFont = new TsbFont();

        Label Label = new Label
        {
            Font = new Font(TsbFont.TsbFonts.Families[0], 12, FontStyle.Bold),
            ForeColor = TsbColor.neutral,
            AutoSize = true,
            Padding = new Padding(0, 0, 0, 16),
            Dock = DockStyle.Top
        };

        public ComboBox input = new ComboBox
        {
            ForeColor = TsbColor.neutral,
            Font = MaterialSkinManager.Instance.ROBOTO_MEDIUM_11,
            BackColor = TsbColor.background,
            FlatStyle = FlatStyle.Flat,
            Dock = DockStyle.Top,
            Margin = new Padding(4, 32, 0, 0),
            Width = 331,
            AutoSize = true
        };

        

        Panel InputPanel = new Panel
        {
            AutoSize = true
        };

        public new bool Enabled
        {

            get
            {
                return input.Enabled;
            }
            set
            {
                input.Enabled = value;
                Label.Enabled = value;
            }

        }

        public event EventHandler LoadValues
        {
            add
            {
                input.Click += value;
            }
            remove
            {
                input.Click -= value;                
            }
        }

        public object DataSource
        {
            get
            {
                return input.DataSource;
            }
            set
            {
                input.DataSource = value;
            }
        }
        
        public string LabelText
        {
            get
            {
                return Label.Text;
            }
            set
            {
                Label.Text = value;
            }
        }

        public string HintText
        {
            get
            {
                return input.Text;
            }
            set
            {
                input.Text = value;
            }
        }

        public ObjectCollection Items
        {
            get
            {
                return input.Items;
            }
            set
            {
                input.Items.Add(value);
            }
        }

        public object SelectedItem {
            get
            {
                return input.SelectedItem;
            }
            
            set
            {
                input.SelectedItem = value;
            }
        }

        public event EventHandler SelectedValueChanged
        {
            add
            {
                input.SelectedValueChanged += value;
            }
            remove
            {
                input.SelectedValueChanged -= value;
            }
        }
        
        public object SelectedComboItem
        {
            get
            {
                return input.SelectedItem;
            }
            set
            {
                input.SelectedItem = value;
                
            }
        }

        public TsbComboBox() 
        {
            input.FlatStyle = FlatStyle.Flat;

            ForeColor = TsbColor.neutralGray;
            InputPanel.Height = 100;
            InputPanel.AutoSize = true;
            InputPanel.Dock = DockStyle.Bottom;
            

            InputPanel.Controls.Add(input);
            InputPanel.Controls.Add(Label);
            Controls.Add(InputPanel);

            InitializeComponent();
        }

        public TsbComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
