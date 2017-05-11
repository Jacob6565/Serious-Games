using P2SeriousGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2SeriousGame
{
    static class Program
    {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GameWindow FirstLevelHandler = new GameWindow();
			//IPathfinding path = new Pathfinding();           
			//Form mainMenu = new MainMenu();
			//Map FirstLevel = new Map(mainMenu, 11, 11, path);
			//Application.Run(mainMenu);

			Graph graph = new Graph
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

			graph.UpdateChartLook();

			for (int i = 0; i < 10; i++)
			{
				roundList.Add(new Round());
				roundList[i].ClicksPerMinute = rand.Next(0, 10);
				roundList[i].RoundID = i;
			}

			graph.AddSeriesToGraph(roundList);

			Application.Run(graph);
		}
    }
}
