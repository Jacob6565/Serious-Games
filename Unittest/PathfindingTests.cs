using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2SeriousGame;
using System.Windows.Forms;

namespace UnitTests
{
    [TestFixture]
    public class PathfindingTests
    {
        #region Kriis

        [TestCase(5, 5)]
        [TestCase(7, 7)]
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
            GameWindow window = new GameWindow();
            IPathfinding pathfindning = new Pathfinding();
            Map map = new Map(window, x, y, pathfindning);
            BreadthFirst bfs = new BreadthFirst(queue, pathsToEdge, reachableHexList);
            foreach (var hexagonButton in Map.hexMap)
            {
                if (hexagonButton.IsEdgeTile == true)
                {
                    hexagonButton.Passable = false;
                    edgeTiles.Add(hexagonButton);
                }
            }

            int addValue = (x / 4) - 1;

            int fromMiddleToTileNextToEdge;// = (x / 2) + 1;
            bfs.CalculateRoutes(Map.hexMap, Map.hexMap[x / 2, y / 2]);
            fromMiddleToTileNextToEdge = (x / 2) + addValue;

            Assert.AreNotEqual(edgeTiles, bfs.FindTheRoutes());
            Assert.AreEqual(fromMiddleToTileNextToEdge, bfs.FindLongestRoutes()[0].CostToStart);
            Assert.IsTrue((bfs.CheckParent(bfs.FindTheRoutes()[0]) < x) || (bfs.CheckParent(bfs.FindTheRoutes()[0]) < y));

        }
        [TestCase(5, 5)]
        [TestCase(7, 7)]
        [TestCase(9, 9)]
        [TestCase(11, 11)]
        [TestCase(13, 13)]
        [TestCase(21, 21)]
        public void FindShortestRoutes_HexMapWithEdges_FindsRightEndTilesAndRightAmount(int x, int y)
        {
            List<HexagonButton> queue = new List<HexagonButton>();
            List<HexagonButton> pathsToEdge = new List<HexagonButton>();
            List<HexagonButton> reachableHexList = new List<HexagonButton>();
            GameWindow window = new GameWindow();
            IPathfinding pathfindning = new Pathfinding();
            Map map = new Map(window, x, y, pathfindning);
            HexagonButton[,] notStaticHexMap = Map.hexMap;
            BreadthFirst bfs = new BreadthFirst(queue, pathsToEdge, reachableHexList);

            bfs.CalculateRoutes(Map.hexMap, Map.hexMap[x / 2, y / 2]);
           
            CheckIfEachRouteHasTheLowestAndTheSameCost(x, bfs.FindShortestRoutes());
            CheckIfEachEndEdgeTileIsFoundFromStartPoint(x, bfs);

            #region Hvis man skulle tjekke for hver gang den tager et skridt.
            //CheckIfEachEndEdgeTileIsFoundFromOtherPointsOnTheRouteOnEmptyMap(map, x, y, bfs);
            #endregion

            //Finde ud af hvad bfs.FindShortestRoutes(bfs._pathsToEdge) retunerer. Svar: den returnere de mulige edgetiles hvortil der er kortest afstand.
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
        #region Teste for hvert punkt, men kan ikke få musen til at flytte sig og dernæst teste ud fra den nye placering
        ////Ud fra 11,11
        //private void Test(int i, int x)
        //{
        //    if (i == (x / 2) - 4)
        //    {
        //        Assert.AreEqual(true, true);
        //    }
        //    else if (i == (x / 2) - 3)
        //    {
        //        Assert.AreEqual(true, true);
        //    }
        //    else if (i == (x / 2) - 2)
        //    {
        //        Assert.AreEqual(true, true);
        //    }
        //    else if (i == (x / 2) - 1)
        //    {
        //        Assert.AreEqual(true, true);
        //    }
        //}

        //private void CheckIfEachEndEdgeTileIsFoundFromOtherPointsOnTheRouteOnEmptyMap(Map map, int x, int y, BreadthFirst bfs)
        //{

           
        //    //IPathfinding pathfinding = new Pathfinding();
        //    //GameWindow window = new GameWindow();
        //    //MouseButtons a = new MouseButtons();
        //    //MouseEventArgs b = new MouseEventArgs(a, 0, 10, 10, 0);
        //    //HexagonButton onlyForParameter = Map.hexMap[x / 2, y / 2];

        //    //int LastX;
        //    //int LastY;
        //    //for (int i = 0; i < x / 2; i++)
        //    //{

        //    //    MouseButtons mouse = new MouseButtons();
        //    //    MouseEventArgs mouseArgs = new MouseEventArgs(mouse, 0, 10, 10, 0);
        //    //    HexagonButton hex = new HexagonButton(0, 0, false);
        //    //    int c = bfs.FindShortestRoutes().Count();
        //    //    map.MousePositioner(hex, mouseArgs);
        //    //    LastX = map.MouseXCoordinate;
        //    //    LastY = map.MouseYCoordinate;
        //    //    bfs.CalculateRoutes(Map.hexMap, Map.hexMap[LastX, LastY]);
        //    //    int d = bfs.FindShortestRoutes().Count();
        //    //    Assert.AreEqual(c, d);

        //    //    if (c == 1 || c == (x / 2) + 1 || c == 3)
        //    //    {
        //    //        Assert.AreEqual(true, true);
        //    //    }
        //    //    else
        //    //    {
        //    //        Assert.AreEqual(false, true);
        //    //    }
        //    //    if (c == 1 || c == x + 5 || c == 3 && i == 0)
        //    //    {
        //    //        Assert.AreEqual(true, true);
        //    //        if (c == 1)
        //    //        {
        //    //            switch (i)
        //    //            {
        //    //                case 1:
        //    //                case 2:
        //    //                case 3:
        //    //                case 4:
        //    //                    {
        //    //                        Assert.AreEqual(1, bfs.FindShortestRoutes().Count);
        //    //                        break;
        //    //                    }
        //    //            }
        //    //        }
        //    //        Assert.AreEqual(true, true);
        //    //        if (c == 3)
        //    //        {
        //    //            switch (i)
        //    //            {

        //    //            }
        //    //        }
        //    //    }
        //    // }
        //}
        //[TestCase(11,11)]
        //public void Tesst(int x, int y)
        //{
        //    List<HexagonButton> queue = new List<HexagonButton>();
        //    List<HexagonButton> pathsToEdge = new List<HexagonButton>();
        //    List<HexagonButton> reachableHexList = new List<HexagonButton>();
        //    IPathfinding pathfinding = new Pathfinding();
        //    GameWindow window = new GameWindow();
        //    MouseButtons a = new MouseButtons();
        //    MouseEventArgs b = new MouseEventArgs(a, 0, 10, 10, 0);
        //    Map map = new Map(window, x, y, pathfinding);
        //    BreadthFirst bfs = new BreadthFirst(queue, pathsToEdge, reachableHexList);
        //    HexagonButton onlyForParameter = new HexagonButton(x / 2, y / 2, false);
        //    int LastX;
        //    int LastY;
        //    int startX = x / 2;
        //    int startY = y / 2;
        //    int numberOfTilesOnRute = (x / 2) - 1;
        //    bfs.CalculateRoutes(Map.hexMap, Map.hexMap[x / 2, y / 2]);
        //    if (Map.newGame)
        //    {
        //        int c = bfs.FindShortestRoutes().Count;
        //        map.MousePositioner(onlyForParameter, b);
                
        //        LastX = map.MouseXCoordinate;
        //        LastY = map.MouseYCoordinate;
        //        bfs.CalculateRoutes(Map.hexMap, Map.hexMap[LastX, LastY]);
        //        int d = bfs.FindShortestRoutes().Count;
        //        Assert.AreEqual(c, d);

        //        //for (int i = 0; i < numberOfTilesOnRute; i++)
        //        //{
        //        //    map.MousePositioner(onlyForParameter, b);
        //        //    LastX = map.MouseXCoordinate;
        //        //    LastY = map.MouseYCoordinate;
        //        //}
        //    }
     //  }
        #endregion
        #endregion
    }

}
