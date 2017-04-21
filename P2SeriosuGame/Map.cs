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
        private int xValue = 6;
        private int yValue = 4;

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
