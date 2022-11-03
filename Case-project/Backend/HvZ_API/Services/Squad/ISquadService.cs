using HvZ_API.Models;

namespace HvZ_API.Services
{
    public interface ISquadService : IGameComponentService<Squad, int>
    {

        /// <summary>Checks if Squad exists asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>returns a bool value of wether it exists</returns>
        Task<bool> SquadExistsAsync(int id);
        /// <summary>Adds player to a squad.</summary>
        /// <param name="SquadId">The identifier.</param>
        /// <returns>nothing</returns>
        Task AddSquadMemberAsync(int SquadId, int playerId);
        /// <summary>Get all players on a squad.</summary>
        /// <param name="SquadId">The identifier.</param>
        /// <returns>List of Players</returns>
        Task<ICollection<Player>> GetSquadMembersAsync(int SquadId);

        Task<bool> PlayerExistsAsync(int id);
        Task<bool> PlayerInSquadExistsAsync(int squadId);
        Task RemoveSquadMemberAsync(int SquadId, int playerId);


    }
}
