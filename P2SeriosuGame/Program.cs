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
			GameWindow Game = new GameWindow();
			Map FirstLevel = new Map(Game, 9, 9);
			Application.Run(Game);
		}
	}
}
