using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UtilityManager
    {
        private readonly string _connectionString;

        public UtilityManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Ritorna tutti gli elementi
        public async Task<int> Truncate(string tableName)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Database.ExecuteSqlCommandAsync($"TRUNCATE TABLE {{tableName}}");
            }
        }
    }
}
