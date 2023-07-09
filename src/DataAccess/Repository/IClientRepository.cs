namespace DataAccess;

public interface IClientRepository : IBaseRepository<DbClient>
{
    Task<DbClient> GetClientAsync(Guid clientId);
    Task<DbClient> GetByPhoneNumberAsync(string number);
    IAsyncEnumerable<DbClient> GetByNameAsync(string name);
    IAsyncEnumerable<DbClient> GetByLastNameAsync(string lastName);
    IAsyncEnumerable<DbClient> GetByAddressAsync(string address);
    IAsyncEnumerable<DbClient> GetByNumberAsync(string phoneNumber);
    IAsyncEnumerable<DbClient> GetClientsAsync();
}