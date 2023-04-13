﻿using Server.Common.Mappings;
using Server.Models.Users;
using System;

namespace Server.Application.Features.Common.Models
{
    public class UserListDto : IMapFrom<User>
    {
        public int Id { get; set; }

        public int MMR { get; set; }

        public string Username { get; set; }

        public int CurrentRealmId { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PhotoUrl { get; set; }

        public int GameId { get; set; }

        public Guid? BattleId { get; set; }
    }
}
