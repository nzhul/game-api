using Server.Common.Mappings;
using Server.Data.Users;
using System;

namespace Server.Application.Features.Users.Models
{
    public class UserListDto : IMapFrom<User>
    {
        public int Id { get; set; }

        public int MMR { get; set; }

        public string Username { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime LastActive { get; set; }

        public int GameId { get; set; }

        public Guid? BattleId { get; set; }
    }
}
