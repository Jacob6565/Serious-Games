using P2SeriosuGame.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2SeriousGame
{
    class GetRoundsFromDatabase
    {
        public GetRoundsFromDatabase()
        {
            using (var context = new Entities())
            {
                foreach (var item in context.Rounds)
                {
                    Round round = new Round(item.Id, float.Parse(item.Clicks.ToString()), float.Parse(item.AVG_Clicks.ToString()), int.Parse(item.Win.ToString()), float.Parse(item.Time_Used.ToString()));
                }
            }
        }

        //public override string ToString()
        //{
        //    return 
        //}
    }
}
