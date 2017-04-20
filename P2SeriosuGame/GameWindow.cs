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

        /// <summary>
        /// Initialises and draws a hexagon button, 
        /// and adds a click event calculates a new route when an HexButton is clicked.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="map"></param>
        public void DrawButton(HexagonButton button, Map map)
        {
            button.Size = new Size((int)(Formatting * _buttonHeight), (int)(Formatting * _buttonWidth));
            button.TabStop = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
			button.BackColor = Color.LightGray;
            button.Paint += ButtonPainter;
            button.MouseClick += button.HexClicked;
			button.MouseClick += map.HexClicked;
            this.Controls.Add(button);
        }

        /// <summary>
        /// Places HexagonButtons in GameWindow based on the coordinates assigned in the button.
        /// </summary>
        /// <param name="button"></param>
        public void PlaceHexagonButton(HexagonButton button)
        {
			button.Left = CalculateButtonWidthOffset(button.XCoordinate, button.YCoordinate);
			button.Top = CalculateButtonHeightOffset(button.YCoordinate);
        }

        /// <summary>
        /// Calculates the points in a hexagon and makes it a button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void AddExitButton()
		{
			Button ExitButton = new Button();
			ExitButton.Size = new Size(100, 25);
			ExitButton.TabStop = false;
			ExitButton.FlatStyle = FlatStyle.Flat;
			ExitButton.FlatAppearance.BorderSize = 0;
			ExitButton.BackColor = Color.LightGray;
			ExitButton.Location = new Point(this.Bounds.Right - ExitButton.Width - 20, this.Bounds.Top + 20);
			ExitButton.MouseClick += ExitButtonClick;
			ExitButton.Text = "Close application";
			ExitButton.TextAlign = ContentAlignment.MiddleCenter;
			this.Controls.Add(ExitButton);
		}

        private void AddResetButton()
        {
            Button ResetButton = new Button();
            ResetButton.Size = new Size(100, 25);
            ResetButton.TabStop = false;
            ResetButton.FlatStyle = FlatStyle.Flat;
            ResetButton.FlatAppearance.BorderSize = 0;
            ResetButton.BackColor = Color.Red;
            ResetButton.Location = new Point(this.Bounds.Right - ResetButton.Width - 20, this.Bounds.Top + 60);
            ResetButton.MouseClick += ResetButtonClick;
            ResetButton.Text = "Reset Game";
            ResetButton.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(ResetButton);
        }

        

        public void DrawWindow(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
			AddExitButton();
            AddResetButton();

        }


        /// <summary>
        /// Converts a coordinate into a position in a hexgrid.
        /// </summary>
        /// <param name="xCoordinate"></param>
        /// <param name="yCoordinate"></param>
        /// <returns></returns>
		private int CalculateButtonWidthOffset(int xCoordinate, int yCoordinate)
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

        /// <summary>
        /// Converts a coordinate into a position in a hexgrid.
        /// </summary>
        /// <param name="yCoordinate"></param>
        /// <returns></returns>
		private int CalculateButtonHeightOffset(int yCoordinate)
		{
			int height = 0;

			height += (yCoordinate * _buttonHeightOffset);

			return height;
		}
        
		public void ExitButtonClick(object sender, MouseEventArgs e)
		{
			Close();
		}


        private void ResetButtonClick(object sender, MouseEventArgs e)
        {
            Application.Restart();                   
        }
    }
}
