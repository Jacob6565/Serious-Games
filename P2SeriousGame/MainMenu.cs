﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P2SeriousGame.SQL;

namespace P2SeriousGame
{
    public partial class MainMenu : Form
    {
        private List<Form> menues = new List<Form>();

        private FlowLayoutPanel menuPanel = new FlowLayoutPanel();

        private Database SQL = new Database();

        #region constant vars

        private int ButtonWidth;
        private int ButtonHeight;
        private int ButtonHeightOffset => (3 * (ButtonHeight / 4));

        int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
        int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

        //These constants declare the amount of reserved space or margins, where 0.05 equals 5%
        private const double _leftWidthReserved = 0.05;
        private const double _endWidthReserved = 0.12;
        private const double _topHeightReserved = 0.05;
        private const double _bottomHeightReserved = 0.03;

        //The gamescreen variables sets the height and width of the area on the screen where hexagonbutton can be drawn
        private double _gameScreenWidth = Screen.PrimaryScreen.Bounds.Width * (1 - (_leftWidthReserved + _endWidthReserved));
        private double _gameScreenHeight = Screen.PrimaryScreen.Bounds.Height * (1 - (_topHeightReserved + _bottomHeightReserved));

        //Centers the hexagonmap starting placement, if the hexagonmap doesnt fill out the entire gamescreen width
        private double WidthCentering => (_gameScreenWidth - (ButtonWidth * Map.TotalHexagonColumns)) / 2;

        //WidthStart and heightStart sets the starting place for the hexagonmap
        private int WidthStart => (int)((_leftWidthReserved * Screen.PrimaryScreen.Bounds.Width) + WidthCentering);
        private int _heightStart = (int)(_topHeightReserved * Screen.PrimaryScreen.Bounds.Height);
        #endregion
        public MainMenu()
        {
            InitializeComponent();
        }

        private void InitializeMenues()
        {
            menues.Add(new GameWindow());
            MenuPanel();
            //menues.Add(new AdministratorMenu());
        }

        public void DrawWindow(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            InitializeMenues();
        }

        private void MenuPanel()
        {
            this.Controls.Add(menuPanel);
            menuPanel.Width = ScreenWidth;
            menuPanel.Height = ScreenHeight;
            menuPanel.BackColor = Color.BlanchedAlmond;
            menuPanel.FlowDirection = FlowDirection.TopDown;
            menuPanel.Padding = new Padding(Size.Width / 2 - 150, 25, Size.Width / 2 + 150, 25);
            StartGameButton(menuPanel);
            StartAdministratorMenuButton(menuPanel);
            CloseMenuButton(menuPanel);
        }

        private void StartGameButton(Panel panel)
        {
            Button btnStartGame = new Button();
            btnStartGame.Size = new Size(300, 100);
            btnStartGame.TabStop = false;
            btnStartGame.FlatStyle = FlatStyle.Flat;
            btnStartGame.FlatAppearance.BorderSize = 0;
            btnStartGame.BackColor = Color.Azure;
            btnStartGame.Location = new Point(this.Bounds.Right / 2 - btnStartGame.Width / 2, this.Bounds.Top + 60);
            btnStartGame.MouseClick += SwitchToGame;
            btnStartGame.Text = "Start Game";
            btnStartGame.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(btnStartGame);
        }

        private void StartAdministratorMenuButton(Panel panel)
        {
            Button btnStartAdministrator = new Button();
            btnStartAdministrator.Size = new Size(300, 100);
            btnStartAdministrator.TabStop = false;
            btnStartAdministrator.FlatStyle = FlatStyle.Flat;
            btnStartAdministrator.FlatAppearance.BorderSize = 0;
            btnStartAdministrator.BackColor = Color.Azure;
            btnStartAdministrator.Location = new Point(this.Bounds.Right / 2 - btnStartAdministrator.Width / 2, this.Bounds.Top + 60);
            btnStartAdministrator.MouseClick += SwitchToAdministration;
            btnStartAdministrator.Text = "Administrator";
            btnStartAdministrator.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(btnStartAdministrator);
        }

