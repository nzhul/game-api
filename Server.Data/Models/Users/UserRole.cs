using Microsoft.AspNetCore.Identity;

namespace Server.Data.Users
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }

        public Role Role { get; set; }
    }
}
