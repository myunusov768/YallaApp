using DataAccess;
namespace BusinessLogic;

public interface IClientServices
{
    Task<long> CreateClientAsync(Client client);
    Task DeleteClientAsync(long id);
    Task<Client> GetClientAsync(long orderId);
    Task<Client> GetClientAsync(string clientPhoneNumber);
     IAsyncEnumerable<Client> GetClientsAsync();
    Task UpdateClientAsync(Client client, long clientId);
}