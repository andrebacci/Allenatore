using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Players = new HashSet<Players>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Players> Players { get; set; }
    }
}
