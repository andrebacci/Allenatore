using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Rounds
    {
        public Rounds()
        {
            Games = new HashSet<Games>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Games> Games { get; set; }
    }
}
