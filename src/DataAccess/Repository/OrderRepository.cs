namespace DataAccess;

public sealed class OrderRepository : BaseRepository<DbOrder>, IOrderRepository
{
    public OrderRepository(IFileStorage<DbOrder> fileStorage)
     : base(fileStorage)
    {
    }
    public async IAsyncEnumerable<DbOrder> GetByCommentAsync(string comment)
    {
        foreach(var item in _list.Where(x=> x.Comment.Contains(comment)))
        {
            yield return item;
        }
    }
    public Task<DbOrder> GetByCommentSinAsync(string comment) 
            => Task.Run( () => _list.Single(x=> x.Comment == comment));
    
    public async IAsyncEnumerable<DbOrder> GetOrderAsync(DateTime fromDate, DateTime toDate)
    {
        ArgumentNullException.ThrowIfNull(fromDate);
        ArgumentNullException.ThrowIfNull(toDate);

        foreach(var item in _list.Where(x=>x.DateOrder >= fromDate && x.DateOrder <= toDate))
        {
            yield return item;
        }
    }

    public async IAsyncEnumerable<DbOrder> GetOrdersAsync(long clientId)
    {
        foreach(var item in _list.Where(x=>x.ClientId.Equals(clientId)))
        {
            yield return item;
        }
    }

    public async IAsyncEnumerable<DbOrder> GetOrdersAsync(int take, int skip)
    {
        if(_list.Count < take)
            take = _list.Count;
        foreach( var item in  _list.Skip(skip -1).Take(take))
            yield return item;
    }
}
