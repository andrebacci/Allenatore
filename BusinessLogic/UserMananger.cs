using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserManager
    {
        private readonly string _connectionString;

        public UserManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Ritorna un utente data la login
        public async Task<Users> GetByLogin(string mail, string password)
        {
            using (POContextDb ctx = new POContextDb(_connectionString))
            {
                return await ctx.Users.Where(x => x.Mail == mail && x.Password == password).FirstOrDefaultAsync();
            }
        }
    }
}
