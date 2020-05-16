using AllenatoreAPI.Controllers;
using AllenatoreAPI.Utils;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Models
{
    public class GamePlayerAPI : Games
    {
        public GamePlayerAPI(Games g)
        {
            Id = g.Id;

            Round = GameUtility.GetNumber(g.Round.GetValueOrDefault());

            IdTeamHome = g.IdTeamHome;
            IdTeamAway = g.IdTeamAway;

            GolTeamHome = g.GolTeamHome;
            GolTeamAway = g.GolTeamAway;

            TeamHome = TeamUtility.GetTeamName(IdTeamHome);            
            TeamAway = TeamUtility.GetTeamName(IdTeamAway);
        }

        public string TeamHome { get; set; }

        public string TeamAway { get; set; }

        public int? TimeIn { get; set; }

        public int? TimeOut { get; set; }

        public string Info { get; set; }
    }
}
