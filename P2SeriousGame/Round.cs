using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2SeriousGame
{
    public class Round
    {       
        public float NumberOfClicks { get; set; }
        public float ClicksPerMinute { get; set; }
        public int Win { get; set; }
        public int Loss { get; set; }
        public float TimeUsed { get; set; }

        public Round(float clicks, float clicksAVG, int win, float timeUsed)
        {
            this.NumberOfClicks = clicks;
            this.ClicksPerMinute = clicksAVG;
            WinOrLoss(win);
            this.TimeUsed = timeUsed;
        }

        public void WinOrLoss(int win)
        {
            if (win == 1)
            {
                Win = 1;
                Loss = 0;
            }
            else
            {
                Win = 0;
                Loss = 1;
            }
        }
        
        //private DateTime[] timeBetweenClicks = new DateTime[50];
        //public DateTime[] TimeBetweenClicks
        //{
        //    get
        //    {
        //        return timeBetweenClicks;
        //    }
        //    set
        //    {
        //        timeBetweenClicks = value;
        //    }
        //}
    }
}
