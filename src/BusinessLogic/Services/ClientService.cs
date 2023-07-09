using DataAccess;
namespace BusinessLogic;

public sealed class ClientService : IClientServices
{
    private ClientRepository _clientRepository;
    public ClientService(ClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<long> CreateClientAsync(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        return await _clientRepository.CreateAsync(client.DbClient());
    }

    public async Task DeleteClientAsync(long id)
    {
        ArgumentNullException.ThrowIfNull(id);
        await _clientRepository.DeleteAsync(id);
    }

    public async Task<Client> GetClientAsync(long clientId)
    {
        ArgumentNullException.ThrowIfNull(clientId);
        return (await _clientRepository.GetAsync(clientId)).DbClientToClient();
    }

    public async Task<Client> GetClientAsync(string clientPhoneNumber)
    {
        ArgumentNullException.ThrowIfNull(clientPhoneNumber);
        return (await _clientRepository.GetByPhoneNumberAsync(clientPhoneNumber)).DbClientToClient();
    }

    public async IAsyncEnumerable<Client> GetClientsAsync()
    {
        await foreach(var item in  _clientRepository.GetAllAsync())
        {
            yield return item.DbClientToClient();
        }
    }

    public async Task UpdateClientAsync(Client client, long clientId)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(clientId);
        await  _clientRepository.UpdateAsync(clientId, client.DbClient());
    }
}