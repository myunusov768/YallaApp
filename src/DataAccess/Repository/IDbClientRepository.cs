namespace DataAccess;

public interface IDbClientRepository : IDbRepository<DbClient>
{
    public Task<IEnumerable<DbClient>> GetByPhoneNumber(string numberPhone);
    public Task<IEnumerable<DbClient>> GetByPhoneNumberLike(string numberPhone);
    public Task<bool> CreatePackageAsync(IList<DbClient> orders);
}