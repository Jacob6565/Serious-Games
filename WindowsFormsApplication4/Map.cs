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
        public HexagonButton[,] hexMap;

        public Map(Handler map)
        {
            hexMap = new HexagonButton[totalHexagonColoumns, totalHexagonRows];
            CreateMap(map);

            IniNeighbors();
        }

        public void CreateMap(Handler map)
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
                    map.DrawButton(hexMap[i, j]);
                    map.PlaceHexagonButton(hexMap[i, j]);
                }
            }
        }
        public void IniNeighbors()
        {
            for (int i = 0; i < totalHexagonColoumns; i++)
            {
                for (int j = 0; j < totalHexagonRows; j++)
                {
                    Console.WriteLine($"{hexMap[i, j].XCoordinate}, {hexMap[i, j].YCoordinate}");
                    if (j != 0)
                    {                       
                        hexMap[i, j].neighbourList.Add(hexMap[i , j]);
                    }
                    //else if (i != totalHexagonColoumns)
                    //{
                    //    hexMap[i, j].neighbourList.Add(hexMap[i, j]);
                    //}
                    //else if (j != 0)
                    //{
                    //    hexMap[i, j].neighbourList.Add(hexMap[i, j]);
                    //}
                    //else if (j != totalHexagonRows)
                    //{
                    //    hexMap[i, j].neighbourList.Add(hexMap[i, j]);
                    //}
                }
            }
        }
        
	}
}
