using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P2SeriosuGame.SQL;
using P2SeriosuGame;

namespace P2SeriousGame
{

    public partial class Handler : Form
    {

        Database SQL = new Database();


        public Handler()
        {
            InitializeComponent();
            SQL.watchRound.Start();
        }
               
        private int ButtonWidth;
        private int ButtonHeight;
        private int ButtonHeightOffset => (3 * (ButtonHeight / 4));

        int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
        int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

        //These constants declare the amount of reserved space or margins, where 0.05 equals 5%
        private const double _leftWidthReserved = 0.05;
        private const double _endWidthReserved = 0.12;
        private const double _topHeightReserved = 0.05;
        private const double _bottomHeightReserved = 0.03;

        //The gamescreen variables sets the height and width of the area on the screen where hexagonbutton can be drawn
        private double _gameScreenWidth = Screen.PrimaryScreen.Bounds.Width * (1 - (_leftWidthReserved + _endWidthReserved));
        private double _gameScreenHeight = Screen.PrimaryScreen.Bounds.Height * (1 - (_topHeightReserved + _bottomHeightReserved));

        //Centers the hexagonmap starting placement, if the hexagonmap doesnt fill out the entire gamescreen width
        private double WidthCentering => (_gameScreenWidth - (ButtonWidth * Map.TotalHexagonColumns)) / 2;

        //WidthStart and heightStart sets the starting place for the hexagonmap
        private int WidthStart => (int) ((_leftWidthReserved * Screen.PrimaryScreen.Bounds.Width) + WidthCentering);
        private int _heightStart = (int) (_topHeightReserved * Screen.PrimaryScreen.Bounds.Height);

