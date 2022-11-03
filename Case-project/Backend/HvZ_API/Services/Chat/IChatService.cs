using HvZ_API.Models;

namespace HvZ_API.Services
{
    public interface IChatService : IGameComponentService<Chat,int>
    {
        /// <summary>Checks if Chat exists asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>returns a bool value of wether it exists</returns>
        Task<bool> ChatExistsAsync(int id);
        Task<ICollection<Chat>> GetAllSquadMessageAsync(int gameId, int squadId);
        Task<ICollection<Chat>> GetAllGlobalMessageAsync(int gameId);
        Task<ICollection<Chat>> GetAllLocalZombieMessageAsync(int gameId);
        Task<ICollection<Chat>> GetAllLocalHumanMessageAsync(int gameId);
        Task<bool> SquadExistsAsync(int id);
        Task<bool> PlayerExistsAsync(int id);



    }
}
