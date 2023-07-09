namespace DataAccess;

public interface IDbOrderRepository : IDbRepository<DbOrder>
{
    public Task<IEnumerable<DbOrder>> GetByPhoneNumberLike(string numberPhone);
    public Task<bool> CreatePackageAsync(IList<DbOrder> orders);
}