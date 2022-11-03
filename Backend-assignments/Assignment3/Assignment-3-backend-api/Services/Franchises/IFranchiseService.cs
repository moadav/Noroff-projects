using Assignment_3_backend_api.Models;

namespace Assignment_3_backend_api.Services.Franchises
{
    public interface IFranchiseService : ICrudService<Franchise, int>
    {

        /// <summary>Updates the franchises asynchronous.</summary>
        /// <param name="movieIds">The movie ids.</param>
        /// <param name="franchiseId">The franchise identifier.</param>
        void UpdateMoviesAsync(int[] movieIds, int franchiseId);

        /// <summary>Franchises the exists asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns a boolean value if franchise exiss</returns>
        bool FranchiseExistsAsync(int id);


        /// <summary>Gets all movies asynchronous.</summary>
        /// <param name="franchiseId">The franchise identifier.</param>
        /// <returns>Returns a Collection of movies</returns>
        Task<ICollection<Movie>> GetAllMoviesAsync(int franchiseId);


        /// <summary>Gets all characters asynchronous.</summary>
        /// <param name="franchiseId">The franchise identifier.</param>
        /// <returns>Returns a collections of Characters</returns>
        Task<ICollection<Character>> GetAllCharactersAsync(int franchiseId);
    }
}
