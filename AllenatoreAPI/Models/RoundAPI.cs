using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace AllenatoreAPI.Models
{
    public class RoundAPI
    {
        public RoundAPI()
        {
            Games = new List<RoundItemAPI>();
        }

        public RoundAPI(Rounds round)
        {
            Number = round.Number.GetValueOrDefault();
            Date = round.Date;

            Games = new List<RoundItemAPI>();
        }

        public int Number { get; set; }

        public DateTime Date { get; set; }

        public List<RoundItemAPI> Games { get; set; }
    }
}