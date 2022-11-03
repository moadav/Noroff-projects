using HvZ_API.Models;
namespace HvZ_API.Services
{
    public interface IGameConfigService : ICrudService<Models.GameConfig, int>
    {
        /// <summary>
        /// Check if gameconfig exists
        /// </summary>
        /// <param name="id">ID of the config</param>
        /// <returns>Returns true if config exists in DB</returns>
        Task<bool> GameConfigExistsAsync(int id);
    }
}
