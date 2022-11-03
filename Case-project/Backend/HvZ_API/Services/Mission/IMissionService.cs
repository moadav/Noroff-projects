using HvZ_API.Models;

namespace HvZ_API.Services
{
    public interface IMissionService : IGameComponentService<Mission, int>
    {
        Task<bool> MissionExistsAsync(int id);
    }
}
