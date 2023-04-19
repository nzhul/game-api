using Microsoft.EntityFrameworkCore;
using Server.Data.Pagination;
using Server.Data.Services.Abstraction;
using Server.Data.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Data.Services.Implementation
{
    public class UsersService : BaseService, IUsersService
    {
        public UsersService(DataContext context) : base(context)
        {
        }

        public async Task<User> GetUser(int id)
        {
            // TODO: i should not use this on all places because includes the photos.
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUser(string usernameOrEmail)
        {
            User dbUser = null;

            if (Utilities.IsEmail(usernameOrEmail))
            {
                dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == usernameOrEmail);
            }
            else
            {
                dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == usernameOrEmail);
            }

            return dbUser;
        }

        public async Task<string> RejectFriendRequest(int requestId, int recieverId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SetOffline(int userId)
        {
            User dbUser = await GetUser(userId);

            if (dbUser == null)
            {
                return $"Cannot find user with Id: {userId}";
            }

            dbUser.ActiveConnection = -1;
            dbUser.OnlineStatus = 0;
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<string> SetOnline(int userId, int connectionId)
        {
            User dbUser = await GetUser(userId);

            if (dbUser == null)
            {
                return $"Cannot find user with Id: {userId}";
            }

            dbUser.ActiveConnection = connectionId;
            dbUser.OnlineStatus = 1;
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task ClearBattle(int userId)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (dbUser != null)
            {
                dbUser.BattleId = null;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearAllBattles()
        {
            var users = _context.Users.Where(x => x.BattleId != null);
            foreach (var user in users)
            {
                user.BattleId = null;
            }

            await _context.SaveChangesAsync();
        }
    }
}