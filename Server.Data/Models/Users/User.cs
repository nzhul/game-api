using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Server.Data.Users
{
    // TODO: Move this into data layer. When everything is moved > delete Server.Data.
    public class User : IdentityUser<int>
    {
        public int MMR { get; set; }

        public string Discriminator { get; set; }

        public DateTime LastActive { get; set; }

        public virtual ICollection<Message> MessagesSent { get; set; }

        public virtual ICollection<Message> MessagesRecieved { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<Friendship> SendFriendRequests { get; set; }

        public virtual ICollection<Friendship> RecievedFriendRequests { get; set; }

        public int ActiveConnection { get; set; }

        public Guid? BattleId { get; set; }

        public int? GameId { get; set; }

        public byte OnlineStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public User()
        {
            MessagesSent = new Collection<Message>();
            MessagesRecieved = new Collection<Message>();
            SendFriendRequests = new Collection<Friendship>();
            RecievedFriendRequests = new Collection<Friendship>();
        }
    }
}
