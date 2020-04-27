using AllenatoreAPI.Utils;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Models
{
    public class RoundItemAPI
    {
        public RoundItemAPI()
        {

        }

        public RoundItemAPI(Games game)
        {
            TeamHome = TeamUtility.GetTeamName(game.IdTeamHome);
            TeamAway = TeamUtility.GetTeamName(game.IdTeamAway);

            GolHome = game.GolTeamHome.GetValueOrDefault();
            GolAway = game.GolTeamAway.GetValueOrDefault();
        }

        public string TeamHome { get; set; }

        public string TeamAway { get; set; }

        public int GolHome { get; set; }

        public int GolAway { get; set; }
    }
}
