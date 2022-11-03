using HvZ_API.Models;
using HvZ_API.Models.DTOs.Gravestone;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HvZ_API.Services
{
    public interface IGameService : ICrudService<Game, int>
    {
        /// <summary>Checks if Game exists asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>returns a bool value of wether it exists</returns>
        Task<bool> GameExistsAsync(int id);
        /// <summary>Gets a list of all gravestones in a game asynchronous.</summary>
        /// <param name="playerId">The current player identifier.</param>
        /// <param name="gameId">The current game identifier.</param>
        /// <returns>returns a list of gravestone objects</returns>
        Task<Gravestone> GetVictimstone(int gameId, int playerId);
        Task<bool> GameConfigExistsAsync(int id);
        Task<Gravestone> CreateKillerStone(GravestoneKillPostDto kill);
        Task<Gravestone> CreateEmptyKillerStone(GravestoneKillPostDto kill);
        Task<ICollection<Player>> GetAllVictims(int gameId);
        Task<bool> PlayerExistsAsync(int id);
        Task<int> GetAllPlayerKillCount(int gameId, int playerId);
        Task<ICollection<Gravestone>> GetAllVictimstones(int gameId);
        Task<ICollection<Player>> GetAllZombiesInGame(int gameId);
        Task<Player> GetOneZombieInGame(int gameId, int playerId);

        Task<ICollection<Player>> GetAllPatientZeroInGame(int gameId);
        Task<ICollection<Player>> GetAllHumansInGame(int gameId);
        Task<Player> GetOneHumanInGame(int gameId, int playerId);
    }

}
