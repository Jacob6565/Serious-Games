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
            Application.Run(FirstLevelHandler);

			Map FirstLevel = new Map();
			FirstLevel.CreateMap(FirstLevelHandler);
		}
    }
}
