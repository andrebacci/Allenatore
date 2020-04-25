using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Models
{
    public class PlayerAPI : Players
    {
        public PlayerAPI(Players p)         
        {
            Id = p.Id;
            IdTeam = p.IdTeam;
            Lastname = p.Lastname;
            Firstname = p.Firstname;
            Age = p.Age;
            Role = p.Role;
            Feet = p.Feet;
            LastTeam = p.LastTeam;
            Details = p.Details;
            Image = p.Image;
            Penalty = p.Penalty;
        }

        public string FeetString { get; set; }

        public string RoleString { get; set; }

        public string LastTeamString { get; set; }        
    }
}
