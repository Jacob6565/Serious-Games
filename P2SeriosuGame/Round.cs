using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2SeriousGame
{
    public class Round
    {       
        public double ClicksPerMinute { get; set; }
        public int NumberOfClicks { get; set; }
        public bool IsWin { get; set; }

        private DateTime[] timeBetweenClicks = new DateTime[50];
        public DateTime[] TimeBetweenClicks
        {
            get
            {
                return timeBetweenClicks;
            }
            set
            {
                timeBetweenClicks = value;
            }
        }
    }
}
