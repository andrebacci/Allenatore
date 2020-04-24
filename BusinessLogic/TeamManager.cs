using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        // Ritorna la lista di tutti gli elementi
        public async Task<List<Team>> GetAll()
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Teams.OrderBy(x => x.Name).ToListAsync();
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
