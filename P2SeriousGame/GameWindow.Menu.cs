using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P2SeriousGame.SQL;
using P2SeriousGame;

namespace P2SeriousGame
{
    public partial class GameWindow : Form 
    {
        FlowLayoutPanel menuPanel = new FlowLayoutPanel();
        Panel gamePanel = new Panel();
        Panel administratorPanel = new Panel();

        public void DrawWindow(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            InitializePanels();
        }

        private void InitializePanels()
        {
            this.Controls.Add(menuPanel);
            this.Controls.Add(gamePanel);
            this.Controls.Add(administratorPanel);
            gamePanel.Width = ScreenWidth;
            gamePanel.Height = ScreenHeight;
            gamePanel.Visible = false;
            menuPanel.Width = ScreenWidth;
            menuPanel.Height = ScreenHeight;
            administratorPanel.Width = ScreenWidth;
            administratorPanel.Height = ScreenHeight;
            administratorPanel.Visible = false;
            menuPanel.BackColor = Color.BlanchedAlmond;
            menuPanel.FlowDirection = FlowDirection.TopDown;
            menuPanel.Padding = new Padding(Size.Width / 2 - 150, 25, Size.Width / 2 + 150, 25);
            AddExitButton(gamePanel);
            AddResetButton(gamePanel);
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
            gamePanel.Visible = true;
            menuPanel.Visible = false;
        }

        private void SwitchToMenu(object sender, MouseEventArgs e)
        {
            menuPanel.Visible = true;
            gamePanel.Visible = false;
            administratorPanel.Visible = false;
        }

        private void SwitchToAdministration(object sender, MouseEventArgs e)
        {
            administratorPanel.Visible = true;
            menuPanel.Visible = false;
        }
    }
}
