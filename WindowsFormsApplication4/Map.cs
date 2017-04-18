using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication4
{
	class Map
	{
        // I selve spillet bruger de 11 x 11 platform
        private int totalHexagonRows = 8;
		private int totalHexagonColoumns = 12;

		public void CreateMap(Handler map)
		{
            HexagonButton[,] hexMap = new HexagonButton[totalHexagonColoumns, totalHexagonRows];
			List<HexagonButton> HexagonButtonList = new List<HexagonButton>();
			for (int i = 0; i < totalHexagonColoumns; i++)
			{
				for (int j = 0; j < totalHexagonRows; j++)
				{
					hexMap[i,j] = new HexagonButton(i, j);
					
					map.DrawButton(hexMap[i,j]);
					map.PlaceHexagonButton(hexMap[i, j]);
				}
			}
		}
	}
}
