using Server.Data.Games;
using System.Threading.Tasks;

namespace Server.Data.Services.Abstraction
{
    public interface IGameService : IService
    {
        Task<Game> GetGameAsync(int id);

        Task EndGame(int gameId, int winnerId);
    }
}
