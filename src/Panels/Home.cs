using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top_Seguros_Brasil_Desktop.src.Components;

namespace Top_Seguros_Brasil_Desktop.src.Panels
{
    public partial class Home : BasePanel
    {
        public Home()
        {
        }

        public Home(string pageTitle, string subtitle, int id)
        {



            GetData(id);


            SubTitle(subtitle);
            Title(pageTitle);
            InitializeComponent();
        }

        
        private async void GetData(int id)
        {
            EngineInterpreter interpreter = new EngineInterpreter(BasePanel.token);

            var response = await interpreter.Request<Dashboard>($"https://tsb-api-policy-engine.herokuapp.com/usuario/relatorio/{id}", "GET", null);

            Dashboard responseBody = response.Body;

            if (responseBody != null)
            {

                TableLayoutPanel dashboardInfoPanel = new TableLayoutPanel
                {
                    Dock = DockStyle.Top,
                    Height = 72,
                };

                SetColumnSpan(dashboardInfoPanel, 2);
                dashboardInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                dashboardInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                dashboardInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                dashboardInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                dashboardInfoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

                Controls.Add(dashboardInfoPanel, 0, 3);
                SetRowSpan(dashboardInfoPanel, 2);

                TsbDataBox totalGains = new TsbDataBox
                {
                    Parent = this,
                    titleText = "Ganhos totais",
                    FontSize = 12,
                    subtitleText = "R$ " + responseBody.estimatedTotalGains.ToString(),
                    Margin = new Padding(24),
                    Dock = DockStyle.Top
                };
                dashboardInfoPanel.Controls.Add(totalGains, 0, 0);

                TsbDataBox monthlyExpenses = new TsbDataBox
                {
                    Parent = this,
                    titleText = "Gastos mensais",
                    FontSize = 12,
                    subtitleText = "R$ " + responseBody.estimatedMonthlyExpenses.ToString(),
                    Margin = new Padding(24),
                    Dock = DockStyle.Top
                };
                dashboardInfoPanel.Controls.Add(monthlyExpenses, 1, 0);

                TsbDataBox monthlyGains = new TsbDataBox
                {
                    Parent = this,
                    titleText = "Ganhos mensais",
                    FontSize = 12,
                    subtitleText = "R$ " + responseBody.estimatedMonthlyGains.ToString(),
                    Margin = new Padding(24),
                    Dock = DockStyle.Top
                };
                dashboardInfoPanel.Controls.Add(monthlyGains, 2, 0);

                TsbDataBox policyCount = new TsbDataBox
                {
                    Parent = this,
                    titleText = "Número de apólices",
                    FontSize = 12,
                    subtitleText = responseBody.policyCount.ToString(),
                    Margin = new Padding(24),
                    Dock = DockStyle.Top
                };
                dashboardInfoPanel.Controls.Add(policyCount, 3, 0);

                TsbDataBox customerCount = new TsbDataBox
                {
                    Parent = this,
                    titleText = "Seus clientes",
                    FontSize = 12,
                    subtitleText = responseBody.clientCount.ToString(),
                    Margin = new Padding(24),
                    Dock = DockStyle.Top
                };
                dashboardInfoPanel.Controls.Add(customerCount, 4, 0);
            };


        }

        public Home(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }

    public class Dashboard
    {
        public double estimatedTotalGains { get; set; }
        public double estimatedMonthlyExpenses { get; set; }
        public double estimatedMonthlyGains { get; set; }
        public int policyCount { get; set; }
        public int clientCount { get; set; }
    }
}
