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
		GraphPanel firstGraph = new GraphPanel();

        public AdministratorForm()
        {
			InitializeComponent();
			InitializePanels();
			FormBorderStyle = FormBorderStyle.None;
			WindowState = FormWindowState.Maximized;
		}

        public void DrawWindow(object sender, EventArgs e)
        {
		}

        private void InitializePanels()
        {
            this.Controls.Add(administratorPanel);
            administratorPanel.Width = formatting.ScreenWidth;
            administratorPanel.Height = formatting.ScreenHeight;

			//firstGraph.XAxisInterval = 1;
			//firstGraph.YAxisMin = 0;
			//firstGraph.YAxisMax = 10;
			//firstGraph.YAxisTitle = "Title";
			//firstGraph.XAxisTitle = "Title";
			//firstGraph.GraphTitle = "YET ANOTHER TITLE";
			//firstGraph.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

			//List<Round> roundList = new List<Round>();
			//Random rand = new Random();

			//firstGraph.UpdateChartLook();

			//for (int i = 0; i < 10; i++)
			//{
			//	roundList.Add(new Round());
			//	roundList[i].ClicksPerMinute = rand.Next(0, 10);
			//	roundList[i].RoundID = i;
			//}

			//InitializeGraph();

			AddSearchSession(administratorPanel);

			CloseMenuButton(administratorPanel);
        }

		private void InitializeGraph(List<Round> roundList)
		{
			firstGraph.AddSeriesToGraph(roundList);

			firstGraph.Size = new Size(400, 400);
			firstGraph.Location = new Point(500, this.Bounds.Top + 60);

			administratorPanel.Controls.Add(firstGraph);
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

		private void AddSearchSession(Panel panel)
		{
			int screenMidPoint = panel.Width / 2;

			NumericUpDown sessionInput = new NumericUpDown();
			sessionInput.AutoSize = false;
			sessionInput.Size = new Size(150, 100);
			sessionInput.Location = new Point(screenMidPoint - (250 / 2), Bounds.Top + 20);
			panel.Controls.Add(sessionInput);

			Button btnCloseGame = new Button();
			btnCloseGame.Size = new Size(75, 30);
			btnCloseGame.TabStop = false;
			btnCloseGame.FlatStyle = FlatStyle.Flat;
			btnCloseGame.FlatAppearance.BorderSize = 0;
			btnCloseGame.BackColor = Color.Azure;
			btnCloseGame.Text = "Search";
			btnCloseGame.TextAlign = ContentAlignment.MiddleCenter;
			btnCloseGame.Location = new Point(screenMidPoint + 75 / 2, this.Bounds.Top + 15);
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
