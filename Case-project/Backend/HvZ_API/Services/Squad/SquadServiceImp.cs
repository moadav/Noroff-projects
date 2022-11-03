using HvZ_API.Utils.Exceptions;
using HvZ_API.Models;
using Microsoft.EntityFrameworkCore;
using HvZ_API.Contexts;

namespace HvZ_API.Services
{
    public class SquadServiceImp : ISquadService
    {

        private readonly HvZDbEfContext _HvZDbEfContext;
        private readonly ILogger<ChatServiceImp> _logger;

        public SquadServiceImp(HvZDbEfContext hvZDbEfContext, ILogger<ChatServiceImp> logger)
        {
            _HvZDbEfContext = hvZDbEfContext;
            _logger = logger;
        }

        public async Task<Squad> AddAsync(Squad entity)
        {
            if (!await GameExistsAsync(entity.GameId))
            {
                _logger.LogError("Game not found with Id: " + entity.GameId);
                //Throw Exception
                throw new EntityNotFoundException($"Game with id {entity.GameId} not found!");
            }
            entity.MemberCount = 0;
            var mySquad = await _HvZDbEfContext.AddAsync(entity);
            await _HvZDbEfContext.SaveChangesAsync();

            return mySquad.Entity;
        }

        public async Task<bool> GameExistsAsync(int id)
        {
            return await _HvZDbEfContext.Game.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> SquadExistsAsync(int id)
        {
            return await _HvZDbEfContext.Squad.AnyAsync(e => e.Id == id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (!await SquadExistsAsync(id))
            {
                _logger.LogError("Squad not found with Id: " + id);
                //Throw Exception
                throw new EntityNotFoundException($"Squad with id {id} not found!");
            }
            var squad = _HvZDbEfContext.Squad.Find(id);
            _HvZDbEfContext.Squad.Remove(squad);
            await _HvZDbEfContext.SaveChangesAsync();
        }

        public async Task<ICollection<Squad>> GetAllAsync()
        {
            return await _HvZDbEfContext.Squad.ToListAsync();
        }

        public async Task<Squad> GetByIdAsync(int id)
        {
            if (!await SquadExistsAsync(id))
            {
                _logger.LogError("Squad not found with Id: " + id);
                //Throw Exception
                throw new EntityNotFoundException($"Squad with id {id} not found!");

            }

            return await _HvZDbEfContext.Squad
                .Where(c => c.Id == id)
                .FirstAsync();
        }

        public async Task UpdateAsync(int id, Squad entity)
        {

            if (!await SquadExistsAsync(entity.Id))
            {
                _logger.LogError("Squad not found with Id: " + entity.Id);
                //Throw Exception
                throw new EntityNotFoundException($"Squad with id {entity.Id} not found!");
            }else if (!await GameExistsAsync(entity.GameId))
            {
                _logger.LogError("Game not found with Id: " + entity.GameId);
                //Throw Exception
                throw new EntityNotFoundException($"Game with id {entity.GameId} not found!");
            }

            _HvZDbEfContext.Entry(entity).State = EntityState.Modified;
            await _HvZDbEfContext.SaveChangesAsync();
        }

        public async Task<ICollection<Squad>> GetAllFromGameAsync(int gameId)
        {
            if (!await GameExistsAsync(gameId))
            {
                _logger.LogError("Game not found with Id: " + gameId);
                //Throw Exception
                throw new EntityNotFoundException($"Game with id {gameId} not found!");
            }
            return await _HvZDbEfContext.Squad.Where(s => s.GameId == gameId).ToListAsync();
        }

        public async Task AddSquadMemberAsync(int squadId, int playerId)
        {
            if (!await SquadExistsAsync(squadId))
            {
                _logger.LogError("Squad not found with Id: " + squadId);
                //Throw Exception
                throw new EntityNotFoundException($"Squad with id {squadId} not found!");
            }
            else if (!await PlayerExistsAsync(playerId))
            {
                _logger.LogError("Player not found with Id: " + playerId);
                //Throw Exception
                throw new EntityNotFoundException($"Player with id {playerId} not found!");
            }
            var squadList = await _HvZDbEfContext.Player.Where(s => s.SquadId == squadId).ToListAsync();
            var mySquad = await _HvZDbEfContext.Squad.Where(s => s.Id == squadId).FirstAsync();
            if (squadList.Count >= mySquad.Size)
            {
                _logger.LogError("Cannot add Player, squad has exceeded memeber limit!");
                //Throw Exception
                throw new EntityNotFoundException($"Cannot add Player, squad has exceeded memeber limit!");
            }
            Squad squad = await _HvZDbEfContext.Squad.Where(s => s.Id == squadId).FirstAsync();
            Player player = await _HvZDbEfContext.Player.Where(p => p.Id == playerId).FirstAsync();
            player.SquadId = squad.Id;
            squad.MemberCount += 1;
                          
            await _HvZDbEfContext.SaveChangesAsync();


        }

        public async Task RemoveSquadMemberAsync(int SquadId, int playerId)
        {
            if (!await SquadExistsAsync(SquadId))
            {
                _logger.LogError("Squad not found with Id: " + SquadId);
                //Throw Exception
                throw new EntityNotFoundException($"Squad with id {SquadId} not found!");
            }
            else if (!await PlayerExistsAsync(playerId))
            {
                _logger.LogError("Player not found with Id: " + playerId);
                //Throw Exception
                throw new EntityNotFoundException($"Player with id {playerId} not found!");
            }else if (!await PlayerInSquadExistsAsync(SquadId))
            {
                _logger.LogError("Player not does not exist in squad with Id: " + SquadId);
                //Throw Exception
                throw new EntityNotFoundException($"Player does not exist in squad with Id:  {SquadId} !");
            }
            Squad squad = await _HvZDbEfContext.Squad.Where(s => s.Id == SquadId).FirstAsync();
            Player player = await _HvZDbEfContext.Player.Where(p => p.Id == playerId).FirstAsync();
            player.SquadId = null;
            squad.MemberCount -= 1;

            await _HvZDbEfContext.SaveChangesAsync();
            if (squad.MemberCount == 0) await DeleteByIdAsync(squad.Id);

        }
        

        public async Task<ICollection<Player>> GetSquadMembersAsync(int SquadId)
        {
            if (!await SquadExistsAsync(SquadId))
            {
                _logger.LogError("Squad not found with Id: " + SquadId);
                //Throw Exception
                throw new EntityNotFoundException($"Squad with id {SquadId} not found!");
            }


            return await _HvZDbEfContext.Player.Where(p => p.SquadId == SquadId).ToListAsync();
             
        }

        public async Task<bool> PlayerExistsAsync(int id)
        {
           return await _HvZDbEfContext.Player.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> PlayerInSquadExistsAsync(int squadId)
        {
           return await _HvZDbEfContext.Player.AnyAsync(p => p.SquadId == squadId);
        }
    }
}
