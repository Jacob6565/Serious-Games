using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace P2SeriousGame
{
    class RoundContext : DbContext
    {
        public DbSet Rounds { get; set; }
    }
}
