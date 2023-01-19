namespace LeaveManagement.Application.Contracts
{
    public interface IGenericRepository<T> where T : class
    {/// <summary>
    /// gets record using id which is int
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
        Task<T?> GetAsync(int?d);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);

        Task AddRangeAsync(List<T> entities);

        Task<bool> Exists(int id);
        Task DeleteAsync(int id);

        Task UpdateAsync(T entity);
        
    }
}
