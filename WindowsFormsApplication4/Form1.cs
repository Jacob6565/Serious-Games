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
		private const int _buttonHeightOffset = (3 * (_buttonHeight / 4));

		public Handler()
        {
            InitializeComponent();
        }

        public void DrawButton(HexagonButton button)
        {
            button.Size = new Size((int)(Formatting * _buttonHeight), (int)(Formatting * _buttonWidth));
            button.TabStop = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
			button.BackColor = Color.LightGray;
            button.Paint += ButtonPainter;
			this.Controls.Add(button);
        }

        public void PlaceHexagonButton(HexagonButton button)
        {
			button.Left = calculateButtonWidthOffset(button.XCoordinate, button.YCoordinate);
			button.Top = calculateButtonHeightOffset(button.YCoordinate);
        }

        public void ButtonPainter(object sender, PaintEventArgs e)
        {
            System.Drawing.Drawing2D.GraphicsPath buttonPath =
            new System.Drawing.Drawing2D.GraphicsPath();

            Button hexagonButton = sender as Button;

            System.Drawing.Rectangle newRectangle = hexagonButton.ClientRectangle;

            e.Graphics.DrawPolygon(Pens.Black, Math.GetPoints(_buttonHeight, _buttonWidth));

            // Create a hexagon within the new rectangle.
            buttonPath.AddPolygon(Math.GetPoints(_buttonHeight, _buttonWidth));

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

		private int calculateButtonWidthOffset(int xCoordinate, int yCoordinate)
		{
			int width = 0;

			width += (xCoordinate * _buttonWidth);
			
			//Gives every second button an offset to make the grid
			if(yCoordinate % 2 == 1)
			{
				width += _buttonWidth / 2;
			}

			return width;
		}

		private int calculateButtonHeightOffset(int yCoordinate)
		{
			int height = 0;

			height += (yCoordinate * _buttonHeightOffset);

			return height;
		}
    }
}
