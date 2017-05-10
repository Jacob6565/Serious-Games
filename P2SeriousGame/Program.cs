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
            IPathfinding path = new Pathfinding();
            Map FirstLevel = new Map(FirstLevelHandler, 11, 11, path);
            Form mainMenu = new MainMenu();
            Application.Run(FirstLevelHandler);

            //Graph FirstLevelHandler = new Graph();
            //Application.Run(FirstLevelHandler);
        }
    }
}
