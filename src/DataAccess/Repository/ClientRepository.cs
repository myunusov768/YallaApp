namespace DataAccess;

public sealed class ClientRepository : BaseRepository<DbClient>, IClientRepository
{
    public ClientRepository(IFileStorage<DbClient> fileStorage)
     : base(fileStorage)
    {
    }

    public async IAsyncEnumerable<DbClient> GetByAddressAsync(string address)
    {
        ArgumentNullException.ThrowIfNull(address);
        foreach(var item in _list.Where(x=>x.Address.Contains(address)))
        {
            yield return item;
        }
    }

    public async IAsyncEnumerable<DbClient> GetByNameAsync(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        
        foreach(var item in _list.Where(x=>x.NumberPhone.Equals(name)))
        {
            yield return item;
        }
    }

    public async IAsyncEnumerable<DbClient> GetByLastNameAsync(string lastName)
    {
        ArgumentNullException.ThrowIfNull(lastName);
        foreach(var item in _list.Where(x=>x.LastName.Equals(lastName)))
        {
            yield return item;
        }
    }


    public Task<DbClient> GetClientAsync(Guid clientId)
    {
        ArgumentNullException.ThrowIfNull(clientId);
        return Task.Run(() => _list.Single(x=>x.Id.Equals(clientId)));
    }

    public async IAsyncEnumerable<DbClient> GetClientsAsync()
    {
        foreach(var item in _list)
        {
            yield return item;
        }
    }

    public async IAsyncEnumerable<DbClient> GetByNumberAsync(string phoneNumber)
    {
        ArgumentNullException.ThrowIfNull(phoneNumber);
        foreach(var item in _list.Where(x=> x.NumberPhone.Equals(phoneNumber)))
        {
            yield return item;
        }
    }

    public Task<DbClient> GetByPhoneNumberAsync(string number)
    {
        ArgumentNullException.ThrowIfNull(number);
        return Task.Run(()=> _list.FirstOrDefault(x=>x.NumberPhone.Equals(number)));
    }
}
