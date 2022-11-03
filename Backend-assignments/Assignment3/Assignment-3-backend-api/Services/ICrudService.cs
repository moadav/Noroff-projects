namespace Assignment_3_backend_api.Services
{
    public interface ICrudService<T, ID>
    {



        /// <summary>
        /// Get all instances of an entity.
        /// </summary>
        /// <returns>A collection of entites</returns>
        Task<ICollection<T>> GetAllAsync();
        /// <summary>
        /// Get a specific entity by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A singular entity</returns>
        Task<T> GetByIdAsync(ID id);
        /// <summary>
        /// Add a new entity.
        /// </summary>
        /// <param name="entity"></param>
        void AddAsync(T entity);
        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity"></param>
        void UpdateAsync(T entity);
        /// <summary>
        /// Deletes an entity by its Id.
        /// </summary>
        /// <param name="id"></param>
        Task DeleteByIdAsync(ID id);

    }
}
