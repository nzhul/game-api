using Server.Data.Users;
using System.Threading.Tasks;

namespace Server.Data.Services.Abstraction
{
    public interface IUsersService : IService
    {
        Task<User> GetUser(int id);

        /// <summary>
        /// Set the status of the friendship to Rejected
        /// </summary>
        /// <param name="requestId">The id of the Friendship.</param>
        /// <param name="recieverId">The id of the one who is recieving the friend request. Will be used for validation.</param>
        /// <returns>Null if success. Error message on fail</returns>
        Task<string> RejectFriendRequest(int requestId, int recieverId);

        /// <summary>
        /// Sets the user as offline
        /// </summary>
        /// <param name="userId">the id of the user to be set as offline</param>
        /// <returns>Null if success. Error message on fail</returns>
        Task<string> SetOffline(int userId);

        /// <summary>
        /// Sets the user as online
        /// </summary>
        /// <param name="userId">the id of the user to be set as online</param>
        /// <param name="connectionId">the id of the connection in the dedicated server</param>
        /// <returns>Null if success. Error message on fail</returns>
        Task<string> SetOnline(int userId, int connectionId);

        /// <summary>
        /// Clears the current battle from the user. Sets it to NULL
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        Task ClearBattle(int userId);

        // [TEST] Clears all user battles.
        Task ClearAllBattles();
    }
}