using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Players
    {
        public Players()
        {
            Presences = new HashSet<Presences>();
            Transfers = new HashSet<Transfers>();
        }

        public int Id { get; set; }
        public int? IdTeam { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public int? Age { get; set; }
        public int? Role { get; set; }
        public int? Feet { get; set; }
        public int? LastTeam { get; set; }
        public bool? Penalty { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }

        public virtual Feets FeetNavigation { get; set; }
        public virtual Teams IdTeamNavigation { get; set; }
        public virtual Teams LastTeamNavigation { get; set; }
        public virtual Roles RoleNavigation { get; set; }
        public virtual ICollection<Presences> Presences { get; set; }
        public virtual ICollection<Transfers> Transfers { get; set; }
    }
}
