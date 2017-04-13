using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{

    public partial class Handler : Form
    {
        public Handler()
        {
            InitializeComponent();
        }

        private const int _buttonWidth = 100;
        public int ButtonWidth
        {
            get { return _buttonWidth; }
        }

        private const int _buttonHeight = (int)(_buttonWidth * 1.15);
        public int ButtonHeigt
        {
            get { return _buttonHeight; }
        }

        private static int _totalHexagons = 80;
        public int TotalHexagons
        {
            get { return _totalHexagons; }
        }

        private const int _numberOfHexagonsInRow = 10;
        public int NumerOfHexagonsInRow
        {
            get { return _numberOfHexagonsInRow; }
        }

        public void MakeButtons()
        {
            List<HexagonButton> buttons = new List<HexagonButton>();
            for (int i = 0; i < _totalHexagons; i++)
            {
                HexagonButton newButton = new HexagonButton();

                newButton.Size = new Size((int)(1.42 * _buttonHeight), (int)(1.42 * _buttonWidth));
                buttons.Add(newButton);
                this.Controls.Add(newButton);
                newButton.TabStop = false;
                newButton.FlatStyle = FlatStyle.Flat;
                newButton.FlatAppearance.BorderSize = 0;
                newButton.BackColor = Color.LightGray;
                newButton.Paint += ButtonPainter;
            }
            PlaceHexagonButton(buttons);

        }

        private void PlaceHexagonButton(List<HexagonButton> Buttons)
        {
            int count = 0;
            for (int i = 0; i < _totalHexagons / _numberOfHexagonsInRow; i++)
            {
                int left = Buttons[count].Left;
                int top = Buttons[count].Top;
                if (i > 0)
                    top += i * (3 * (_buttonHeight / 4));
                if (i % 2 != 0)
                    left += (int)(_buttonWidth) / 2;
                for (int k = 0; k < _numberOfHexagonsInRow; k++)
                {
                    Buttons[count].Top = top;
                    Buttons[count].Left = left;
                    left += (int)(_buttonWidth);
                    ++count;
                }
            }
        }

        public void ButtonPainter(object sender, PaintEventArgs e)
        {
            System.Drawing.Drawing2D.GraphicsPath buttonPath =
            new System.Drawing.Drawing2D.GraphicsPath();

            Button sender_b = sender as Button;

            System.Drawing.Rectangle newRectangle = sender_b.ClientRectangle;

            e.Graphics.DrawPolygon(Pens.Black, Math.GetPoints(ButtonHeigt, ButtonWidth));

            // Create a hexagon within the new rectangle.
            buttonPath.AddPolygon(Math.GetPoints(ButtonHeigt, ButtonWidth));

            // Set the button's Region property to the newly create

            // Hexagon region.
            sender_b.Region = new System.Drawing.Region(buttonPath);
        }

        /////

        private void Hexagon_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            //this.ClientSize = new System.Drawing.Size(1000, 1000);
            MakeButtons();

        }
    }
}
