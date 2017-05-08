using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2SeriosuGame
{
	interface IFetchData
	{
		Session GetSessionData(int SessionID);
	}
}
