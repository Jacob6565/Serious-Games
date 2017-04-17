using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public class HexagonButton : Button
    {
        private bool _passable = true;
        public bool Passable
        {
            get { return _passable; }
            set { _passable = value; }
        }
        private bool _showImage = false;
        public bool ShowImage
        {
            get
            {
                return _showImage;
            }
            set
            {
                _showImage = value;
            }
        }
        public void NotPassable(object sender, MouseEventArgs e)
        {
            HexagonButton sender_Button = sender as HexagonButton;
            sender_Button.BackColor = Color.Black;
            sender_Button.Enabled = false;
            sender_Button.Passable = false;
        }
    }
}
