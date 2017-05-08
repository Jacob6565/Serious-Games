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
        public Statistics(IFetchData fetch)
        {
            this.fetch = fetch;
        }

        public Array GetDataArray(int sessionID)
        {
            Session session = fetch.GetSessionData(sessionID);
            return new int[10];
        }

    }
}
