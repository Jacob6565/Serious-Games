using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication4
{
	class Map
	{
		private int totalHexagonRows = 8;
		private int totalHexagonColoumns = 12;
        public HexagonButton[,] hexMap;

        public Map(Handler map)
        {
            hexMap = new HexagonButton[totalHexagonColoumns, totalHexagonRows];
            CreateMap(map);
        }

        public void CreateMap(Handler map)
		{
            
			List<HexagonButton> HexagonButtonList = new List<HexagonButton>();
			for (int i = 0; i < totalHexagonColoumns; i++)
			{
				for (int j = 0; j < totalHexagonRows; j++)
				{
                    bool isEdge = false;
                    if (i == 0 || i == totalHexagonColoumns-1 || j == 0 || j == totalHexagonRows-1)
                    {
                        isEdge = true;
                    }
					hexMap[i,j] = new HexagonButton(i, j, isEdge);
					
					map.DrawButton(hexMap[i,j]);
					map.PlaceHexagonButton(hexMap[i, j]);
				}
			}
		}
	}
}
