using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{

    public partial class Handler : Form
    {
		private const double Formatting = 1.42;
        private const int _buttonWidth = 100;
		private const int _buttonHeight = (int)(_buttonWidth * 1.15);
		private const int _numberOfHexagonsInRow = 10;

		public Handler()
        {
            InitializeComponent();
        }

        public int ButtonWidth
        {
            get { return _buttonWidth; }
        }

        public int ButtonHeigt
        {
            get { return _buttonHeight; }
        }

        public int NumerOfHexagonsInRow
        {
            get { return _numberOfHexagonsInRow; }
        }

        public void DrawButton(HexagonButton button)
        {
            button.Size = new Size((int)(Formatting * _buttonHeight), (int)(Formatting * _buttonWidth));
            this.Controls.Add(button);
            button.TabStop = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
			button.BackColor = Color.LightGray;
            button.Paint += ButtonPainter;
        }

        public void PlaceHexagonButton(HexagonButton button)
        {
			button.Left = calculateButtonWidthOffset(button.XCoordinate);
			button.Top = calculateButtonHeightOffset(button.YCoordinate);

			/*
            for (int i = 0; i < _totalHexagons / _numberOfHexagonsInRow; i++)
            {
                int left = Buttons[count].Left;
                int top = Buttons[count].Top;
                if (i > 0)
                    top += i * (3 * (_buttonHeight / 4));
                if (i % 2 != 0)
                    left += (int)(_buttonWidth) / 2;
                for (int k = 0; k < _numberOfHexagonsInRow; k++)
                {
                    Buttons[count].Top = top;
                    Buttons[count].Left = left;
                    left += (int)(_buttonWidth);
                    ++count;
                }
            }
			*/
        }

        public void ButtonPainter(object sender, PaintEventArgs e)
        {
            System.Drawing.Drawing2D.GraphicsPath buttonPath =
            new System.Drawing.Drawing2D.GraphicsPath();

            Button hexagonButton = sender as Button;

            System.Drawing.Rectangle newRectangle = hexagonButton.ClientRectangle;

            e.Graphics.DrawPolygon(Pens.Black, Math.GetPoints(ButtonHeigt, ButtonWidth));

            // Create a hexagon within the new rectangle.
            buttonPath.AddPolygon(Math.GetPoints(ButtonHeigt, ButtonWidth));

            // Hexagon region.
            hexagonButton.Region = new System.Drawing.Region(buttonPath);
        }

        /////

        public void DrawWindow(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
			//this.ClientSize = new System.Drawing.Size(1000, 1000);
        }

		private int calculateButtonWidthOffset(int xCoordinate)
		{
			int width = 0;

			width += (xCoordinate * ButtonWidth);
			
			//Gives every second button an offset to make the grid
			if(xCoordinate % 2 == 1)
			{
				width += ButtonWidth / 2;
			}

			return width;
		}

		private int calculateButtonHeightOffset(int yCoordinate)
		{
			int height = 0;

			height += (yCoordinate * ButtonHeigt);

			return height;
		}
    }
}