        private void CloseMenuButton(Panel panel)
        {
            Button btnCloseGame = new Button();
            btnCloseGame.Size = new Size(300, 100);
            btnCloseGame.TabStop = false;
            btnCloseGame.FlatStyle = FlatStyle.Flat;
            btnCloseGame.FlatAppearance.BorderSize = 0;
            btnCloseGame.BackColor = Color.Azure;
            btnCloseGame.Text = "Exit Game";
            btnCloseGame.TextAlign = ContentAlignment.MiddleCenter;
            btnCloseGame.Location = new Point(this.Bounds.Right / 2 - btnCloseGame.Width / 2, this.Bounds.Top + 60);
            btnCloseGame.MouseClick += ExitButtonClick;
            panel.Controls.Add(btnCloseGame);
        }

        private void SwitchToGame(object sender, MouseEventArgs e)
        {
            //gamePanel.Visible = true;
            menuPanel.Visible = false;
        }

        private void SwitchToMenu(object sender, MouseEventArgs e)
        {
            menuPanel.Visible = true;
            //gamePanel.Visible = false;
            //administratorPanel.Visible = false;
        }

        private void SwitchToAdministration(object sender, MouseEventArgs e)
        {
            //administratorPanel.Visible = true;
            menuPanel.Visible = false;
        }

        private void AddExitButton(Panel panel)
        {
            Button ExitButton = new Button();
            ExitButton.Size = new Size(100, 25);
            ExitButton.TabStop = false;
            ExitButton.FlatStyle = FlatStyle.Flat;
            ExitButton.FlatAppearance.BorderSize = 0;
            ExitButton.BackColor = Color.LightGray;
            ExitButton.Location = new Point(this.Bounds.Right - ExitButton.Width - 20, this.Bounds.Top + 20);
            //ExitButton.MouseClick += ExitButtonClick;
            ExitButton.MouseClick += SwitchToMenu;
            ExitButton.Text = "Return to menu";
            ExitButton.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(ExitButton);
        }

        private void AddResetButton(Panel panel)
        {
            Button ResetButton = new Button();
            ResetButton.Size = new Size(100, 25);
            ResetButton.TabStop = false;
            ResetButton.FlatStyle = FlatStyle.Flat;
            ResetButton.FlatAppearance.BorderSize = 0;
            ResetButton.BackColor = Color.Red;
            ResetButton.Location = new Point(this.Bounds.Right - ResetButton.Width - 20, this.Bounds.Top + 60);
            ResetButton.MouseClick += SQL.RoundDataCollector;
            ResetButton.MouseClick += ResetButtonClick;
            ResetButton.Text = "Reset Game";
            ResetButton.TextAlign = ContentAlignment.MiddleCenter;
            panel.Controls.Add(ResetButton);
        }

        public void ExitButtonClick(object sender, MouseEventArgs e)
        {
            SQL.SendToDatabase();
            Close();
        }

        private void ResetButtonClick(object sender, MouseEventArgs e)
        {
            foreach (HexagonButton hex in Map.hexMap)
            {
                hex.Visited = false;
                hex.Passable = true;
                hex.Enabled = true;
                hex.BackColor = System.Drawing.Color.LightGray;
                PlaceHexagonButton(hex);
            }
            Map.ResetMouse();
        }

        public void PlaceHexagonButton(HexagonButton button)
        {
            //For at farve midten før man har klikket på skærmen.
            if (button.XCoordinate == Map.TotalHexagonColumns / 2 && button.YCoordinate == Map.TotalHexagonRows / 2)
            {
                button.BackColor = System.Drawing.Color.Aqua;
                button.Enabled = false;
            }

            button.Left = CalculateButtonWidthOffset(button.XCoordinate, button.YCoordinate);
            button.Top = CalculateButtonHeightOffset(button.YCoordinate);
        }

        /// <summary>
        /// Converts a coordinate into a position in a hexgrid.
        /// </summary>
        /// <param name="xCoordinate"></param>
        /// <param name="yCoordinate"></param>
        /// <returns></returns>
        private int CalculateButtonWidthOffset(int xCoordinate, int yCoordinate)
        {
            int width = WidthStart;
            width += (xCoordinate * ButtonWidth);
            //Gives every second button an offset to make the grid
            if (yCoordinate % 2 == 1)
            {
                width += ButtonWidth / 2;
            }
            return width;
        }

        /// <summary>
        /// Converts a coordinate into a position in a hexgrid.
        /// </summary>
        /// <param name="yCoordinate"></param>
        /// <returns></returns>
		private int CalculateButtonHeightOffset(int yCoordinate)
        {
            int height = _heightStart;

            height += (yCoordinate * ButtonHeightOffset);

            return height;
        }

    }
}
