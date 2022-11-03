using HvZ_API.Models;

namespace HvZ_API.Services
{
    public interface IPlayerService : IGameComponentService<Player,int>
    {
        Task<bool> SquadExistsAsync(int id);
        Task<ICollection<Player>> GetSquadCheckInsAsync(int squadId);
        Task<bool> PlayerExistsAsync(int id);
        Task<Player> GetCheckInAsync(int CheckIn);
        Task<Player> CreateCheckInForPlayerAsync(int playerId, Player s);
        string generateBiteCode(string s);

    }
}
