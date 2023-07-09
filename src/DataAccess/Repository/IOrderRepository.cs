namespace DataAccess;

public interface IOrderRepository : IBaseRepository<DbOrder>
{
    public IAsyncEnumerable<DbOrder> GetByCommentAsync(string comment);
    //discreption
    public Task<DbOrder> GetByCommentSinAsync(string comment);
    public IAsyncEnumerable<DbOrder> GetOrderAsync(DateTime fromDate, DateTime toDate);
    public IAsyncEnumerable<DbOrder> GetOrdersAsync(long clientId);
    public IAsyncEnumerable<DbOrder> GetOrdersAsync(int take, int skip);
}


