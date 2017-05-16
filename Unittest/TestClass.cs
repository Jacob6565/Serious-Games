using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2SeriousGame;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace UnitTests
{

    /*Navngivning af testmetoder på følgende form:
     * 
     * Metodenavn_Betingelse_ForventetHandling
     * 
     *    
         */
    #region Jacob
    [TestFixture]
    public class UnitTest1
    {
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(11,11)]
        [TestCase(13, 13)]
        [TestCase(50, 50)]
        public void Map_PositiveOddValue_HexmapRightSize(int x, int y)
        {
            GameWindow tester = new GameWindow();
            IPathfinding a = null;
            Map map = new Map(tester, x, y, a);
            Assert.AreEqual(x, Map.hexMap.GetLength(0));
            Assert.AreEqual(y, Map.hexMap.GetLength(1));
        }

        //Burde nok gøre sådan, at man ikke kan lave mappen 0,0.
        //tror ikke knappen med koordinaterne 0,0 er en edgetile.
        [TestCase(9, 9, 8, 8)]
        [TestCase(5, 5, 4, 4)]
        [TestCase(1, 1, 0, 0)]
        [TestCase(0, 0, 0, 0)]
        public void FindNeighbours_PositiveOddValues_RightAmountOfNeighbours(int x, int y, int buttomX, int buttomY)
        {
            IPathfinding ipathfinding = new Pathfinding();
            GameWindow tester = new GameWindow();
            Map map = new Map(tester, x, y, ipathfinding);

            if (!Map.hexMap[buttomX, buttomY].IsEdgeTile)
            {
                Assert.AreEqual(6, Map.hexMap[buttomX, buttomY].neighbourList.Count);
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
        [TestCase(-1, -1)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(5, 5)]
        [TestCase(20, 20)]
        public void Map_PositiveOddValues_ConstructedRight(int x, int y)
        {
            IPathfinding ipathfinding = new Pathfinding();
            GameWindow window = new GameWindow();
            Map map = new Map(window, x, y, ipathfinding);
            Assert.AreEqual(x, Map.TotalHexagonColumns);
            Assert.AreEqual(y, Map.TotalHexagonRows);
            Assert.AreEqual(x, Map.hexMap.GetLength(0));
            Assert.AreEqual(y, Map.hexMap.GetLength(1));
        }

        //Burde ikke kunne lave en buttom med negative koordinater
        [TestCase(10, 10)]
        [TestCase(0, 0)]
        [TestCase(3, 1)]
        [TestCase(-1, -3)]
        public void HexClicked_PositiveValues_ValuesChangedRight(int buttomX, int buttomY)
        {
            HexagonButton hex = new HexagonButton(buttomX, buttomY, false);
            MouseButtons a = new MouseButtons();
            MouseEventArgs b = new MouseEventArgs(a, 0, 10, 10, 0);
            hex.HexClicked(hex, b);
            Assert.AreEqual(false, hex.Enabled);
            Assert.AreEqual(false, hex.Passable);
        }

        [TestCase(-10,-10)]
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
        [TestCase(3, 3)]
        [TestCase(11, 11)]
        [TestCase(21, 21)]
        public void MousePositioner_CalculateRoutesWorks_ColorsAndDisablesRightFollowsRightPath(int x, int y)
        {
            IPathfinding pathfinding = new Pathfinding();
            GameWindow window = new GameWindow();
            MouseButtons a = new MouseButtons();
            MouseEventArgs b = new MouseEventArgs(a, 0, 10, 10, 0);
            Map map = new Map(window, x, y, pathfinding);
            HexagonButton onlyForParameter = new HexagonButton(x/2, y/2, false);
            int LastX;
            int LastY;
            int startX = x / 2;
            int startY = y / 2;
            int numberOfTilesOnRute = (x / 2) - 1;
            
            if (Map.newGame)
            {
                map.MousePositioner(onlyForParameter, b);
                Assert.AreEqual(false, Map.newGame);
                Assert.AreEqual(Color.LightGray, Map.hexMap[startX, startY].BackColor);
                Assert.AreEqual(true, Map.hexMap[startX, startY].Enabled);
                Assert.AreEqual(Color.Aqua, Map.hexMap[map.MouseXCoordinate, map.MouseYCoordinate].BackColor);
                Assert.AreEqual(false, Map.hexMap[map.MouseXCoordinate, map.MouseYCoordinate].Enabled);
                //Saves the current mouseposition before they gets overriden in the next call. 
                //They are going to be used to tjeck if the current mouseposition will be colored grey and enabled after next call.
                //Because MouseX and MouseY are equal the coordinates of the first hex in the path. 
                //in that way we kinda test that the mouse will move along the path. 
                LastX = map.MouseXCoordinate;
                LastY = map.MouseYCoordinate;
                for (int i = 0; i < numberOfTilesOnRute; i++)
                {
                    map.MousePositioner(onlyForParameter, b);
                    Assert.AreEqual(Color.LightGray, Map.hexMap[LastX, LastY].BackColor);
                    Assert.AreEqual(true, Map.hexMap[LastX, LastY].Enabled);
                    Assert.AreEqual(Color.Aqua, Map.hexMap[map.MouseXCoordinate, map.MouseYCoordinate].BackColor);
                    Assert.AreEqual(false, Map.hexMap[map.MouseXCoordinate, map.MouseYCoordinate].Enabled);
                    LastX = map.MouseXCoordinate;
                    LastY = map.MouseYCoordinate;
                }
            }
        }
        #endregion


        //denne burde virke, man kan ikke få Painteventet triggered. Har bare indsat funkionen, så man ikke kalder dem, men bare udfører den.
        #region DrawButtonTest
        [TestCase(-1, -1)]
        [TestCase(0, 0)]
        [TestCase(2, 2)]
        [TestCase(5, 5)]
        [TestCase(10, 10)]
        [TestCase(11,11)]
        public void GameWindow_ButtonPainter_RightPolygon(int x, int y)
        {
            IPathfinding ipathfinding = new Pathfinding();
            HexagonButton hexagonButton = new HexagonButton(x, y, false);
            GameWindow window = new GameWindow();
            System.Drawing.Drawing2D.GraphicsPath buttonPath;
            PointF[] expectedPoints = P2SeriousGame.Math.GetPoints(x, y);
            Map map = new Map(window, x, y, ipathfinding);

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
        //    GameWindow window = new GameWindow();
        //    HexagonButton hex = new HexagonButton(buttomX, buttomY, false);
        //    Map map = new Map(window, mapX, mapY, pathfinding);
        //    pathfinding.CalculateRoutes(Map.hexMap, hex);
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
        //public void GameWindow_CalculateButtonDimensionBasedOnScreenHeight_RightDimensionRatio(int buttomX, int buttomY, int rows, int columns)
        //{
        //    GameWindow window = new GameWindow();
        //    HexagonButton hex = new HexagonButton(buttomX, buttomY, false);
        //    Map map = new Map(window, rows, columns, ipathfinding);
        //    window.Button
        //}

        //[TestCase(0, 0, 10, 10)]
        //[TestCase(1, 1, 2, 2)]
        //[TestCase(10, 10, 20, 20)]
        //public void GameWindow_CalculateButtonDimensionBasedOnScreenWidth_RightDimension(int buttomX, int buttomY, int rows, int columns)
        //{
        //    GameWindow window = new GameWindow();
        //    HexagonButton hex = new HexagonButton(buttomX, buttomY, false);
        //    Map map = new Map(window, rows, columns, ipathfinding);
        //}

        //[TestCase(0,0, 10, 10)]
        //[TestCase(1,1, 2, 2)]
        //[TestCase(10,10, 20, 20)]
        //public void GameWindow_PlaceHexagonButton_RightPosition(int buttomX, int buttomY, int rows, int columns)
        //{
        //    GameWindow window = new GameWindow();
        //    HexagonButton hex = new HexagonButton(buttomX, buttomY, false);
        //    Map map = new Map(window, rows, columns, ipathfinding);
        //}

        //[TestCase(2, 2)]
        //[TestCase(5, 4)]
        //[TestCase(5, 6)]
        //public void Map_HexagonButtons_ClickEvent(int x, int y)
        //{
        //    GameWindow tester = new GameWindow();

        //    Map map = new Map(tester, 10, 10, a);

        //    Map.hexMap[x, y].HexClicked(null, System.Windows.Forms.MouseEventArgs);

        //    Assert.AreEqual(false, Map.hexMap[x, y].Passable);
        //}
        #endregion
    }

}

