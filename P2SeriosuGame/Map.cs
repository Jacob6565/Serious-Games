﻿using System;
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
        private static bool _firstThreeGets = true;
        private static int _totalHexagonRows = 0;
        public static int TotalHexagonRows
        {
            get { return _totalHexagonRows; }
            set { _totalHexagonRows = value; }
        }

        private static int _totalHexagonColoumns = 0;
        public static int TotalHexagonColumns
        {
            get { return _totalHexagonColoumns; }
            set { _totalHexagonColoumns = value; }
        }


        public HexagonButton[,] hexMap;
		Pathfinding path = new Pathfinding();
		private HexagonButton currentMousePosition;
        private int xValue;
        public int XValue
        {
            get
            {
                if (_firstThreeGets)
                {
                    return TotalHexagonColumns / 2;
                }
                else
                {
                    return xValue;
                }
            }
            set
            {
                xValue = value;
            }
        }
        private int yValue;
        public int YValue
        {
            get
            {
                if (_firstThreeGets)
                {
                    return TotalHexagonRows / 2;
                }
                else
                {
                    return yValue;
                }
            }
            set
            {
                yValue = value;
            }
        }


        /// <summary>
        /// Creates a HexagonButton grid in xSize * ySize, needs a reference to the handler window.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="xSize"></param>
        /// <param name="ySize"></param>
        public Map(Handler handler, int xSize, int ySize)
        {
            TotalHexagonRows = ySize;
            TotalHexagonColumns = xSize;
            hexMap = new HexagonButton[TotalHexagonColumns, TotalHexagonRows];
            CreateMap(handler);     
            IniNeighbours();
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
            hexMap[XValue, YValue].BackColor = System.Drawing.Color.LightGray;
            hexMap[XValue, YValue].Enabled = true;

            //Nye position.
            path.CalculateRoutes(hexMap, hexMap[XValue, YValue]);
            _firstThreeGets = false;
            XValue = path.FirstButtonInPath.XCoordinate;
            YValue = path.FirstButtonInPath.YCoordinate;
            hexMap[XValue, YValue].BackColor = System.Drawing.Color.Aqua;
            hexMap[XValue, YValue].Enabled = false;       
        }

        /// <summary>
        /// Finds the neighbours for each HexagonButton in Map.cs (except of the edge buttons).
        /// </summary>
		public void IniNeighbours()
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
