
using HvZ_API.Models;
using Microsoft.EntityFrameworkCore;
using HvZ_API.Utils.Exceptions;
using HvZ_API.Contexts;

namespace HvZ_API.Services
{
    public class ChatServiceImp : IChatService
    {

        private readonly HvZDbEfContext _HvZDbEfContext;
        private readonly ILogger<ChatServiceImp> _logger;

        public ChatServiceImp(HvZDbEfContext hvZDbEfContext, ILogger<ChatServiceImp> logger)
        {
            _HvZDbEfContext = hvZDbEfContext;
            _logger = logger;
        }

        public async Task<Chat> AddAsync(Chat entity)
        {
            if (!await GameExistsAsync((int)entity.GameId))
            {
                _logger.LogError("Game not found with Id: " + entity.GameId);
                //Throw Exception
                throw new EntityNotFoundException($"Game with id {entity.GameId} not found!");

            }
            else if (!await PlayerExistsAsync((int)entity.PlayerId))
            {
                _logger.LogError("Player not found with Id: " + entity.PlayerId);
                //Throw Exception
                throw new EntityNotFoundException($"Player with id {entity.PlayerId} not found!");

            }
            var player = await _HvZDbEfContext.Player.Where(p => p.Id == entity.PlayerId).FirstAsync();          
            entity.FirstName = player.FirstName;
            entity.LastName = player.LastName;
           

            var chat = await _HvZDbEfContext.AddAsync(entity);
            await _HvZDbEfContext.SaveChangesAsync();
            return chat.Entity;

        }

        public async Task<ICollection<Chat>> GetAllGlobalMessageAsync(int gameId)
        {
            if (!await GameExistsAsync(gameId))
            {
                _logger.LogError("Game not found with Id: " + gameId);
                //Throw Exception
                throw new EntityNotFoundException($"Game with id {gameId} not found!");

            }

            return await _HvZDbEfContext.Chat.Where(s => s.GameId == gameId && s.IsZombieGlobal == null && s.IsHumanGlobal == null && s.SquadId == null).ToListAsync();
        }



        public async Task<ICollection<Chat>> GetAllSquadMessageAsync(int gameId, int squadId)
        {
            if (!await GameExistsAsync(gameId))
            {
                _logger.LogError("Game not found with Id: " + gameId);
                //Throw Exception
                throw new EntityNotFoundException($"Game with id {gameId} not found!");

            }
            else if (!await SquadExistsAsync(squadId))
            {
                _logger.LogError("Squad not found with Id: " + squadId);
                //Throw Exception
                throw new EntityNotFoundException($"Squad with id {squadId} not found!");
            }
            return await _HvZDbEfContext.Chat.Where(s => s.SquadId == squadId && s.GameId == gameId).ToListAsync();
        }

        public async Task<ICollection<Chat>> GetAllLocalZombieMessageAsync(int gameId)
        {
            if (!await GameExistsAsync(gameId))
            {
                _logger.LogError("Game not found with Id: " + gameId);
                //Throw Exception
                throw new EntityNotFoundException($"Game with id {gameId} not found!");

            }

            return await _HvZDbEfContext.Chat.Where(s => s.GameId == gameId && s.IsZombieGlobal == true).ToListAsync();
        }
        public async Task<ICollection<Chat>> GetAllLocalHumanMessageAsync(int gameId)
        {
            if (!await GameExistsAsync(gameId))
            {
                _logger.LogError("Game not found with Id: " + gameId);
                //Throw Exception
                throw new EntityNotFoundException($"Game with id {gameId} not found!");

            }

            return await _HvZDbEfContext.Chat.Where(s => s.GameId == gameId && s.IsHumanGlobal == true ).ToListAsync();
        }


        public async Task<ICollection<Chat>> GetPlayerLocalHumanMessageAsync(int gameId)
        {
            if (!await GameExistsAsync(gameId))
            {
                _logger.LogError("Game not found with Id: " + gameId);
                //Throw Exception
                throw new EntityNotFoundException($"Game with id {gameId} not found!");

            }
            return await _HvZDbEfContext.Chat.Where(s => s.GameId == gameId && s.IsHumanGlobal == true).ToListAsync();
        }
        public async Task<bool> SquadExistsAsync(int id)
        {
            return await _HvZDbEfContext.Squad.AnyAsync(e => e.Id == id);

        }
        public async Task<bool> ChatExistsAsync(int id)
        {
            return await _HvZDbEfContext.Chat.AnyAsync(e => e.Id == id);
        }
        public async Task DeleteByIdAsync(int id)
        {
            if (!await ChatExistsAsync(id))
            {
                _logger.LogError("Game not found with Id: " + id);
                //Throw Exception
                throw new EntityNotFoundException($"Game with id {id} not found!");
            }

            var chat = _HvZDbEfContext.Chat.Where(c => c.Id == id).FirstOrDefault();
            _HvZDbEfContext.Chat.Remove(chat);
            await _HvZDbEfContext.SaveChangesAsync();
        }

        public async Task<bool> GameExistsAsync(int id)
        {
            return await _HvZDbEfContext.Game.AnyAsync(e => e.Id == id);
        }
        public async Task<bool> PlayerExistsAsync(int id)
        {
            return await _HvZDbEfContext.Player.AnyAsync(e => e.Id == id);
        }

        public async Task<ICollection<Chat>> GetAllAsync()
        {
            return await _HvZDbEfContext.Chat.ToListAsync();
        }
        public async Task<ICollection<Chat>> GetAllFromGameAsync(int gameId)
        {
            return await _HvZDbEfContext.Chat.Where(c => c.GameId == gameId).ToListAsync();
        }

        public async Task<Chat> GetByIdAsync(int id)
        {
            if (!await ChatExistsAsync(id))
            {
                _logger.LogError("Chat not found with Id: " + id);
                //Throw Exception
                throw new EntityNotFoundException($"Chat with id {id} not found!");

            }

            return await _HvZDbEfContext.Chat
                .Where(c => c.Id == id)
                .FirstAsync();
        }


        public async Task UpdateAsync(int id, Chat entity)
        {
            if (!await ChatExistsAsync(id))
            {
                _logger.LogError("Chat not found with Id: " + id);
                //Throw Exception
                throw new EntityNotFoundException($"Chat with id {id} not found!");
            }
            _HvZDbEfContext.Entry(entity).State = EntityState.Modified;
            await _HvZDbEfContext.SaveChangesAsync();
        }

    }
}
