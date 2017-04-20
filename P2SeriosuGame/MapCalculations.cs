﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2SeriousGame
{
	class MapCalculations
	{
		private List<HexagonButton> _queue = new List<HexagonButton>();
		private List<HexagonButton> _pathsToEdge = new List<HexagonButton>();
        private List<HexagonButton> _reachableHexList = new List<HexagonButton>();
		private Random rnd = new Random();

		public void CalculateRoutes(HexagonButton[,] hexMap, HexagonButton startingHex)
		{
			ResetAllButtons(hexMap);
			_pathsToEdge.Clear();
            _reachableHexList.Clear();
			_queue.Add(startingHex);

			while(_queue.Any())
			{
				HexagonButton currentHex = _queue.First();
				_queue.Remove(_queue.First());
				currentHex.Visited = true;
				if (currentHex.IsEdgeTile == false)
				{
					foreach (HexagonButton hex in currentHex.neighbourList)
					{
						if (hex.Visited == false && hex.Passable == true)
						{
							hex.parent = currentHex;
							_queue.Add(hex);
							hex.Visited = true;
                            _reachableHexList.Add(hex);
						}
					}
				} else
				{
					_pathsToEdge.Add(currentHex);
				}
			}

            FindTheRoute(_pathsToEdge, _reachableHexList);
		}

        private void FindTheRoute(List<HexagonButton> pathsToEdge, List<HexagonButton> reachableHexList)
        {
            var bestRoutes = new List<HexagonButton>();

            //If logic statement is true, then there is a route to an edge
            if(pathsToEdge.Count > 0)
            {
                bestRoutes = FindShortestRoutes(pathsToEdge);
            }

            //If logic statement is true, then there is a reachable route and the mouse has not been totally trapped yet 
            else if (reachableHexList.Count > 0)
            {
                bestRoutes = FindLongestRoutes(reachableHexList);
            }

            else
            {
                //You Won :)
                throw new NotImplementedException();
            }

            List<HexagonButton> bestRouteByRand = ChooseRouteByRand(bestRoutes);
        }

        //Reachable hexes that are not edges of the map. Used for finding the longest route when mouse is trapped
        private List<HexagonButton> FindLongestRoutes(List<HexagonButton> reachableHexList)
        {
            var longestRoutes = new List<HexagonButton>();
            foreach (HexagonButton hex in reachableHexList)
            {
                hex.CostToStart = CheckParent(0, hex);

                if (longestRoutes.Count == 0)
                    longestRoutes.Add(hex);
                else if (longestRoutes.First().CostToStart < hex.CostToStart)
                {
                    longestRoutes.Clear();
                    longestRoutes.Add(hex);
                }
                else if (longestRoutes.First().CostToStart == hex.CostToStart)
                    longestRoutes.Add(hex);
            }
            return longestRoutes;
        }

		private List<HexagonButton> FindShortestRoutes(List<HexagonButton> edgeHexList)
		{
			var shortestRoutes = new List<HexagonButton>();
			foreach (HexagonButton hex in edgeHexList)
			{
				hex.CostToStart = CheckParent(0, hex);

				if (shortestRoutes.Count == 0)
					shortestRoutes.Add(hex);
				else if (shortestRoutes.First().CostToStart > hex.CostToStart)
				{
					shortestRoutes.Clear();
					shortestRoutes.Add(hex);
				}
				else if (shortestRoutes.First().CostToStart == hex.CostToStart)
					shortestRoutes.Add(hex);
			}
			return shortestRoutes;
		}

		private int CheckParent(int count, HexagonButton hexToCheck)
		{
			if(hexToCheck.parent == null)
			{
				return count;
			} else
			{
				return CheckParent(count + 1, hexToCheck.parent);
			}
		}

		private List<HexagonButton> ChooseRouteByRand(List<HexagonButton> routes)
		{
			var routeByRand = new List<HexagonButton>();
			int routeToChoose = rnd.Next(0, routes.Count);

			HexagonButton edgeHex = routes.ElementAt(routeToChoose);
			HexagonButton currentHex = edgeHex;

			do
			{
				routeByRand.Add(currentHex);
				currentHex.BackColor = System.Drawing.Color.FromArgb(50, 205, 50);
				currentHex = currentHex.parent;
			} while (currentHex.parent != null);

			routeByRand.Reverse();
			
			return routeByRand;
		}

		private void ResetAllButtons(HexagonButton[,] hexMap)
		{
			foreach (HexagonButton hex in hexMap)
			{
				hex.Visited = false;
				hex.parent = null;
				if (hex.BackColor == System.Drawing.Color.FromArgb(50, 205, 50))
				{
					hex.BackColor = System.Drawing.Color.LightGray;
				}
			}
		}
	}
}