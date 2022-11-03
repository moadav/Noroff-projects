using HvZ_API.Contexts;
using HvZ_API.Models;
using HvZ_API.Models.DTOs.Game;
using HvZ_API.Models.DTOs.Gravestone;
using HvZ_API.Utils.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace HvZ_API.Services
{
    public class GameServiceImp : IGameService
    {
        private readonly HvZDbEfContext _HvZDbEfContext;
        private readonly ILogger<ChatServiceImp> _logger;
        public GameServiceImp(HvZDbEfContext hvZDbEfContext, ILogger<ChatServiceImp> logger)
        {
            _HvZDbEfContext = hvZDbEfContext;
            _logger = logger;
        }

        public async Task<Game> AddAsync(Game entity)
        {

            if (!await GameConfigExistsAsync(entity.GameConfigId))
            {
                _logger.LogError("GameConfig not found with Id: " + entity.GameConfigId);

                throw new EntityNotFoundException($"GameConfig with {entity.GameConfigId} cannot be found");
            }
            var Game = await _HvZDbEfContext.AddAsync(entity);
            await _HvZDbEfContext.SaveChangesAsync();

            return Game.Entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (!await GameExistsAsync(id))
            {
                _logger.LogError("Game not found with Id: " + id);

                throw new EntityNotFoundException($"Game with {id} cannot be deleted");
            }
            else
            {
                var game = _HvZDbEfContext.Game.Find(id);
                _HvZDbEfContext.Game.Remove(game);
                await _HvZDbEfContext.SaveChangesAsync();
            }

        }

        public async Task<ICollection<Game>> GetAllAsync()
        {
            return await _HvZDbEfContext.Game.ToListAsync();
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            if (!await GameExistsAsync(id))
            {
                _logger.LogError("Game not found with Id: " + id);

                throw new EntityNotFoundException($"Game with id {id} not found!");

            }

            return await _HvZDbEfContext.Game
                .Where(c => c.Id == id)
                .FirstAsync();
        }

        public async Task<bool> GameExistsAsync(int id)
        {
            return await _HvZDbEfContext.Game.AnyAsync(g => g.Id == id);
        }


        public async Task UpdateAsync(int id, Game entity)
        {
            if (!await GameExistsAsync(entity.Id))
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

        private async Task UpdatePlayerToZombie(int playerId)
        {
            var player = await _HvZDbEfContext.Player.Where(p => p.Id == playerId).FirstAsync();

        }
        public async Task<Gravestone> CreateKillerStone(GravestoneKillPostDto kill)
        {
            
            var playerDead = await _HvZDbEfContext.Player.Where(p => p.BiteCode == kill.BiteCode).FirstAsync();
            Gravestone grave = new Gravestone { Description = kill.Description, KillerId = kill.KillerId, Lat = kill.Lat, Lng = kill.Lng, VictimId = playerDead.Id, GameId = kill.GameId };
            if (playerDead == null)
            {
                throw new EntityNotFoundException($"Player with bitecode" + kill.BiteCode + "not found!");

            }

            var victim = await _HvZDbEfContext.Gravestone.AnyAsync(s => s.VictimId == grave.VictimId);

            if (!await GameExistsAsync(grave.GameId))
            {
                throw new EntityNotFoundException($"Game with id {grave.GameId} not found!");
            }
            else if (kill.KillerId == grave.VictimId)
            {
                throw new EntityNotFoundException($"Player cannot kill itself!");
            }
            else if (victim)
            {
                var grave2 = _HvZDbEfContext.Gravestone.Where(s => s.VictimId == grave.VictimId).First();
                _HvZDbEfContext.Gravestone.Remove(grave2);
                await _HvZDbEfContext.SaveChangesAsync();
            }
            await UpdatePlayerStatus(grave);
            await _HvZDbEfContext.AddAsync(grave);
            await _HvZDbEfContext.SaveChangesAsync();

            return grave;

        }
        public async Task<Gravestone> CreateEmptyKillerStone(GravestoneKillPostDto kill)
        {

            
            Gravestone grave = new Gravestone { Description = kill.Description, KillerId = kill.KillerId, Lat = kill.Lat, Lng = kill.Lng, VictimId = null, GameId = kill.GameId };

            if (!await GameExistsAsync(grave.GameId))
            {
                throw new EntityNotFoundException($"Game with id {grave.GameId} not found!");
            }

            //await UpdatePlayerStatus(grave);
            var graveResponse = await _HvZDbEfContext.AddAsync(grave);
            await _HvZDbEfContext.SaveChangesAsync();

            return graveResponse.Entity;

        }
        public async Task<ICollection<Player>> GetAllVictims(int gameId)
        {
            if (!await GameExistsAsync(gameId))
            {
                throw new EntityNotFoundException($"Game with id {gameId} not found!");
            }
            return _HvZDbEfContext.Player.Where(i => i.VictimId != null && _HvZDbEfContext.Gravestone.Any(i => i.GameId == gameId)).ToList();


        }

        public async Task<int> GetAllPlayerKillCount(int gameId, int playerId)
        {
            if (!await GameExistsAsync(gameId))
            {
                throw new EntityNotFoundException($"Game with id {gameId} not found!");
            }
            else if (!await PlayerExistsAsync(playerId))
            {
                throw new EntityNotFoundException($"Player with id {playerId} not found!");
            }

            return _HvZDbEfContext.Gravestone.Where(g => g.KillerId == playerId && g.GameId == gameId).ToList().Count();
        }

        private async Task UpdatePlayerStatus(Gravestone gravestone)
        {
            var playerDead = await _HvZDbEfContext.Player.FindAsync(gravestone.VictimId);
            playerDead.IsHuman = false;
            playerDead!.VictimId = gravestone.VictimId;
            _HvZDbEfContext.Entry(playerDead).State = EntityState.Modified;
            await _HvZDbEfContext.SaveChangesAsync();

        }

        public async Task<bool> PlayerExistsAsync(int id)
        {
            return await _HvZDbEfContext.Player.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> GameConfigExistsAsync(int id)
        {
            return await _HvZDbEfContext.GameConfig.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> GraveStoneExistsAsync(int id)
        {
            return await _HvZDbEfContext.Gravestone.AnyAsync(e => e.Id == id);
        }


        public async Task<Gravestone> GetVictimstone(int gameId, int playerId)
        {
            if (!await PlayerExistsAsync(playerId))
            {
                _logger.LogError("Player not found with Id: " + playerId);

                throw new EntityNotFoundException($"Player with id {playerId} not found!");

            }
            else if (!await GameExistsAsync(gameId))
            {
                _logger.LogError("Game not found with Id: " + gameId);

                throw new EntityNotFoundException($"Game with id {gameId} not found!");
            }
            var player = await _HvZDbEfContext.Player.Where(p => p.Id == playerId).FirstAsync();
            var victimGravestone = await _HvZDbEfContext.Gravestone.Where(g => g.VictimId == player.VictimId).FirstAsync();


            return victimGravestone;
        }

        public async Task<ICollection<Gravestone>> GetAllVictimstones(int gameId)
        {
            if (!await GameExistsAsync(gameId))
            {
                _logger.LogError("Game not found with Id: " + gameId);

                throw new EntityNotFoundException($"Game with id {gameId} not found!");

            }

            return _HvZDbEfContext.Gravestone.Where(i => i.GameId == gameId).ToList();

        }

        public async Task<ICollection<Player>> GetAllZombiesInGame(int gameId)
        {
            if (!await GameExistsAsync(gameId))
            {
                _logger.LogError("Game not found with Id: " + gameId);

                throw new EntityNotFoundException($"Game with id {gameId} not found!");

            }
            return await _HvZDbEfContext.Player.Where(p => p.GameId == gameId && !p.IsHuman || p.IsPatientZero).ToListAsync();
        }

        public async Task<ICollection<Player>> GetAllPatientZeroInGame(int gameId)
        {
            if (!await GameExistsAsync(gameId))
            {
                _logger.LogError("Game not found with Id: " + gameId);

                throw new EntityNotFoundException($"Game with id {gameId} not found!");

            }
            return await _HvZDbEfContext.Player.Where(p => p.GameId == gameId && p.IsPatientZero).ToListAsync();
        }

        public async Task<Player> GetOneZombieInGame(int gameId, int playerId)
        {
            if (!await GameExistsAsync(gameId))
            {
                _logger.LogError("Game not found with Id: " + gameId);

                throw new EntityNotFoundException($"Game with id {gameId} not found!");

            }
            else if (!await PlayerExistsAsync(playerId))
            {

                _logger.LogError("Player not found with Id: " + playerId);

                throw new EntityNotFoundException($"Game with id {playerId} not found!");
            }
            return await _HvZDbEfContext.Player.Where(p => p.GameId == gameId && p.Id == playerId && !p.IsHuman || p.IsPatientZero).FirstAsync();

            
        }

        public async Task<ICollection<Player>> GetAllHumansInGame(int gameId)
        {
            if (!await GameExistsAsync(gameId))
            {
                _logger.LogError("Game not found with Id: " + gameId);

                throw new EntityNotFoundException($"Game with id {gameId} not found!");

            }
            return await _HvZDbEfContext.Player.Where(p => p.GameId == gameId && p.IsHuman).ToListAsync();

        }

        public async Task<Player> GetOneHumanInGame(int gameId, int playerId)
        {
            if (!await GameExistsAsync(gameId))
            {
                _logger.LogError("Game not found with Id: " + gameId);

                throw new EntityNotFoundException($"Game with id {gameId} not found!");

            }
            else if (!await PlayerExistsAsync(playerId))
            {

                _logger.LogError("Player not found with Id: " + playerId);

                throw new EntityNotFoundException($"Game with id {playerId} not found!");
            }
            return await _HvZDbEfContext.Player.Where(p => p.GameId == gameId && p.Id == playerId && p.IsHuman).FirstAsync();

            
        }
    }
}
