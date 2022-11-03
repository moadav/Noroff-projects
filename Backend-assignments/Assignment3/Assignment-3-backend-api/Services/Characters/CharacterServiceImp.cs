using Assignment_3_backend_api.Exceptions;
using Assignment_3_backend_api.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3_backend_api.Services.Characters
{
    public class CharacterServiceImp : ICharacterService
    {

        private readonly MovieDbEfContext _MovieDbEfContext;
        private readonly ILogger<CharacterServiceImp> _logger;

        public CharacterServiceImp(MovieDbEfContext movieDbEfContext, ILogger<CharacterServiceImp> logger)
        {
            _MovieDbEfContext = movieDbEfContext;
            _logger = logger;
        }


        /// <summary>Add a new Character.</summary>
        /// <param name="entity">Character object</param>
        public async void AddAsync(Character entity)
        {
            await _MovieDbEfContext.AddAsync(entity);
             _MovieDbEfContext?.SaveChangesAsync();
        }

        /// <summary>Checks if characters exists asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>returns a bool value wether character exists</returns>
        public bool CharacterExistsAsync(int id)
        {
            return _MovieDbEfContext.Characters!.Any(e => e.Id == id);
        }

        /// <summary>Deletes an Character by its Id.</summary>
        /// <param name="id">the character id</param>
        /// <exception cref="System.NotImplementedException">Throws NotImplentedException</exception>
        public async Task DeleteByIdAsync(int id)
        {
            var character = _MovieDbEfContext.Characters!.Find(id);
            _MovieDbEfContext.Characters.Remove(character!);
            await _MovieDbEfContext.SaveChangesAsync();

        }

        /// <summary>Gets all Characters Asynchronous</summary>
        /// <returns>A collection of characters</returns>
        public async Task<ICollection<Character>> GetAllAsync()
        {
            return await _MovieDbEfContext.Characters!.Include(m => m.Movies).ToListAsync();
        }

        /// <summary>Get a specific Character by its Id.</summary>
        /// <param name="id">the character id</param>
        /// <returns>A singular Character</returns>
        public async Task<Character> GetByIdAsync(int id)
        {
            // Log and throw error handling
            if (!CharacterExistsAsync(id))
            {
                _logger.LogError("Character not found with Id: " + id);
                //Throw Exception
                throw new DatabaseObjectNotFoundException($"Character with id {id} not found!");
            }

            return await _MovieDbEfContext.Characters!
                .Where(c => c.Id == id)
                .Include( m => m.Movies)              
                .FirstAsync();

        }

        /// <summary>Updates an existing character</summary>
        /// <param name="entity">the Character object</param>
        public void UpdateAsync(Character entity)
        {
            // Log and throw pattern
            if (!CharacterExistsAsync(entity.Id))
            {
                _logger.LogError("Character not found with Id: " + entity.Id);
                throw new DatabaseObjectNotFoundException($"Character with id {entity.Id} not found!");


            }
            _MovieDbEfContext.Entry(entity).State = EntityState.Modified;
            _MovieDbEfContext.SaveChangesAsync();


        }

     
    }
}
