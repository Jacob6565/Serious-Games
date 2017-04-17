using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication4
{
	class Map
	{
		private const int _totalHexagons = 80;
		//private int hexagonsInLastRow;
		//private int hexagonRows;
		private int totalHexagonRows = _totalHexagons / 2;
		private int totalHexagonColoumns = _totalHexagons / 2;

		
		
		public void CreateMap(Handler map)
		{
			List<HexagonButton> HexagonButtonList = new List<HexagonButton>();
			for (int i = 0; i < totalHexagonRows; i++)
			{
				for (int j = 0; j < totalHexagonColoumns; j++)
				{
					HexagonButton Button = new HexagonButton(i, j);
					HexagonButtonList.Add(Button);

					map.DrawButton(Button);
					map.PlaceHexagonButton(Button);
				}
			}
		}
	}
}
