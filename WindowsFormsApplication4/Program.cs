using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
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
			Handler FirstLevelHandler = new Handler();
			Map FirstLevel = new Map(FirstLevelHandler);
			MapCalculations calc = new MapCalculations();
			FirstLevel.hexMap[6, 4].BackColor = System.Drawing.Color.Aqua;
			calc.calculateRoutes(FirstLevel.hexMap, FirstLevel.hexMap[6, 4]);
			Application.Run(FirstLevelHandler);
		}
	}
}
