using HvZ_API.Contexts;
using HvZ_API.Models;
using HvZ_API.Utils.Exceptions;
using Microsoft.EntityFrameworkCore;
using HvZ_API.Services;

namespace HvZ_API.Services
{
    public class MissionsServiceImp : IMissionService
    {
        private readonly HvZDbEfContext _context;
        private readonly ILogger<ChatServiceImp> _logger;
        public MissionsServiceImp(HvZDbEfContext context, ILogger<ChatServiceImp> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Mission> AddAsync(Mission entity)
        {
            if (!await GameExistsAsync(entity.GameId))
            {
                _logger.LogError("Mission not found with Id: " + entity.GameId);
                //Throw Exception
                throw new EntityNotFoundException($"Mission with id {entity.GameId} not found!");
            }
            var mission = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return mission.Entity;
        }


        public async Task DeleteByIdAsync(int id)
        {
            if (!await MissionExistsAsync(id))
            {
                _logger.LogError("Mission not found with Id: " + id);
                //Throw Exception
                throw new EntityNotFoundException($"Mission with id {id} not found!");
            }
            var mission = await _context.Mission.Where(m => m.Id == id).FirstAsync();
            _context.Mission.Remove(mission);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Mission>> GetAllAsync()
        {
            return await _context.Mission.ToListAsync();
        }

        public async Task<ICollection<Mission>> GetAllFromGameAsync(int gameId)
        {
            if (!await GameExistsAsync(gameId))
            {
                _logger.LogError("Game not found with Id: " + gameId);
                //Throw Exception
                throw new EntityNotFoundException($"Game with id {gameId} not found!");
            }
            return await _context.Mission.Where(m => m.GameId == gameId).ToListAsync();
        }

        public async Task<Mission> GetByIdAsync(int id)
        {
            if (!await MissionExistsAsync(id))
            {
                _logger.LogError("Mission not found with Id: " + id);
                //Throw Exception
                throw new EntityNotFoundException($"Mission with id {id} not found!");
            }

            return await _context.Mission.Where(m => m.Id == id).FirstAsync();
        }

        public async Task<bool> MissionExistsAsync(int id)
        {
            return await _context.Mission.AnyAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(int id, Mission entity)
        {
            if (!await GameExistsAsync(id))
            {
                _logger.LogError("Mission not found with Id: " + id);
                //Throw Exception
                throw new EntityNotFoundException($"Mission with id {id} not found!");
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> GameExistsAsync(int id)
        {
            return await _context.Game.AnyAsync(m => m.Id == id);

        }
    }
}
