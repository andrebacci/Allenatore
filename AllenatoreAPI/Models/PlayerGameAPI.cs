using AllenatoreAPI.Controllers;
using AllenatoreAPI.Utils;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Models
{
    public class PlayerGameAPI
    {
        public PlayerGameAPI()
        {
            
        }

        public int Id { get; set; }

        public string Fullname { get; set; }
        
        public int? YellowCards { get; set; }

        public int? RedCards { get; set; }

        public int? Gols { get; set; }

        public int? ChangeIn { get; set; }

        public int? ChangeOut { get; set; }

        public int Number { get; set; }
    }
}
