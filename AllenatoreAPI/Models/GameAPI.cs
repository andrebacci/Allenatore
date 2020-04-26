using AllenatoreAPI.Utils;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Models
{
    public class GameAPI : Games
    {
        public GameAPI(Games g)
        {
            Id = g.Id;

            IdTeamHome = g.IdTeamHome;
            IdTeamAway = g.IdTeamAway;

            GolTeamHome = g.GolTeamHome;
            GolTeamAway = g.GolTeamAway;

            Date = g.Date;
            Round = g.Round;

            ModuleHome = g.ModuleHome;
            ModuleAway = g.ModuleAway;

            TeamHome = TeamUtility.GetTeamName(IdTeamHome);            
            TeamAway = TeamUtility.GetTeamName(IdTeamAway);
        }

        public string TeamHome { get; set; }

        public string TeamAway { get; set; }
    }
}
