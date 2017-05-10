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
            Form mainMenu = new MainMenu();
            //Map FirstLevel = new Map(mainMenu, 11, 11, path);
            Application.Run(mainMenu);

			Application.Run(new Graph());
		}
    }
}
