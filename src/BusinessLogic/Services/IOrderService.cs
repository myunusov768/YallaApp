using DataAccess;
namespace BusinessLogic;

public interface IOrderServices
{
    Task<long> CreateOrderAsync(Order order, Client client);
    Task DeleteAsinc(long orderId);
    Task UpdateAsync(long orderId, Order orderForUpdate);
    Task<Order> GetOrderAsync(string description);
    Task<Order> GetOrderAsync(long orderId);
    Task<Order> GetOrderAsync(long clientId, long orderId);
    IAsyncEnumerable<Order> GetOrderAsync();
    IAsyncEnumerable<Order> GetOrdersAsync(long clientId);
    IAsyncEnumerable<Order> GetOrdersAsync(string phoneNumber);
    IAsyncEnumerable<Order> GetOrdersAsync(int take, int skip);
    IAsyncEnumerable<Order> GetOrderAsync(DateTime fromDate, DateTime toDate);
}