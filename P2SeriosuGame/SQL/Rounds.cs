//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace P2SeriosuGame.SQL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rounds
    {
        public Rounds()
        {
            this.PersonRoundsTestParametre = new HashSet<PersonRoundsTestParametre>();
        }
    
        public int IdRounds { get; set; }
    
        public virtual ICollection<PersonRoundsTestParametre> PersonRoundsTestParametre { get; set; }
    }
}