        /// <summary>
        /// 
        /// </summary>
        public void CalculateButtonDimension()
        {
            CalculateButtonDimensionBasedOnScreenHeight();

            //Does the calculated width fit the screen width, if not then calculate height and width based on screen width
            if ((ButtonWidth * Map.TotalHexagonColumns) > _gameScreenWidth)
                CalculateButtonDimensionBasedOnScreenWidth();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CalculateButtonDimensionBasedOnScreenHeight()
        {
            double rowHeight;
            double hexagonRows = Map.TotalHexagonRows;
            const double evenRowsToHeight = 0.75;

            //The height to width ratio for a pointy top regulare hexagon
            double heightToWidth = System.Math.Sqrt(3)/2;

            //These series of if-else calculates the height of one button, determined by the number of rows and the screen height
            if (hexagonRows == 1)
                ButtonHeight = (int)(_gameScreenHeight / hexagonRows);
            else if (hexagonRows % 2 == 0)
            {
                rowHeight = (hexagonRows * evenRowsToHeight) + 0.25;
                ButtonHeight = (int)(_gameScreenHeight / rowHeight);
            }
            else if (hexagonRows % 2 == 1 && hexagonRows > 1)
            {
                rowHeight = ((hexagonRows - 1) / 4) + ((hexagonRows + 1) / 2);
                ButtonHeight = (int)(_gameScreenHeight / rowHeight);
            }

            //We calculate the width by multiplying height to width ratio
            ButtonWidth = (int)((ButtonHeight * heightToWidth));
        }

        /// <summary>
        /// 
        /// </summary>
        private void CalculateButtonDimensionBasedOnScreenWidth()
        {
            //The width to height ratio for a pointy top regulare hexagon
            double widthToHeight = System.Math.Sqrt(3) * ((double)2 / 3);

            double buttonWidthTemp;

            //We calculate the button width by dividing the screen width with number of columns + 0.5 (because we have an offset)
            buttonWidthTemp = (int)(_gameScreenWidth/ (Map.TotalHexagonColumns + 0.5));

            //We calculate the height by multiplying width to height ratio
            ButtonHeight = (int)(buttonWidthTemp * widthToHeight);

            //Now we do not need the buttonWidthTemp with precision, so we typecast the double to an int
            ButtonWidth = (int)buttonWidthTemp;
        }
        
        /// <summary>
        /// Initialises and draws a hexagon button, 
        /// and adds a click event calculates a new route when an HexButton is clicked.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="map"></param>
        public void DrawButton(HexagonButton button, Map map)
        {
            button.Size = new Size((int)(ConvertPointToPixel(ButtonHeight)), (int)(ConvertPointToPixel(ButtonWidth)));
            button.TabStop = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
			button.BackColor = Color.LightGray;
            button.Paint += ButtonPainter;
            button.MouseClick += button.HexClicked;
            button.MouseClick += HexClickedColor;
			button.MouseClick += map.MousePositioner;
            this.Controls.Add(button);
        }

        private float _hexClickedRound;

        public void HexClickedColor(object sender, MouseEventArgs e)
        {
            HexagonButton sender_Button = sender as HexagonButton;
            sender_Button.BackColor = Color.FromArgb(255, 105, 180);
            _hexClickedRound += 1;
        }

        /// <summary>
        /// Places HexagonButtons in GameWindow based on the coordinates assigned in the button.
        /// </summary>
        /// <param name="button"></param>
        public void PlaceHexagonButton(HexagonButton button)
        { 
            //For at farve midten før man har klikket på skærmen.
            if(button.XCoordinate == Map.TotalHexagonColumns/2 && button.YCoordinate == Map.TotalHexagonRows/2)
            {
                button.BackColor = System.Drawing.Color.Aqua;
                button.Enabled = false;
            }
                                             
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
            e.Graphics.DrawPolygon(Pens.Black, Math.GetPoints(ButtonHeight, ButtonWidth));

            // Create a hexagon within the new rectangle.
            buttonPath.AddPolygon(Math.GetPoints(ButtonHeight, ButtonWidth));
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
            ResetButton.MouseClick += RoundDataCollector;
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
			int width = WidthStart;
			width += (xCoordinate * ButtonWidth);			
			//Gives every second button an offset to make the grid
			if(yCoordinate % 2 == 1)
			{
				width += ButtonWidth / 2;
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
			int height = _heightStart;

			height += (yCoordinate * ButtonHeightOffset);

			return height;
		}

        public void ExitButtonClick(object sender, MouseEventArgs e)
        {
            SendToDatabase();
            Close();
        }

        private void ResetButtonClick(object sender, MouseEventArgs e)
        {
            foreach (HexagonButton hex in Map.hexMap)
            {
                hex.Visited = false;
                hex.Passable = true;
                hex.Enabled = true;
                hex.BackColor = System.Drawing.Color.LightGray;
                PlaceHexagonButton(hex);
            }
            Map.ResetMouse();
        }

        //We assume that there is 72 points per inch and 96 pixels per inch
        private double ConvertPointToPixel(double point)
        {
            return point * 96 / 72;
        }

        /*
        private float _secondsTotal;
        private float _clickedTotal;
        private int _roundWin;
        private int _roundLoss;

        public void SendToDatabase()
        {
            _watchRound.Stop(); // Stops the time for the round
            var elapsedSec = _watchRound.ElapsedMilliseconds / 1000; // Converts the time to seconds
            float secondsRound = unchecked(elapsedSec);

            _secondsTotal += secondsRound;
            _clickedTotal += _hexClickedRound;

            if (Pathfinding.gameRoundWin)
            {
                _roundWin = 1;
                _roundLoss = 0;
            }
            else if (!Pathfinding.gameRoundWin)
            {
                _roundWin = 0;
                _roundLoss = 1;
            }

            using (var context = new Entities())
            {
                context.TestParameters.Add(new TestParameters // adds a row to the TestParameters table in the SQL database
                {
                    Clicks = _clickedTotal, 
                    AVG_Clicks = AverageClick(_clickedTotal, _secondsTotal),
                    Rounds = _resetCounter + 1,
                    Wins = Pathfinding.gameTotalWins,
                    Losses = _totalLoss,
                    Time_Used = _secondsTotal
                });

                context.Rounds.Add(new Rounds // adds a row to the Rounds table in the SQL database
                {
                    Clicks = _hexClickedRound,
                    AVG_Clicks = AverageClick(_hexClickedRound, secondsRound),
                    Win = _roundWin,
                    Loss = _roundLoss,
                    Time_Used = secondsRound
                });

                context.SaveChanges();
            }
        }

        private long elapsedSec;
        private float _secondsRound;
        private static int _totalLoss;

        private void RoundDataCollector(object sender, MouseEventArgs e)
        {
            _watchRound.Stop(); // Stops the time for the round
            elapsedSec = _watchRound.ElapsedMilliseconds / 1000; // Converts the time to seconds
            _secondsRound = unchecked(elapsedSec);

            _secondsTotal += _secondsRound;
            _clickedTotal += _hexClickedRound;

            _totalLoss += 1; 
        
            // Testing parameters
            string testFirstName = "Foo";
            string testLastName = "Bar";

            ResetCounter();

            using (var context = new Entities())
            {
                context.Rounds.Add(new Rounds // adds a row to the Rounds table in the SQL database
                {
                    Clicks = _hexClickedRound,
                    AVG_Clicks = AverageClick(_hexClickedRound, _secondsRound),
                    Win = _roundWin,
                    Loss = _roundLoss,
                    Time_Used = _secondsRound
                });

                context.Person.Add(new Person // adds a row to the Person table in the SQL database
                {
                    First_Name = testFirstName,
                    Last_Name = testLastName
                });

                context.SaveChanges();
            }
        }

        private int _resetCounter;

        private void ResetCounter()
        {
            _resetCounter += 1;
        }

        private float AverageClick(float hexClicked, float seconds)
        {
            return hexClicked / seconds;
        }*/
    }
}
