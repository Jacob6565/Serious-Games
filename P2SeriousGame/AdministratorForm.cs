using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2SeriousGame
{
    public partial class AdministratorForm : Form
    {
        Formatting formatting = new Formatting();
        Panel administratorPanel = new Panel();

        public AdministratorForm()
        {
            InitializeComponent();
            InitializePanels();
        }

        public void DrawWindow(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void InitializePanels()
        {
            this.Controls.Add(administratorPanel);
            administratorPanel.Width = formatting.ScreenWidth;
            administratorPanel.Height = formatting.ScreenHeight;

			GraphPanel firstGraph = new GraphPanel
			{
				XAxisInterval = 1,
				YAxisMin = 0,
				YAxisMax = 10,
				YAxisTitle = "Title",
				XAxisTitle = "Title",
				GraphTitle = "YET ANOTHER TITLE",
				ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
			};

			List<Round> roundList = new List<Round>();
			Random rand = new Random();

			firstGraph.UpdateChartLook();

			for (int i = 0; i < 10; i++)
			{
				roundList.Add(new Round());
				roundList[i].ClicksPerMinute = rand.Next(0, 10);
				roundList[i].RoundID = i;
			}

			firstGraph.AddSeriesToGraph(roundList);

			CloseMenuButton(administratorPanel);
        }

        private void CloseMenuButton(Panel panel)
        {
            Button btnCloseGame = new Button();
            btnCloseGame.Size = new Size(300, 100);
            btnCloseGame.TabStop = false;
            btnCloseGame.FlatStyle = FlatStyle.Flat;
            btnCloseGame.FlatAppearance.BorderSize = 0;
            btnCloseGame.BackColor = Color.Azure;
            btnCloseGame.Text = "Return to menu";
            btnCloseGame.TextAlign = ContentAlignment.MiddleCenter;
            btnCloseGame.Location = new Point(this.Bounds.Right / 2 - btnCloseGame.Width / 2, this.Bounds.Top + 60);
            btnCloseGame.MouseClick += ReturnToMainMenu;
            panel.Controls.Add(btnCloseGame);
        }


        private void ReturnToMainMenu(object sender, MouseEventArgs e)
        {
            Close();
        }

		private void AdministratorForm_Load(object sender, EventArgs e)
		{

		}
	}
}
