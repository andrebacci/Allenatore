using AllenatoreAPI.Utils;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllenatoreAPI.Models
{
    public class TeamAPI
    {
        public TeamAPI(Teams team)
        {
            Id = team.Id;
            Name = team.Name;
            City = team.City;
            Mister = team.Mister;
            IdCategory = team.IdCategory.GetValueOrDefault();
            Category = CategoryUtility.GetCategoryName(IdCategory);
            Logo = team.Logo;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Mister { get; set; }

        public string Category { get; set; }

        public string Logo { get; set; }

        public int IdCategory { get; set; }

        public TeamStatisticsAPI Statistics { get; set; }
    }
}
