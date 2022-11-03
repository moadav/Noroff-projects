using Assignment_3_backend_api.Exceptions;
using Assignment_3_backend_api.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3_backend_api.Services.Franchises
{
    public class FranchiseServiceImp : IFranchiseService
    {

        private readonly MovieDbEfContext _MovieDbEfContext;
        private readonly ILogger<FranchiseServiceImp> _logger;

        public FranchiseServiceImp(MovieDbEfContext movieDbEfContext, ILogger<FranchiseServiceImp> logger)
        {
            _MovieDbEfContext = movieDbEfContext;
            _logger = logger;
        }

        /// <summary>Add a new Franchise object asynchrous</summary>
        /// <param name="entity">A franchise object</param>
        public async void AddAsync(Franchise entity)
        {
            await _MovieDbEfContext.AddAsync(entity);
            _MovieDbEfContext?.SaveChangesAsync();
        }

        /// <summary>Deletes an Franchise by its Id.</summary>
        /// <param name="id">The franchise id</param>
        /// <exception cref="System.NotImplementedException">Throws NotImplementedException</exception>
        public async Task DeleteByIdAsync(int id)
        {
            var franchise = _MovieDbEfContext.Franchises!.Find(id);
            _MovieDbEfContext.Franchises.Remove(franchise!);
            await _MovieDbEfContext.SaveChangesAsync();
        }

        /// <summary>Checks if Franchises exists asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns a boolean value if franchise exists</returns>
        public bool FranchiseExistsAsync(int id)
        {
            return  _MovieDbEfContext.Franchises.Any(e => e.Id == id);
        }


        /// <summary>Get all Franchises asynchrounsly</summary>
        /// <returns>A collection of Franchises</returns>
        public async Task<ICollection<Franchise>> GetAllAsync()
        {
            return await _MovieDbEfContext.Franchises!.Include(m => m.Movies).ToListAsync();
        }

        /// <summary>Gets all characters asynchronous.</summary>
        /// <param name="franchiseId">The franchise identifier.</param>
        /// <returns>Returns a collections of Characters</returns>
        public async Task<ICollection<Character>> GetAllCharactersAsync(int franchiseId)
        {
            if (!FranchiseExistsAsync(franchiseId))
            {
                _logger.LogError("Franchise not found with Id: " + franchiseId);
                //throw expection
                throw new DatabaseObjectNotFoundException($"Franchise with id {franchiseId} not found!");

            }

            return await _MovieDbEfContext.Movies!.Where(m => m.FranchiseId == franchiseId).SelectMany(c => c.Characters!).Distinct().ToListAsync();
        }

        /// <summary>Gets all movies asynchronous.</summary>
        /// <param name="franchiseId">The franchise identifier.</param>
        /// <returns>Returns a Collection of movies</returns>
        public async Task<ICollection<Movie>> GetAllMoviesAsync(int franchiseId)
        {
            if (!FranchiseExistsAsync(franchiseId))
            {
                _logger.LogError("Franchise not found with Id: " + franchiseId);
                //throw expection
                throw new DatabaseObjectNotFoundException($"Franchise with id {franchiseId} not found!");

            }

            return await _MovieDbEfContext.Movies!.Where(m => m.FranchiseId == franchiseId).Include(c => c.Characters).Include(f => f.Franchise).ToListAsync();
        }

        /// <summary>Get a specific Franchise by its Id.</summary>
        /// <param name="id">the Franchise id</param>
        /// <returns>Gets a Franchise object</returns>
        public async Task<Franchise> GetByIdAsync(int id)
        {
            // Log and throw error handling
            if (!FranchiseExistsAsync(id))
            {
                _logger.LogError("Franchise not found with Id: " + id);
                //Throw Exception
                throw new DatabaseObjectNotFoundException($"Franchise with id {id} not found!");

            }

            return await _MovieDbEfContext.Franchises!
                .Where(c => c.Id == id)
                .Include(m => m.Movies)
                .FirstAsync();
        }

        /// <summary>Updates an existing franchise.</summary>
        /// <param name="entity">the Franchsie object</param>
        public void UpdateAsync(Franchise entity)
        {
            // Log and throw pattern
            if (!FranchiseExistsAsync(entity.Id))
            {
                _logger.LogError("Franchise not found with Id: " + entity.Id);
                throw new DatabaseObjectNotFoundException($"Franchise with id {entity.Id} not found!");


            }
            _MovieDbEfContext.Entry(entity).State = EntityState.Modified;
            _MovieDbEfContext.SaveChangesAsync();
        }

        /// <summary>Updates the franchises Movies asynchronous.</summary>
        /// <param name="movieIds">The movie ids.</param>
        /// <param name="franchiseId">The franchise identifier.</param>
        public void UpdateMoviesAsync(int[] movieIds, int franchiseId)
        {
            // Log and throw pattern
            if (!FranchiseExistsAsync(franchiseId))
            {
                _logger.LogError("Franchise not found with Id: " + franchiseId);
                //throw expection
                throw new DatabaseObjectNotFoundException($"Franchise with id {franchiseId} not found!");

            }

            List<Movie> movies = movieIds
                .ToList()
                .Select(sid => _MovieDbEfContext.Movies!
                .Where(s => s.Id == sid).First())
                .ToList();

            Franchise franchise = _MovieDbEfContext.Franchises!
                .Where(p => p.Id == franchiseId)
                .FirstAsync().Result;

            franchise.Movies = movies;
            _MovieDbEfContext.Entry(franchise).State = EntityState.Modified;
            // Save all the changes
            _MovieDbEfContext?.SaveChangesAsync();

        }
    }
}
