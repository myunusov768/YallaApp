namespace DataAccess;

public interface IFileStorage<T>
            where T: DbEntity
{
    public Task SaveAsync(T entities);
    public Task SaveAsync(IEnumerable<T> entities);
    public IAsyncEnumerable<T> ReadAsync();
}