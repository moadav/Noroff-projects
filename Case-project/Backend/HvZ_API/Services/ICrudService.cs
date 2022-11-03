namespace HvZ_API.Services
{
    public interface ICrudService<T,ID>
    {
        /// <summary>
        /// Get all instances of an entity.
        /// </summary>
        /// <returns>A collection of entites</returns>
        Task<ICollection<T>> GetAllAsync();
        /// <summary>
        /// Get a specific entity by its Id.
        /// </summary>
        /// <param name="id"> The id of the entity</param>
        /// <returns>A singular entity</returns>
        Task<T> GetByIdAsync(ID id);
        /// <summary>
        /// Add a new entity.
        /// </summary>
        /// <param name="entity"> The entity object</param>
        Task<T> AddAsync(T entity);
        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity"> The entity object</param>
        Task UpdateAsync(ID id,T entity);
        /// <summary>
        /// Deletes an entity by its Id.
        /// </summary>
        /// <param name="id"> The entity id</param>
        Task DeleteByIdAsync(ID id);

    }
}
