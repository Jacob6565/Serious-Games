using P2SeriousGame;
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

namespace P2SeriosuGame
{
	public partial class Graph : Form
	{
		public Graph()
		{
			InitializeComponent();
		}

		private void Graph_Load(object sender, EventArgs e)
		{
			List<Round> dataset = new List<Round>();

			for (int i = 0; i < 10; i++)
			{
				Round round = new Round();
				round.RoundID = i;
				round.ClicksPerMinute = 10 + i;
				dataset.Add(round);
			}

			AddGraphData(chart1, "Series1", dataset);

			chart1.Series["Series1"].ChartType = SeriesChartType.FastLine;
			chart1.Series["Series1"].Color = Color.Red;
		}

		public void AddGraphData(Chart chart, string seriesName, List<Round> dataset)
		{
			foreach (var round in dataset)
			{
				chart.Series[seriesName].Points.AddXY(round.RoundID, round.ClicksPerMinute);
			}
		}

	}
}
