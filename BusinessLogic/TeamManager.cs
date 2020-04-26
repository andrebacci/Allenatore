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

        // Ritorna una squadra dato il suo id
        public async Task<Teams> Get(int id)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Teams.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
        }

        // Ritorna una squadra dato il suo nome
        public async Task<Teams> Get(string name)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Teams.Where(x => x.Name == name).FirstOrDefaultAsync();
            }
        }

        // Ritorna la lista di tutte le squadre
        public async Task<List<Teams>> GetAll()
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Teams.OrderBy(x => x.Name).ToListAsync();
            }
        }

        // Ritorna il nome della squadra dato il suo id
        public async Task<string> GetNameById(int id)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Teams.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefaultAsync();
            }
        }

        // Ritorna una lista con i nomi di tutte le squadre
        public async Task<List<string>> GetNameTeams()
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Teams.OrderBy(x => x.Name).Select(x => x.Name).ToListAsync();
            }
        }

        // Aggiunge una nuova squadra
        public async Task<Teams> Insert(Teams team)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                await ctx.Teams.AddAsync(team);
                await ctx.SaveChangesAsync();
            }

            return await Get(team.Id);
        }

        // Aggiorna una squadra esistente
        public async Task<Teams> Update(Teams team)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                ctx.Teams.Update(team);
                await ctx.SaveChangesAsync();
            }

            return await Get(team.Id);
        }
    }
}
