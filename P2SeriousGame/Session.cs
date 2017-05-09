using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2SeriousGame
{
    public class Session
    {
        public int SessionID {get; set;}
        public List<Round> Rounds = new List<Round>();       
	}
}
