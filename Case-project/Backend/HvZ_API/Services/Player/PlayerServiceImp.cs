using HvZ_API.Utils.Exceptions;
using HvZ_API.Models;
using Microsoft.EntityFrameworkCore;
using HvZ_API.Contexts;
using System.Numerics;

namespace HvZ_API.Services
{
    public class PlayerServiceImp : IPlayerService
    {

        private readonly HvZDbEfContext _HvZDbEfContext;
        private readonly ILogger<ChatServiceImp> _logger;

        public PlayerServiceImp(HvZDbEfContext hvZDbEfContext, ILogger<ChatServiceImp> logger)
        {
            _HvZDbEfContext = hvZDbEfContext;
            _logger = logger;
        }
        public async Task<Player> GetCheckInAsync(int playerId)
        {
            if (!await PlayerExistsAsync(playerId))
            {
                _logger.LogError("Player not found with Id: " + playerId);
                //Throw Exception
                throw new EntityNotFoundException($"Player with id {playerId} not found!");

            }
            return await _HvZDbEfContext.Player.Where(p => p.Id == playerId).FirstAsync();
        }
        public async Task<ICollection<Player>> GetSquadCheckInsAsync(int squadId)
        {
            if (!await SquadExistsAsync(squadId))
            {
                _logger.LogError("Player not found with Id: " + squadId);
                //Throw Exception
                throw new EntityNotFoundException($"Player with id {squadId} not found!");

            }
            return await _HvZDbEfContext.Player.Where(p => p.SquadId == squadId).ToListAsync();
        }

        public async Task<bool> PlayerExistsAsync(int id)
        {
            return await _HvZDbEfContext.Player.AnyAsync(e => e.Id == id);
        }



        public async Task<Player> CreateCheckInForPlayerAsync(int playerId, Player checkin)
        {
            if(!await PlayerExistsAsync(playerId))
            {
                _logger.LogError("Player not found with Id: " + playerId);
                //Throw Exception
                throw new EntityNotFoundException($"Player with id {playerId} not found!");
            }
            var realPlayer = await _HvZDbEfContext.Player.Where(i => i.Id == checkin.Id).FirstAsync();
            realPlayer.CheckinLat = checkin.CheckinLat;
            realPlayer.CheckinLon = checkin.CheckinLon;

            _HvZDbEfContext.Entry(realPlayer).State = EntityState.Modified;
            await _HvZDbEfContext.SaveChangesAsync();
            return realPlayer;
        }


        public string generateBiteCode(string s)
        {
            int MUST_BE_LESS_THAN = 1000000; // 6 decimal digits


            //https://stackoverflow.com/questions/548158/fixed-length-numeric-hash-code-from-variable-length-string-in-c-sharp
            uint hash = 0;

            foreach (byte b in System.Text.Encoding.Unicode.GetBytes(s))
            {
                hash += b;
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
            // final avalanche
            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            // helpfully we only want positive integer < MUST_BE_LESS_THAN
            // so simple truncate cast is ok if not perfect
            return (hash % MUST_BE_LESS_THAN).ToString();
        }

        public async Task<ICollection<Player>> GetAllFromGameAsync(int gameId)
        {
            return await _HvZDbEfContext.Player.Where(g => g.GameId == gameId).ToListAsync();

        }

        public async Task<ICollection<Player>> GetAllAsync()
        {
            return await _HvZDbEfContext.Player.Include(p => p.Chats).ToListAsync();

        }

        public async Task<Player> GetByIdAsync(int id)
        {
            if (!await PlayerExistsAsync(id))
            {
                _logger.LogError("Player not found with Id: " + id);
                //Throw Exception
                throw new EntityNotFoundException($"Player with id {id} not found!");

            }
            return await _HvZDbEfContext.Player
                .Where(c => c.Id == id)
                .FirstAsync();
        }

        public async Task<Player> AddAsync(Player entity)
        {
            if (!await GameExistsAsync(entity.GameId))
            {
                _logger.LogError("Game not found with Id: " + entity.GameId);
                //Throw Exception
                throw new EntityNotFoundException($"Game with id {entity.GameId} not found!");


            }

            var player = await _HvZDbEfContext.AddAsync(entity);
            player.Entity.BiteCode = generateBiteCode(new Random().Next().ToString());
            player.Entity.Squad = _HvZDbEfContext.Squad.Where(s => s.Id == entity.SquadId).FirstOrDefault();
            await _HvZDbEfContext.SaveChangesAsync();
            return player.Entity;
        }

        public async Task<bool> GameExistsAsync(int id)
        {
            return await _HvZDbEfContext.Game.AnyAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(int id, Player entity)
        {

            if (!await PlayerExistsAsync(entity.Id))
            {
                _logger.LogError("Player not found with Id: " + entity.Id);
                //Throw Exception
                throw new EntityNotFoundException($"Player with id {entity.Id} not found!");
            }

            _HvZDbEfContext.Entry(entity).State = EntityState.Modified;
            await _HvZDbEfContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (!await PlayerExistsAsync(id))
            {
                _logger.LogError("Player not found with Id: " + id);
                //Throw Exception
                throw new EntityNotFoundException($"Player with id {id} not found!");
            }
            var player = await _HvZDbEfContext.Player.Where(p => p.Id == id).FirstAsync();
            _HvZDbEfContext.Player.Remove(player);
            await _HvZDbEfContext.SaveChangesAsync();
        }

        public async Task<bool> SquadExistsAsync(int id)
        {
           return await _HvZDbEfContext.Squad.AnyAsync(s => s.Id == id);
        }
    }

}
