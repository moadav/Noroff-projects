using Assignment_3_backend_api.Models;

namespace Assignment_3_backend_api.Services.Characters
{
    public interface ICharacterService: ICrudService<Character, int>
    {

        /// <summary>Checks if characters exists asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>returns a bool value wether character exists</returns>
        bool CharacterExistsAsync(int id);
    }
}
