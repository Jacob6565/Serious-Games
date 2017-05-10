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
	public partial class Graph : Form
	{
		public Graph()
		{
			InitializeComponent();
		}

		private void Graph_Load(object sender, EventArgs e)
		{
			Chart chart1 = new Chart();
			

			Axis xAxis = new Axis
			{
				Interval = 1
			};

			Axis yAxis = new Axis
			{
				Minimum = 0,
				Maximum = 250,
				Title = "Some title"
			};

			ChartArea chartArea = new ChartArea
			{
				AxisX = xAxis,
				AxisY = yAxis
			};

			Title title = new Title
			{
				Name = "Some title",
				Text = "Some text",
				Visible = true
			};

			chart1.Titles.Add(title);
			chart1.ChartAreas.Add(chartArea);

			Series series = new Series
			{
				Name = "Some name",
				Color = System.Drawing.Color.Red,
				BorderWidth = 5,
				IsVisibleInLegend = true,
				IsXValueIndexed = true,
				ChartType = SeriesChartType.Line
			};

			for (int i = 0; i < 11; i++)
			{
				int yValue = i * i * 2;
				series.Points.AddXY(i, yValue);
			}

			chart1.Series.Add(series);

			this.Controls.Add(chart1);
		}
	}
}
