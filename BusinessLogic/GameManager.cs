using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class GameManager
    {
        private readonly string _connectionString;

        public GameManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Ritorna tutte le partite per una determinata giornata
        public async Task<List<Games>> GetByRound(int round)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Games.Where(x => x.Round.GetValueOrDefault() == round).ToListAsync();
            }
        }

        // Ritorna una partita dato il suo id
        public async Task<Games> GetById(int id)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Games.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
        }
    }
}
