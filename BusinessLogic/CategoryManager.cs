using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CategoryManager
    {
        private readonly string _connectionString;

        public CategoryManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Ritorna un elemento dato il suo id
        public async Task<Category> GetById(int id)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Category.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
        }
    }
}
