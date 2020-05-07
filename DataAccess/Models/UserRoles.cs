using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class UserRoles
    {
        public UserRoles()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
