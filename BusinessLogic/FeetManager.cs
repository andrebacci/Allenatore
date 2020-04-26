using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class FeetManager
    {
        private readonly string _connectionString;

        public FeetManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Ritorna tutti gli elementi
        public async Task<List<Feets>> GetAll()
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Feets.ToListAsync();
            }
        }
    }
}
