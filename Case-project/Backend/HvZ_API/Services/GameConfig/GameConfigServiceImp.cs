using HvZ_API.Contexts;
using HvZ_API.Utils.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HvZ_API.Services
{
    public class GameConfigServiceImp : IGameConfigService
    {
        private readonly HvZDbEfContext _HvZDbEfContext;
        private readonly ILogger<ChatServiceImp> _logger;

        public GameConfigServiceImp(HvZDbEfContext hvZDbEfContext, ILogger<ChatServiceImp> logger)
        {
            _HvZDbEfContext = hvZDbEfContext;
            _logger = logger;
        }

        public async Task<Models.GameConfig> AddAsync(Models.GameConfig entity)
        {
 
            var Game = await _HvZDbEfContext.AddAsync(entity);
            await _HvZDbEfContext.SaveChangesAsync();

            return Game.Entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (!await GameConfigExistsAsync(id))
            {
                _logger.LogError("Game not found with Id: " + id);

                throw new EntityNotFoundException($"Game with {id} cannot be deleted");
            }
            else
            {
                var config = _HvZDbEfContext.GameConfig.Find(id);
                _HvZDbEfContext.GameConfig.Remove(config);
                await _HvZDbEfContext.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Models.GameConfig>> GetAllAsync()
        {
            return await _HvZDbEfContext.GameConfig.ToListAsync();
        }

        public async Task<Models.GameConfig> GetByIdAsync(int id)
        {
            if (!await GameConfigExistsAsync(id))
            {
                _logger.LogError("GameConfig not found with Id: " + id);

                throw new EntityNotFoundException($"GameConfig with id {id} not found!");

            }

            return await _HvZDbEfContext.GameConfig
                .Where(g => g.Id == id)
                .FirstAsync();
        }

        public async Task UpdateAsync(int id, Models.GameConfig entity)
        {
            if (!await GameConfigExistsAsync(entity.Id))
            {
                throw new EntityNotFoundException($"Game with id {entity.Id} not found!");
            }
            if (entity.Id != id)
            {
                throw new BadRequestException($"Game with id {entity.Id} Does not match!");
            }
            _HvZDbEfContext.Entry(entity).State = EntityState.Modified;
            await _HvZDbEfContext.SaveChangesAsync();
        }

        public async Task<bool> GameConfigExistsAsync(int id)
        {
            return await _HvZDbEfContext.GameConfig.AnyAsync(g => g.Id == id);
        }

    }
}
