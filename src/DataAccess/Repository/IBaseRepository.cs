namespace  DataAccess;

public interface IBaseRepository<T>
    where T : DbEntity 
{
    public Task<long> CreateAsync(T entitie);
    public Task<T> GetAsync(long id);
    public IAsyncEnumerable<T> GetAllAsync();
    public Task UpdateAsync(long id, T entitie);
    public Task DeleteAsync(long id);
}