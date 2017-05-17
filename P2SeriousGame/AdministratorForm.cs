using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace P2SeriousGame
{
	public partial class AdministratorForm : Form, IStatistic
	{
		Formatting formatting = new Formatting(new Control());
		Panel administratorPanel = new Panel();
		GraphPanel[] graphList = new GraphPanel[4];

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

			AddSearchSession();

			CloseMenuButton();
		}

		private double InitializeGraph(List<float> valueList, int graphNumber)
		{
			GraphPanel graph = graphList[graphNumber];
			graph.AddSeriesToGraph(valueList);

			graph.Size = new Size(300, 400);
			graph.Location = new Point((administratorPanel.Right / 4 - graph.Width / 2) * graphNumber, this.Bounds.Top + 180);

			administratorPanel.Controls.Add(graph);

			return (valueList.Max() * 1.05);
		}


		private void CloseMenuButton()
		{
			Button btnCloseGame = new Button();
			btnCloseGame.Size = new Size(300, 100);
			btnCloseGame.TabStop = false;
			btnCloseGame.FlatStyle = FlatStyle.Flat;
			btnCloseGame.FlatAppearance.BorderSize = 0;
			btnCloseGame.BackColor = Color.Azure;
			btnCloseGame.Text = "Return to menu";
			btnCloseGame.TextAlign = ContentAlignment.MiddleCenter;
			btnCloseGame.Location = new Point(administratorPanel.Right / 2 - btnCloseGame.Width / 2, administratorPanel.Top + 60);
			btnCloseGame.MouseClick += ReturnToMainMenu;
			administratorPanel.Controls.Add(btnCloseGame);
		}

		private void AddSearchSession()
		{
			int screenMidPoint = administratorPanel.Width / 2;

			NumericUpDown sessionInput = new NumericUpDown();
			sessionInput.AutoSize = false;
			sessionInput.Size = new Size(150, 100);
			sessionInput.Location = new Point(screenMidPoint - (250 / 2), Bounds.Top + 20);
			administratorPanel.Controls.Add(sessionInput);

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
			administratorPanel.Controls.Add(btnCloseGame);
		}


		private void ReturnToMainMenu(object sender, MouseEventArgs e)
		{
			Close();
		}

		private void LoadGraphs(object sender, MouseEventArgs e)
		{

		}

		private void AdministratorForm_Load(object sender, EventArgs e)
		{

		}

		public void drawGraph(List<float> valueList, string xAxisTitle, string yAxisTitle, string graphTitle, int xAxisInterval, int yAxisMin, int yAxisMax, SeriesChartType chartType)
		{
			graphList[graphList.Length] = new GraphPanel
			{
				XAxisTitle = xAxisTitle,
				YAxisTitle = yAxisTitle,
				GraphTitle = graphTitle,
				XAxisInterval = xAxisInterval,
				YAxisMin = yAxisMin,
				YAxisMax = yAxisMax,
				ChartType = chartType,
			};
			GraphPanel newGraph = graphList[graphList.Length];

			newGraph.UpdateChartLook();
			InitializeGraph(valueList, graphList.Length);
		}

		public void drawGraph(List<float> valueList, string xAxisTitle, string yAxisTitle, string graphTitle, int xAxisInterval, int yAxisMin, SeriesChartType chartType)
		{
			double yMaxDouble = valueList.Max() * 1.05;
			int yMax = Int32.Parse(yMaxDouble.ToString());
			drawGraph(valueList, xAxisTitle, yAxisTitle, graphTitle, xAxisInterval, yAxisMin, yMax, chartType);
		}
	}
}
