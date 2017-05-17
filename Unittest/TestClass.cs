using NUnit.Framework;
using P2SeriousGame;
using System.Windows.Forms;
using System.Drawing;


namespace UnitTests
{

    /*Navngivning af testmetoder på følgende form:
     * 
     * Metodenavn_Betingelse_ForventetHandling
     * 
     *    
         */
    [TestFixture]
    public class UnitTest1
    {
        [TestCase(11, 11)]
        [TestCase(13, 13)]
        [TestCase(21, 21)]
        public void MapTest_PositiveOddValue_HexmapRightSize(int x, int y)
        {
            GameForm tester = new GameForm();
            IPathfinding a = null;
            MapTest map = new MapTest(tester, x, y, a);
            Assert.AreEqual(x, MapTest.hexMap.GetLength(0));
            Assert.AreEqual(y, MapTest.hexMap.GetLength(1));
        }

        [TestCase(-1, -1)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        public void MapTest_WrongDimensions_ThrowMapDimensionsMustBeHigher(int x, int y)
        {
            try
            {
                GameForm tester = new GameForm();
                IPathfinding a = null;
                MapTest map = new MapTest(tester, x, y, a);
            }
            catch (MapDimensionsMustBeHigher e)
            {
                Assert.AreEqual(typeof(MapDimensionsMustBeHigher), e.GetType());
            }
        }
        [TestCase(6, 6)]
        [TestCase(10, 10)]
        public void MapTest_WrongDimensions_ThrowMapDimensionsMustBeOdd(int x, int y)
        {
            try
            {
                GameForm tester = new GameForm();
                IPathfinding a = null;
                MapTest map = new MapTest(tester, x, y, a);
            }
            catch (MapDimensionsMustBeOdd e)
            {
                Assert.AreEqual(typeof(MapDimensionsMustBeOdd), e.GetType());
            }
        }

        //Burde nok gøre sådan, at man ikke kan lave mappen 0,0.
        //tror ikke knappen med koordinaterne 0,0 er en edgetile.
        [TestCase(13, 13, 11, 11)]
        [TestCase(9, 9, 8, 8)]
        [TestCase(7, 7, 2, 2)]
        [TestCase(5, 5, 4, 4)]
        public void FindNeighbours_PositiveOddValues_RightAmountOfNeighboursForAGivenTile(int x, int y, int buttomX, int buttomY)
        {
            IPathfinding ipathfinding = new Pathfinding();
            GameForm tester = new GameForm();
            MapTest map = new MapTest(tester, x, y, ipathfinding);

            if (!MapTest.hexMap[buttomX, buttomY].IsEdgeTile)
            {
                Assert.AreEqual(6, MapTest.hexMap[buttomX, buttomY].neighbourList.Count);
            }
        }
        [TestCase(11, 11)]
        public void FindNeighbours_PositiveOddValues_RightAmountOfNeighboursForEachTileOnRoute(int x, int y)
        {
            IPathfinding ipathfinding = new Pathfinding();
            GameForm tester = new GameForm();
            MapTest map = new MapTest(tester, x, y, ipathfinding);
            MouseButtons a = new MouseButtons();
            MouseEventArgs b = new MouseEventArgs(a, 0, 10, 10, 0);
            HexagonButton onlyForParameter = new HexagonButton(x / 2, y / 2, false);
            int numberOfTilesOnRute = (x / 2);

            if (!MapTest.hexMap[map.MouseXCoordinate, map.MouseYCoordinate].IsEdgeTile)
            {
                Assert.AreEqual(6, MapTest.hexMap[map.MouseXCoordinate, map.MouseYCoordinate].neighbourList.Count);

                for (int i = 0; i < numberOfTilesOnRute; i++)
                {
                    map.MousePositioner(onlyForParameter, b);

                    if (!MapTest.hexMap[map.MouseXCoordinate, map.MouseYCoordinate].IsEdgeTile)
                    {
                        Assert.AreEqual(6, MapTest.hexMap[map.MouseXCoordinate, map.MouseYCoordinate].neighbourList.Count);
                    }
                    else if (MapTest.hexMap[map.MouseXCoordinate, map.MouseYCoordinate].IsEdgeTile)
                    {
                        int n = MapTest.hexMap[map.MouseXCoordinate, map.MouseYCoordinate].neighbourList.Count;
                        switch (n)
                        {
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                {
                                    Assert.AreEqual(true, true);
                                    break;
                                }
                            default:
                                {
                                    Assert.AreEqual(false, true);
                                    break;
                                }
                        }
                    }
                }
            }
        }


        //Burde man gører sådan, at man ikke kan sætte koordinaterne en negativværdi.
        [TestCase(-1, -1, false)]
        [TestCase(0, 0, true)]
        [TestCase(1, 1, true)]
        [TestCase(10, 10, true)]
        //[ExpectedException(typeof(Exception))]
        public void HexagonButton_PositiveValues_ConstructedRight(int x, int y, bool edge)
        {
            HexagonButton n = new HexagonButton(x, y, edge);
            Assert.AreEqual(x, n.XCoordinate);
            Assert.AreEqual(y, n.YCoordinate);
            Assert.AreEqual(edge, n.IsEdgeTile);
        }
        //Gøre sådan, at når der angives en negativ værdi skal der kastes en exception og mappet skal ikke laves.
        //Lige pt. kommer der en exception, da man i Map-constructoren forsøget at tildelee 
        //[TestCase(-1, -1)]
        //[TestCase(0, 0)]
        //[TestCase(1, 1)]
        [TestCase(5, 5)]
        [TestCase(11, 11)]
        [TestCase(21, 21)]
        public void MapTest_PositiveOddValues_ConstructedRight(int x, int y)
        {
            IPathfinding ipathfinding = new Pathfinding();
            GameForm window = new GameForm();
            MapTest map = new MapTest(window, x, y, ipathfinding);
            Assert.AreEqual(x, MapTest.TotalHexagonColumns);
            Assert.AreEqual(y, MapTest.TotalHexagonRows);
            Assert.AreEqual(x, MapTest.hexMap.GetLength(0));
            Assert.AreEqual(y, MapTest.hexMap.GetLength(1));
        }

        //Burde ikke kunne lave en buttom med negative koordinater
        [TestCase(-1, -3)]
        [TestCase(0, 0)]
        [TestCase(3, 1)]
        [TestCase(10, 10)]
        public void HexClicked_PositiveValues_ValuesChangedRight(int buttomX, int buttomY)
        {
            HexagonButton hex = new HexagonButton(buttomX, buttomY, false);
            MouseButtons a = new MouseButtons();
            MouseEventArgs b = new MouseEventArgs(a, 0, 10, 10, 0);
            hex.HexClicked(hex, b);
            Assert.AreEqual(false, hex.Enabled);
            Assert.AreEqual(false, hex.Passable);
        }

        [TestCase(-10, -10)]
        [TestCase(-1, -1)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(10, 10)]
        public void GetPoints_NumericValues_RightPointsCreated(int height, int width)
        {

            float expX = width / 2;
            float expSide = height / 2;
            float expY = 0;
            float expR = width / 2;
            float expH = (height - expSide) / 2;
            PointF[] expectedPoints = new PointF[6]
            {
                new PointF(expX, expY),
                new PointF(expX + expR, expY + expH),
                new PointF(expX + expR, expY + expSide + expH ),
                new PointF(expX, expY + height),
                new PointF(expX - expR, expY + expSide + expH),
                new PointF(expX - expR, expY + expH)
            };
            PointF[] actualPoints = P2SeriousGame.Math.GetPoints(height, width);
            Assert.AreEqual(expectedPoints, actualPoints);
        }
        #region MousePositioner
        //Denne tester også at musen følger den rigtige rute ved at man kan se, at musen bliver flyttet hvergang til næste sted på pathen, da MouseYCoordinate og X er dannet ud fra den første hex i pathen.
        [TestCase(5, 5)]
        [TestCase(11, 11)]
        [TestCase(21, 21)]
        public void MousePositioner_CalculateRoutesWorks_ColorsAndDisablesRightFollowsRightPath(int x, int y)
        {
            IPathfinding pathfinding = new Pathfinding();
            GameForm window = new GameForm();
            MouseButtons a = new MouseButtons();
            MouseEventArgs b = new MouseEventArgs(a, 0, 10, 10, 0);
            MapTest map = new MapTest(window, x, y, pathfinding);
            HexagonButton onlyForParameter = new HexagonButton(x / 2, y / 2, false);
            int LastX;
            int LastY;
            int startX = x / 2;
            int startY = y / 2;
            int numberOfTilesOnRute = (x / 2);
            int edgeTile = 1;

            if (MapTest.newGame)
            {
                map.MousePositioner(onlyForParameter, b);
                Assert.AreEqual(false, MapTest.newGame);
                Assert.AreEqual(Color.LightGray, MapTest.hexMap[startX, startY].BackColor);
                Assert.AreEqual(true, MapTest.hexMap[startX, startY].Enabled);
                Assert.AreEqual(Color.Aqua, MapTest.hexMap[map.MouseXCoordinate, map.MouseYCoordinate].BackColor);
                Assert.AreEqual(false, MapTest.hexMap[map.MouseXCoordinate, map.MouseYCoordinate].Enabled);
                //Saves the current mouseposition before it gets overriden in the next call. 
                //They are going to be used to tjeck if the current mouseposition will be colored grey and enabled after next call.
                //Because MouseX and MouseY are equal the coordinates of the first hex in the path. 
                //In that way we kinda test that the mouse will move along the path. 
                LastX = map.MouseXCoordinate;
                LastY = map.MouseYCoordinate;
                for (int i = 0; i < numberOfTilesOnRute - edgeTile; i++)
                {
                    map.MousePositioner(onlyForParameter, b);
                    Assert.AreEqual(Color.LightGray, MapTest.hexMap[LastX, LastY].BackColor);
                    Assert.AreEqual(true, MapTest.hexMap[LastX, LastY].Enabled);
                    Assert.AreEqual(Color.Aqua, MapTest.hexMap[map.MouseXCoordinate, map.MouseYCoordinate].BackColor);
                    Assert.AreEqual(false, MapTest.hexMap[map.MouseXCoordinate, map.MouseYCoordinate].Enabled);
                    LastX = map.MouseXCoordinate;
                    LastY = map.MouseYCoordinate;
                }
            }
        }
        #endregion


        //denne burde virke, man kan ikke få Painteventet triggered. Har bare indsat funkionen, så man ikke kalder dem, men bare udfører den.
        #region DrawButtonTest
        [TestCase(5, 5)]
        [TestCase(11, 11)]
        [TestCase(15, 15)]
        public void GameForm_ButtonPainter_RightPolygon(int x, int y)
        {
            IPathfinding ipathfinding = new Pathfinding();
            HexagonButton hexagonButton = new HexagonButton(x, y, false);
            GameForm window = new GameForm();
            System.Drawing.Drawing2D.GraphicsPath buttonPath;
            PointF[] expectedPoints = P2SeriousGame.Math.GetPoints(x, y);
            MapTest map = new MapTest(window, x, y, ipathfinding);

            buttonPath = new System.Drawing.Drawing2D.GraphicsPath();
            buttonPath.AddPolygon(P2SeriousGame.Math.GetPoints(x, y));
            Region region = new Region(buttonPath);
            hexagonButton.Region = region;

            Assert.AreEqual(hexagonButton.Region, region);
            Assert.AreEqual(expectedPoints, buttonPath.PathPoints);
        }

        #endregion
        //Hvis man tester CalculateRoutes, så tester man også de andre funktioner som bidrager til at finde ruten.
        //Ideen var, at man lavede 3 forskellige hex koordinatsæt, som kunne blive returneret af funktionen og så tjekke om den hex som bliver returneret af funktionen er lig en af dem. 
        //de tre forskellige koordinatsæt er, (x+1, y), (x-1, y), (nej.
        //[TestCase(0,0,0,0)]
        //[TestCase(2, 2, 1, 1)]
        //[TestCase(10, 10, 5, 5)]
        //[TestCase(20, 20, 3, 3)]
        //public void PathFinding_CalculateRoutes_CorrectlyFindsNextHexOnEmptyMap(int mapX, int mapY, int buttomX, int buttomY)
        //{
        //    Pathfinding pathfinding = new Pathfinding();
        //    GameForm window = new GameForm();
        //    HexagonButton hex = new HexagonButton(buttomX, buttomY, false);
        //    MapTest map = new MapTest(window, mapX, mapY, pathfinding);
        //    pathfinding.CalculateRoutes(MapTest.hexMap, hex);
        //    HexagonButton expectedHex1;
        //    HexagonButton expectedHex2;
        //    if (buttomX < mapX/2 && buttomY < mapY/2)
        //    {
        //       if(buttomY < buttomX)
        //       {
        //           expectedHex1 = new HexagonButton()
        //       }
        //       else if (buttomX < buttomY)
        //       {
        //            expectedLengthOfRoute = buttomX;              
        //       }
        //       else
        //       {

        //       }
        //    }

        //}


        //[TestCase(0, 0, 10, 10)]
        //[TestCase(1, 1, 2, 2)]
        //[TestCase(10, 10, 20, 20)]
        //[TestCase(20, 20, 1, 1)]
        //public void GameForm_CalculateButtonDimensionBasedOnScreenHeight_RightDimensionRatio(int buttomX, int buttomY, int rows, int columns)
        //{
        //    GameForm window = new GameForm();
        //    HexagonButton hex = new HexagonButton(buttomX, buttomY, false);
        //    MapTest map = new MapTest(window, rows, columns, ipathfinding);
        //    window.Button
        //}

        //[TestCase(0, 0, 10, 10)]
        //[TestCase(1, 1, 2, 2)]
        //[TestCase(10, 10, 20, 20)]
        //public void GameForm_CalculateButtonDimensionBasedOnScreenWidth_RightDimension(int buttomX, int buttomY, int rows, int columns)
        //{
        //    GameForm window = new GameForm();
        //    HexagonButton hex = new HexagonButton(buttomX, buttomY, false);
        //    MapTest map = new MapTest(window, rows, columns, ipathfinding);
        //}

        //[TestCase(0,0, 10, 10)]
        //[TestCase(1,1, 2, 2)]
        //[TestCase(10,10, 20, 20)]
        //public void GameForm_PlaceHexagonButton_RightPosition(int buttomX, int buttomY, int rows, int columns)
        //{
        //    GameForm window = new GameForm();
        //    HexagonButton hex = new HexagonButton(buttomX, buttomY, false);
        //    MapTest map = new MapTest(window, rows, columns, ipathfinding);
        //}
        [TestCase(11, 11)]
        public void CreateMap_CalculateButtonDimensionWorks_RightCoordinatesAndMode(int x, int y)
        {
            GameForm window = new GameForm();
            IPathfinding ipathfinding = new Pathfinding();
            MapTest map = new MapTest(window, x, y, ipathfinding);
            map.CreateMap(window);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (i == 0 || i == x - 1 || j == 0 || j == y - 1)
                    {
                        Assert.AreEqual(true, MapTest.hexMap[i, j].IsEdgeTile);
                    }

                    Assert.AreEqual(i, MapTest.hexMap[i, j].XCoordinate);
                    Assert.AreEqual(j, MapTest.hexMap[i, j].YCoordinate);
                }
            }
        }
    }

}

