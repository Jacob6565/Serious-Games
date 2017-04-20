using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2SeriousGame
{
	public class Map
	{
        private int totalHexagonRows = 0;
		private int totalHexagonColoumns = 0;
        public HexagonButton[,] hexMap;
		MapCalculations calc = new MapCalculations();
		private HexagonButton currentMousePosition;

        public Map(Handler handler, int xSize, int ySize)
        {
            totalHexagonRows = ySize;
            totalHexagonColoumns = xSize;
            hexMap = new HexagonButton[totalHexagonColoumns, totalHexagonRows];
            CreateMap(handler);

            IniNeighbors();
        }

        public void CreateMap(Handler handler)
        {
            for (int i = 0; i < totalHexagonColoumns; i++)
            {
                for (int j = 0; j < totalHexagonRows; j++)
                {
                    bool isEdge = false;
                    if (i == 0 || i == totalHexagonColoumns - 1 || j == 0 || j == totalHexagonRows - 1)
                    {
                        isEdge = true;
                    }
                    hexMap[i, j] = new HexagonButton(i, j, isEdge);
                    handler.DrawButton(hexMap[i, j], this);
                    handler.PlaceHexagonButton(hexMap[i, j]);
                }
            }
        }

		public void HexClicked(object sender, MouseEventArgs e) // Start point fra midten
		{
			hexMap[6, 4].BackColor = System.Drawing.Color.Aqua;
			calc.CalculateRoutes(hexMap, hexMap[6, 4]);
		}

		public void IniNeighbors()
        {
            for (int i = 0; i < totalHexagonColoumns; i++)
            {
                for (int j = 0; j < totalHexagonRows; j++)
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
