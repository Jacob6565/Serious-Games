using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication4
{
	class MapCalculations
	{
		private List<HexagonButton> _queue;

		public void shortestRoute(List<HexagonButton> buttonList, HexagonButton startingButton, Map map)
		{
			resetAllButtons(buttonList);
			_queue.Add(startingButton);

			while(!_queue.Any())
			{
				HexagonButton currentHex = _queue.First();
				_queue.Remove(_queue.First());
				currentHex.Visited = true;
				if (currentHex.IsEdgeTile == false)
				{
					
				}
			}
		}

		private HexagonButton checkNeighbour(HexagonButton currentHex, HexagonButton neighbourHex)
		{
			

			return neighbourHex;
		}

		public void shortestRouteRec(List<HexagonButton> buttonList, HexagonButton button)
		{
			button.Visited = true;
			
			_queue.Add(button);
		}

		private void resetAllButtons(List<HexagonButton> buttonList)
		{
			foreach (HexagonButton button in buttonList)
			{
				button.Visited = false;
				button.parentButtonList.Clear();
			}
		}
	}
}
