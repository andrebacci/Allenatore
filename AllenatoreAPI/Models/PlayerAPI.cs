using AllenatoreAPI.Controllers;
using AllenatoreAPI.Utils;
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

            FeetString = Constant.FeetToString(p.Feet.GetValueOrDefault());

            RoleString = RoleUtility.GetRoleDescription(p.Role.GetValueOrDefault());

            LastTeamString = TeamUtility.GetLastTeamName(p.IdTeam.GetValueOrDefault(), p.LastTeam.GetValueOrDefault());
            Team = TeamUtility.GetTeamName(p.IdTeam.GetValueOrDefault());

            PenaltyString = p.Penalty.GetValueOrDefault() ? "Sì" : "No";

            Fullname = Lastname + " " + Firstname;
        }

        public string Fullname { get; set; }

        public string FeetString { get; set; }

        public string RoleString { get; set; }

        public string LastTeamString { get; set; }

        public string Team { get; set; }

        public string PenaltyString { get; set; }
    }
}
