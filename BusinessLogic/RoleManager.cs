using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class RoleManager
    {
        private readonly string _connectionString;

        public RoleManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Ritorna tutti gli elementi
        public async Task<List<Roles>> GetAll()
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Roles.ToListAsync();
            }
        }

        // Ritorna la description di un ruolo
        public async Task<string> GetDescriptionById(int id)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Roles.Where(x => x.Id == id).Select(x => x.Description).FirstOrDefaultAsync();
            }
        }
    }
}
