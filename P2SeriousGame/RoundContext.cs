using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace P2SeriosuGame
{
    class RoundContext : DbContext
    {
        public DbSet Rounds { get; set; }
    }
}
