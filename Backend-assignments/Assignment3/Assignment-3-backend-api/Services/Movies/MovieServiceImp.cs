using Assignment_3_backend_api.Exceptions;
using Assignment_3_backend_api.Models;
using Assignment_3_backend_api.Services.Characters;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3_backend_api.Services.Movies
{
    public class MovieServiceImp : IMovieService
    {

        private readonly MovieDbEfContext _MovieDbEfContext;
        private readonly ILogger<MovieServiceImp> _logger;

        public MovieServiceImp(MovieDbEfContext movieDbEfContext, ILogger<MovieServiceImp> logger)
        {
            _MovieDbEfContext = movieDbEfContext;
            _logger = logger;
        }

        /// <summary>Add a new entity.</summary>
        /// <param name="entity">The movie object</param>
        public async void AddAsync(Movie entity)
        {
            await _MovieDbEfContext.AddAsync(entity);
            _MovieDbEfContext?.SaveChangesAsync();
        }


        /// <summary>Deletes an entity by its Id.</summary>
        /// <param name="id">the Id of the movie</param>
        /// <exception cref="System.NotImplementedException">Throws NotImplementedException</exception>
        public async Task DeleteByIdAsync(int id)
        {
            var movie = _MovieDbEfContext.Movies!.Find(id);
            _MovieDbEfContext.Movies.Remove(movie!);
            await _MovieDbEfContext.SaveChangesAsync();
        }

        /// <summary>Gets a Collection of all Movies</summary>
        /// <returns>A collection of Movies</returns>
        public async Task<ICollection<Movie>> GetAllAsync()
        {
            return await _MovieDbEfContext.Movies!.Include(c => c.Characters).Include(f => f.Franchise).ToListAsync();
        }

        /// <summary>Gets all characters asynchronous.</summary>
        /// <param name="MovieId">The movie identifier.</param>
        /// <returns>Returns a ICollection of character</returns>
        public async Task<ICollection<Character>> GetAllCharactersAsync(int MovieId)
        {
            if (!MovieExistsAsync(MovieId))
            {
                _logger.LogError("Movie not found with Id: " + MovieId);
                //throw expection
                throw new DatabaseObjectNotFoundException($"Movie with id {MovieId} not found!");

            }

            return await _MovieDbEfContext.Movies!.Where(m => m.Id == MovieId).SelectMany(c => c.Characters!).ToListAsync();
        }

        /// <summary>Get a specific Movie by its Id.</summary>
        /// <param name="id">The movie Identifier</param>
        /// <returns>Returns a single Movie object</returns>
        public async Task<Movie> GetByIdAsync(int id)
        {
            // Log and throw error handling
            if (!MovieExistsAsync(id))
            {
                _logger.LogError("Movie not found with Id: " + id);
                //Throw Exception
                throw new DatabaseObjectNotFoundException($"Movie with id {id} not found!");

            }

            return await _MovieDbEfContext.Movies!
                .Where(c => c.Id == id)
                .Include(m => m.Characters)
                .Include(f => f.Franchise)
                .FirstAsync();
        }

        /// <summary>Checks if Movies exists asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>returns a bool value of wether it exists</returns>
        public bool MovieExistsAsync(int id)
        {
            return _MovieDbEfContext.Movies!.Any(e => e.Id == id);
        }

        /// <summary>Updates an existing Movie</summary>
        /// <param name="entity">the Movie identifier</param>
        public void UpdateAsync(Movie entity)
        {
            // Log and throw pattern
            if (!MovieExistsAsync(entity.Id))
            {
                _logger.LogError("Movie not found with Id: " + entity.Id);
                throw new DatabaseObjectNotFoundException($"Movie with id {entity.Id} not found!");


            }
            _MovieDbEfContext.Entry(entity).State = EntityState.Modified;
            _MovieDbEfContext.SaveChangesAsync();
        }

        /// <summary>Updates the characters asynchronous.</summary>
        /// <param name="charactersIds">The characters ids.</param>
        /// <param name="movieId">The movie identifier.</param>
        public void UpdateCharactersAsync(int[] charactersIds, int movieId)
        {
            // Log and throw pattern
            if (!MovieExistsAsync(movieId))
            {
                _logger.LogError("MovieId not found with Id: " + movieId);
                //throw expection
                throw new DatabaseObjectNotFoundException($"Movie with id {movieId} not found!");

            }

            List<Character> characters = charactersIds
                .ToList()
                .Select(sid => _MovieDbEfContext.Characters!
                .Where(s => s.Id == sid).First())
                .ToList();

            Movie movie = _MovieDbEfContext.Movies!
                .Where(p => p.Id == movieId)
                .FirstAsync().Result;

            movie.Characters = characters;
            _MovieDbEfContext.Entry(movie).State = EntityState.Modified;
            // Save all the changes
            _MovieDbEfContext.SaveChangesAsync();
        }
    }
}
