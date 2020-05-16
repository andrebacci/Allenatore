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
        public GameAPI(Games g, bool createFormation)
        {
            Id = g.Id;

            IdTeamHome = g.IdTeamHome;
            IdTeamAway = g.IdTeamAway;

            GolTeamHome = g.GolTeamHome;
            GolTeamAway = g.GolTeamAway;

            Round = g.Round;
            RoundNumber = GameUtility.GetNumber(g.Round.GetValueOrDefault());

            ModuleHome = g.ModuleHome;
            ModuleAway = g.ModuleAway;

            TeamHome = TeamUtility.GetTeamName(IdTeamHome);            
            TeamAway = TeamUtility.GetTeamName(IdTeamAway);

            if (createFormation)
            {
                PlayersHome = PlayerUtility.GetFormation(g.Id, g.IdTeamHome);
                PlayersAway = PlayerUtility.GetFormation(g.Id, g.IdTeamAway);
            }
        }

        public int RoundNumber { get; set; }

        public string TeamHome { get; set; }

        public string TeamAway { get; set; }

        public List<PlayerGameAPI> PlayersHome { get; set; }

        public List<PlayerGameAPI> PlayersAway { get; set; }
    }
}
