namespace DataAccess;

public class BaseRepository<T> : IBaseRepository<T>
            where T: DbEntity
{
    protected List<T> _list;
    private IFileStorage<T> fileStorage; 
    public BaseRepository(IFileStorage<T> storage)
    {
        fileStorage = storage;
        _list = new List<T>();
        var task = Task.Run(async ()=> 
        {
            await foreach(var item in fileStorage.ReadAsync())
            {
                _list.Add(item);
            }
            
        });
        task.Wait();

    }

    public Task<long> CreateAsync(T entity)
    {
        return Task.Run(async ()=> 
        {
            ArgumentNullException.ThrowIfNull(nameof(entity));
            _list.Add(entity);
            await CommitAsync();
            return entity.Id;
        });
    }

    public Task DeleteAsync(long id)
    {
        return Task.Run(async ()=>
        {
            var get = _list.FirstOrDefault(x=>x.Id == id);
            ArgumentNullException.ThrowIfNull(get);
            _list.Remove(get);
            await CommitAsync();
        });
    }

    public async Task<T> GetAsync(long id)
    {
        return await Task.Run(()=> 
        {
            var element =  _list.FirstOrDefault(x=>x.Id == id);
            ArgumentNullException.ThrowIfNull(element);
            return element;
        });
    }

    public async IAsyncEnumerable<T> GetAllAsync()
    {
        foreach(var item in _list)
        {
            yield return item;
        }
    } 

    public Task UpdateAsync(long id, T entitie)
    {
        return Task.Run(()=> 
        {
            var getEntitie = _list.FirstOrDefault(x=>x.Id == id);
            ArgumentNullException.ThrowIfNull(getEntitie);
            getEntitie = entitie;
        });
    }

    private Task CommitAsync() => fileStorage.SaveAsync(_list);
}