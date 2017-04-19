using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public class HexagonButton : Button
    {
        public HexagonButton(int xCoordinate, int yCoordinate, bool isEdgeTile)
        {
            _xCoordinate = xCoordinate;
            _yCoordinate = yCoordinate;
			_isEdgeTile = isEdgeTile;
        }

		private bool _visited = false;

		public bool Visited
		{
			get { return _visited; }
			set { _visited = value; }
		}


		private bool _passable = true;
        public bool Passable
        {
            get { return _passable; }
            set { _passable = value; }
        }

        private int _xCoordinate;
        public int XCoordinate
        {
            get { return _xCoordinate; }
        }

        private int _yCoordinate;
        public int YCoordinate
        {
            get { return _yCoordinate; }
        }

		private bool _isEdgeTile;

		public bool IsEdgeTile
		{
			get { return _isEdgeTile; }
		}


		private bool _showImage = false;
        public bool ShowImage
        {
            get
            {
                return _showImage;
            }
            set
            {
                _showImage = value;
            }
        }

        public void HexClicked(object sender, MouseEventArgs e)
        {
			Console.WriteLine($"You pressed on tile: ({XCoordinate}, {YCoordinate}) {IsEdgeTile}");
            HexagonButton sender_Button = sender as HexagonButton;
            sender_Button.BackColor = Color.FromArgb(255, 105, 180);
            sender_Button.Enabled = false;
            sender_Button.Passable = false;
            PrintNeighbours();
        }

		public HexagonButton parent;
		public List<HexagonButton> neighbourList = new List<HexagonButton>();
		public int CostToStart;

        private void PrintNeighbours()
        {
            foreach (HexagonButton hex in neighbourList)
            {
                Console.WriteLine($"{ hex.XCoordinate}, { hex.YCoordinate} { hex.IsEdgeTile}");
            }
        }

    }
}