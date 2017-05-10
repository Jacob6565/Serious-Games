using System;
using System.Windows.Forms;

namespace P2SeriousGame
{
    public class Formatting
    {
        #region constant vars

        public int ButtonWidth;
        public int ButtonHeight;
        public int ButtonHeightOffset => (3 * (ButtonHeight / 4));

        public int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

        //These constants declare the amount of reserved space or margins, where 0.05 equals 5%
        public const double _leftWidthReserved = 0.05;
        public const double _endWidthReserved = 0.12;
        public const double _topHeightReserved = 0.05;
        public const double _bottomHeightReserved = 0.03;

        //The gamescreen variables sets the height and width of the area on the screen where hexagonbutton can be drawn
        public double _gameScreenWidth = Screen.PrimaryScreen.Bounds.Width * (1 - (_leftWidthReserved + _endWidthReserved));
        public double _gameScreenHeight = Screen.PrimaryScreen.Bounds.Height * (1 - (_topHeightReserved + _bottomHeightReserved));

        //Centers the hexagonmap starting placement, if the hexagonmap doesnt fill out the entire gamescreen width
        public double WidthCentering => (_gameScreenWidth - (ButtonWidth * Map.TotalHexagonColumns)) / 2;

        //WidthStart and heightStart sets the starting place for the hexagonmap
        public int WidthStart => (int)((_leftWidthReserved * Screen.PrimaryScreen.Bounds.Width) + WidthCentering);


        public int _heightStart = (int)(_topHeightReserved * Screen.PrimaryScreen.Bounds.Height);
        #endregion
    }
}