using AllenatoreAPI.Utils;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Models
{
    public class TransferAPI : Transfers
    {
        public TransferAPI(Transfers t)
        {
            Id = t.Id;

            IdPlayer = t.IdPlayer;

            IdTeamNew = t.IdTeamNew;
            IdTeamOld = t.IdTeamOld;

            Date = t.Date;

            TeamNew = TeamUtility.GetTeamName(IdTeamNew.GetValueOrDefault());
            TeamOld = TeamUtility.GetTeamName(IdTeamOld.GetValueOrDefault());

            Player = PlayerUtility.GetPlayerName(IdPlayer);
        }

        public string Player { get; set; }

        public string TeamNew { get; set; }

        public string TeamOld { get; set; }
    }
}
