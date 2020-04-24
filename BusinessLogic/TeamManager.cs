using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class TeamManager
    {
        private readonly string _connectionString;

        public TeamManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Ritorna un elemento dato il suo id
        public async Task<Team> Get(int id)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Teams.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
        }

        // Aggiunge un nuovo elemento
        public async Task<Team> Insert(Team team)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                await ctx.Teams.AddAsync(team);
                await ctx.SaveChangesAsync();
            }

            return await Get(team.Id);
        }
    }
}
