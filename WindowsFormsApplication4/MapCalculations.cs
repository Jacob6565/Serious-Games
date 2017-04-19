using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication4
{
	class MapCalculations
	{
		private List<HexagonButton> _queue = new List<HexagonButton>();
		private List<HexagonButton> _pathsToEdge = new List<HexagonButton>();
		private Random rnd = new Random();

		public void CalculateRoutes(/*List<HexagonButton> buttonList,*/ HexagonButton startingHex)
		{
			//resetAllButtons(buttonList);
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
						if (hex.Visited == false)
						{
							hex.parent = currentHex;
							_queue.Add(hex);
							hex.Visited = true;
						}
					}
				} else
				{
					_pathsToEdge.Add(currentHex);
				}
			}
			List<HexagonButton> shortestRoutes = FindShortestRoutes(_pathsToEdge);
			List<HexagonButton> shortestRouteByRand = ChooseRouteByRand(shortestRoutes);

			Console.WriteLine($"The route starts with: ({startingHex.XCoordinate}, {startingHex.YCoordinate})");
			foreach (HexagonButton hex in shortestRouteByRand)
			{
				Console.WriteLine($"The route goes by: ({hex.XCoordinate}, {hex.YCoordinate})");
			}
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

		private List<HexagonButton> ChooseRouteByRand(List<HexagonButton> shortestRoutes)
		{
			var shortestRouteByRand = new List<HexagonButton>();
			int routeToChoose = rnd.Next(0, shortestRoutes.Count);

			HexagonButton edgeHex = shortestRoutes.ElementAt(routeToChoose);
			HexagonButton currentHex = edgeHex;

			do
			{
				shortestRouteByRand.Add(currentHex);
				currentHex.BackColor = System.Drawing.Color.FromArgb(50, 205, 50);
				currentHex = currentHex.parent;
			} while (currentHex.parent != null);

			shortestRouteByRand.Reverse();
			
			return shortestRouteByRand;
		}

		private void ResetAllButtons(List<HexagonButton> buttonList)
		{
			foreach (HexagonButton button in buttonList)
			{
				button.Visited = false;
				button.parent = null;
			}
		}
	}
}
