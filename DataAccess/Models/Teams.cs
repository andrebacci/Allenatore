﻿using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Teams
    {
        public Teams()
        {
            GamesIdTeamAwayNavigation = new HashSet<Games>();
            GamesIdTeamHomeNavigation = new HashSet<Games>();
            PlayersIdTeamNavigation = new HashSet<Players>();
            PlayersLastTeamNavigation = new HashSet<Players>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Mister { get; set; }
        public string Category { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<Games> GamesIdTeamAwayNavigation { get; set; }
        public virtual ICollection<Games> GamesIdTeamHomeNavigation { get; set; }
        public virtual ICollection<Players> PlayersIdTeamNavigation { get; set; }
        public virtual ICollection<Players> PlayersLastTeamNavigation { get; set; }
    }
}
