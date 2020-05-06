using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    }
}
