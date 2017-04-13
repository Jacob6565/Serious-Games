using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    class HexagonButton : Button
    {
        private bool _passable = true;
        public bool Passable
        {
            get { return _passable; }
            set { _passable = value; }
        }
    }
}
