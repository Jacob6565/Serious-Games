﻿using System;
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

		public void CreateMap(Handler map)
		{
			List<HexagonButton> HexagonButtonList = new List<HexagonButton>();
			for (int i = 0; i < totalHexagonColoumns; i++)
			{
				for (int j = 0; j < totalHexagonRows; j++)
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