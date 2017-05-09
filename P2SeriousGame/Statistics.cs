using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2SeriousGame
{
    public class Statistics
    {
        IFetchData fetch;        
        private int SessionID { get; set; }
        public Statistics(IFetchData fetch, Session session)
        {
            this.SessionID = session.SessionID;
            this.fetch = fetch;
        }

        public Array GetDataArray()
        {
            Session session = fetch.GetSessionData(SessionID);
            //session.Rounds[0].IsWin;
            int[] a = new int[2];
            return a;
        }
    }
}
