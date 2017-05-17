using NUnit.Framework;
using System.Collections.Generic;
using P2SeriousGame;

namespace UnitTests
{
    [TestFixture]
    public class PathfindingTests
    {
        [TestCase(9, 9)]
        [TestCase(11, 11)]
        [TestCase(13, 13)]
        [TestCase(15, 15)]
        [TestCase(17, 17)]
        [TestCase(19, 19)]
        [TestCase(21, 21)]
        [TestCase(23, 23)]
        [TestCase(25, 25)]
        public void CalculateRoutes_HexMapWithEdges_FindLongestRoutes(int x, int y)
        {
            List<HexagonButton> queue = new List<HexagonButton>();
            List<HexagonButton> pathsToEdge = new List<HexagonButton>();
            List<HexagonButton> reachableHexList = new List<HexagonButton>();
            List<HexagonButton> edgeTiles = new List<HexagonButton>();
            GameForm window = new GameForm();
            IPathfinding pathfindning = new Pathfinding();
            MapTest map = new MapTest(window, x, y, pathfindning);
            BreadthFirst bfs = new BreadthFirst(queue, pathsToEdge, reachableHexList);
            foreach (var hexagonButton in MapTest.hexMap)
            {
                if (hexagonButton.IsEdgeTile == true)
                {
                    hexagonButton.Passable = false;
                    edgeTiles.Add(hexagonButton);
                }
            }

            int addValue = (x / 4) - 1;
            int fromMiddleToTileNextToEdge = (x / 2) + addValue;
            bfs.CalculateRoutes(MapTest.hexMap, MapTest.hexMap[x / 2, y / 2]);

            
            Assert.AreNotEqual(edgeTiles, bfs.FindTheRoutes());
            foreach (HexagonButton hex in bfs.FindLongestRoutes())
            {
                Assert.AreEqual(fromMiddleToTileNextToEdge, hex.CostToStart);
            }
            foreach(HexagonButton hex in bfs.FindLongestRoutes())
            {
                Assert.IsTrue(hex.CostToStart < x || hex.CostToStart < y);
            }

            int tempCost = 0;

            foreach(HexagonButton hex in bfs.FindLongestRoutes())
            {
                if (tempCost == 0)
                    tempCost = hex.CostToStart;
                Assert.AreEqual(tempCost, hex.CostToStart);
            }
        }

        [TestCase(9, 9)]
        [TestCase(11, 11)]
        [TestCase(13, 13)]
        [TestCase(15, 15)]
        [TestCase(17, 17)]
        [TestCase(19, 19)]
        [TestCase(21, 21)]
        [TestCase(23, 23)]
        [TestCase(25, 25)]
        public void FindShortestRoutes_HexMapWithEdges_FindsRightEndTilesAndRightAmount(int x, int y)
        {
            List<HexagonButton> queue = new List<HexagonButton>();
            List<HexagonButton> pathsToEdge = new List<HexagonButton>();
            List<HexagonButton> reachableHexList = new List<HexagonButton>();
            GameForm window = new GameForm();
            IPathfinding pathfindning = new Pathfinding();
            MapTest map = new MapTest(window, x, y, pathfindning);
            HexagonButton[,] notStaticHexMap = MapTest.hexMap;
            BreadthFirst bfs = new BreadthFirst(queue, pathsToEdge, reachableHexList);

            bfs.CalculateRoutes(MapTest.hexMap, MapTest.hexMap[x / 2, y / 2]);
           
            CheckIfEachRouteHasTheLowestAndTheSameCost(x, bfs.FindShortestRoutes());
            CheckIfEachEndEdgeTileIsFoundFromStartPoint(x, bfs);

            #region Hvis man skulle tjekke for hver gang den tager et skridt.
            //CheckIfEachEndEdgeTileIsFoundFromOtherPointsOnTheRouteOnEmptyMap(map, x, y, bfs);
            #endregion

            //Finde ud af hvad bfs.FindShortestRoutes() retunerer. Svar: den returnere de mulige edgetiles hvortil der er kortest afstand.
        }

        private void CheckIfEachRouteHasTheLowestAndTheSameCost(int x, List<HexagonButton> edgeHexes)
        {
            foreach (HexagonButton hex in edgeHexes)
            {
                Assert.AreEqual(x / 2, hex.CostToStart);
            }
        }
        private void CheckIfEachEndEdgeTileIsFoundFromStartPoint(int x, BreadthFirst bfs)
        {
            Assert.AreEqual(x + 5, bfs.FindShortestRoutes().Count);
            //Så der er x + 5 forskellige edgetiles som musen kan gå til, når den står i midten.
        }
    }
}
