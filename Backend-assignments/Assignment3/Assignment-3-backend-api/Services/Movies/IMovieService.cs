using Assignment_3_backend_api.Models;

namespace Assignment_3_backend_api.Services.Movies
{
    public interface IMovieService : ICrudService<Movie, int>
    {

        /// <summary>Updates the characters asynchronous.</summary>
        /// <param name="charactersIds">The characters ids.</param>
        /// <param name="movieId">The movie identifier.</param>
        void UpdateCharactersAsync(int[] charactersIds, int movieId);

        /// <summary>Checks if Movies exists asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>returns a bool value of wether it exists</returns>
        bool MovieExistsAsync(int id);


        /// <summary>Gets all characters asynchronous.</summary>
        /// <param name="MovieId">The movie identifier.</param>
        /// <returns>Returns a ICollection of character</returns>
        Task<ICollection<Character>> GetAllCharactersAsync(int MovieId);


    }
}
