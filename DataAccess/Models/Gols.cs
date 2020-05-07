using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Gols
    {
        public int Id { get; set; }
        public int IdPlayer { get; set; }
        public int IdTeam { get; set; }
        public int IdGame { get; set; }
        public bool? IsPenalby { get; set; }
        public int? Minute { get; set; }
        public string Details { get; set; }

        public virtual Games IdGameNavigation { get; set; }
        public virtual Players IdPlayerNavigation { get; set; }
        public virtual Teams IdTeamNavigation { get; set; }
    }
}
