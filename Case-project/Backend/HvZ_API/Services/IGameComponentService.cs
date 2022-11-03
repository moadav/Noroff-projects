namespace HvZ_API.Services
{
    public interface IGameComponentService<T, GAMEID> : ICrudService<T, GAMEID>
    {
        /// <summary>
        /// Get all instances of an entity.
        /// </summary>
        /// <param name="gameId">ID of the game</param>
        /// <returns>A collection of entites belonging to a game</returns>
        Task<ICollection<T>> GetAllFromGameAsync(GAMEID gameId);

        Task<bool> GameExistsAsync(int id);

    }
}
