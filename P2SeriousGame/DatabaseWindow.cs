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
    public partial class DatabaseWindow : Form
    {
        public DatabaseWindow()
        {
            InitializeComponent();
        }

        private static DatabaseWindow _instance;
        public static DatabaseWindow GetInstance()
        {
            if (_instance == null) _instance = new DatabaseWindow();
            {
                return _instance;
            }
        }
    }
}
