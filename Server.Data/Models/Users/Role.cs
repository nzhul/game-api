using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Server.Data.Users
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
