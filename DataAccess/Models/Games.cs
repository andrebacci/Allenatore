using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Games
    {
        public int Id { get; set; }
        public int IdTeamHome { get; set; }
        public int IdTeamAway { get; set; }
        public int? GolTeamHome { get; set; }
        public int? GolTeamAway { get; set; }
        public int? Round { get; set; }
        public string ModuleHome { get; set; }
        public string ModuleAway { get; set; }

        public virtual Teams IdTeamAwayNavigation { get; set; }
        public virtual Teams IdTeamHomeNavigation { get; set; }
        public virtual Rounds RoundNavigation { get; set; }
    }
}
