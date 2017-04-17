using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public class HexagonButton : Button
    {
		public HexagonButton(int xCoordinate, int yCoordinate)
		{
			this.xCoordinate = xCoordinate;
			this.yCoordinate = yCoordinate;
		}

        private bool _passable = true;
        public bool Passable
        {
            get { return _passable; }
            set { _passable = value; }
        }

		private int xCoordinate;
		public int XCoordinate
		{
			get { return xCoordinate; }
		}

		private int yCoordinate;
		public int YCoordinate
		{
			get { return yCoordinate; }
		}

	}
}
