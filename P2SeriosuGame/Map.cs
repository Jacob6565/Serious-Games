using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2SeriousGame
{
    /// <summary>
    /// Class to contain a grid of HexagonButtons.
    /// </summary>
	public class Map
	{
        private static int _totalHexagonRows = 0;
        public static int TotalHexagonRows
        {
            get { return _totalHexagonRows; }
        }

        private static int _totalHexagonColoumns = 0;
        public static int TotalHexagonColumns
        {
            get { return _totalHexagonColoumns; }
        }


        public HexagonButton[,] hexMap;
		MapCalculations calc = new MapCalculations();
		private HexagonButton currentMousePosition;
        private int xValue = 6;
        private int yValue = 4;

        /// <summary>
        /// Creates a HexagonButton grid in xSize * ySize, needs a reference to the handler window.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="xSize"></param>
        /// <param name="ySize"></param>
        public Map(Handler handler, int xSize, int ySize)
        {
            _totalHexagonRows = ySize;
            _totalHexagonColoumns = xSize;
            hexMap = new HexagonButton[_totalHexagonColoumns, _totalHexagonRows];
            CreateMap(handler);     
            FindNeighbours();
        }
        
        /// <summary>
        /// Initialises the HexagonButton grid. Flags edge buttons.
        /// </summary>
        /// <param name="handler"></param>
        public void CreateMap(Handler handler)
        {
            handler.CalculateButtonDimension();
            for (int i = 0; i < _totalHexagonColoumns; i++)
            {
                for (int j = 0; j < _totalHexagonRows; j++)
                {
                    bool isEdge = false;
                    if (i == 0 || i == _totalHexagonColoumns - 1 || j == 0 || j == _totalHexagonRows - 1)
                    {
                        isEdge = true;
                    }
                    hexMap[i, j] = new HexagonButton(i, j, isEdge);
                    handler.DrawButton(hexMap[i, j], this);
                    handler.PlaceHexagonButton(hexMap[i, j]);
                }
            }
        }

        /// <summary>
        /// Calculates new route when HexagonButton is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HexClicked(object sender, MouseEventArgs e)
        {
            //Når der bliver klikket bliver tidliger punkt farvet gråt, så bliver der beregnet ny vej og koordinaterne til næste knap bliver assignet til xValue og yValue og knappen med disse koordinater farves Aqua.
            //næste to linjer er det som skal ske for den knap musen stop på i det tidligere trin.
            hexMap[xValue, yValue].BackColor = System.Drawing.Color.LightGray;
            hexMap[xValue, yValue].Enabled = true;

            //Nye position.
            calc.CalculateRoutes(hexMap, hexMap[xValue, yValue]);
            xValue = calc.FirstButtonInPath.XCoordinate;
            yValue = calc.FirstButtonInPath.YCoordinate;
            hexMap[xValue, yValue].BackColor = System.Drawing.Color.Aqua;
            hexMap[xValue, yValue].Enabled = false;       
        }

        /// <summary>
        /// Finds the neighbours for each HexagonButton in Map.cs (except of the edge buttons).
        /// </summary>
		public void FindNeighbours()
        {
            for (int i = 0; i < _totalHexagonColoumns; i++)
            {
                for (int j = 0; j < _totalHexagonRows; j++)
                {
                    if (!hexMap[i, j].IsEdgeTile)
                    {
                        hexMap[i, j].neighbourList.Add(hexMap[i - 1, j]);
                        hexMap[i, j].neighbourList.Add(hexMap[i + 1, j]);
                        if (j % 2 == 1)
                        {
                            hexMap[i, j].neighbourList.Add(hexMap[i, j - 1]);
                            hexMap[i, j].neighbourList.Add(hexMap[i + 1, j - 1]);
                            hexMap[i, j].neighbourList.Add(hexMap[i, j + 1]);
                            hexMap[i, j].neighbourList.Add(hexMap[i + 1, j + 1]);
                        }
                        if(j % 2 == 0)
                        {
                            hexMap[i, j].neighbourList.Add(hexMap[i, j - 1]);
                            hexMap[i, j].neighbourList.Add(hexMap[i - 1, j - 1]);
                            hexMap[i, j].neighbourList.Add(hexMap[i , j + 1]);
                            hexMap[i, j].neighbourList.Add(hexMap[i - 1, j + 1]);
                        }
                    }
                }
            }
        }  
	}
}
