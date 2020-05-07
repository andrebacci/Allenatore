using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Presences
    {
        public int Id { get; set; }
        public int IdPlayer { get; set; }
        public int IdGame { get; set; }
        public int? TimeIn { get; set; }
        public int? TimeOut { get; set; }
        public int? IdTeam { get; set; }
        public int? Number { get; set; }

        public virtual Games IdGameNavigation { get; set; }
        public virtual Players IdPlayerNavigation { get; set; }
        public virtual Teams IdTeamNavigation { get; set; }
    }
}
