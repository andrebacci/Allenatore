using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class RoundManager
    {
        private readonly string _connectionString;

        public RoundManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Ritorna l'ultima giornata giocata
        public async Task<Rounds> GetLast()
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Rounds.Where(x => x.Date <= DateTime.Now).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            }
        }

        // Ritorna la prossima giornata da giocare
        public async Task<Rounds> GetNext()
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Rounds.Where(x => x.Date >= DateTime.Now).OrderBy(x => x.Date).FirstOrDefaultAsync();
            }
        }

        // Ritorna la giornata dato il suo number
        public async Task<Rounds> GetByNumber(int number)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Rounds.Where(x => x.Id == number).FirstOrDefaultAsync();
            }
        }
    }
}
