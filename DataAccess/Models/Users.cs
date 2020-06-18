using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public int? Category { get; set; }

        public virtual UserRoles RoleNavigation { get; set; }
    }
}
