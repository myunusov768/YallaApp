namespace DataAccess;

public interface IDbRepository<T> : IDisposable
where T : DbEntity
{
    public Task<bool> CreateAsync(T entity);
    public Task<T> GetAsync(long id);
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<bool> UpdateAsync(T entitie);
    public Task<bool> DeleteAsync(long id);

    public Task ConnectionAsync();
}