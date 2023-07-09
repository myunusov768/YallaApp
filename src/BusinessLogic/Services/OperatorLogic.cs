using DataAccess;

namespace BusinessLogic;

public sealed class OperatorLogic
{
    private IOrderServices _orderServices;
    public OperatorLogic(IOrderServices orderServices)
    {
        _orderServices = orderServices;
    }
    
    public async Task<long> CreateAsync(Order newOrder, Client client)
    {
        ArgumentNullException.ThrowIfNull(newOrder);
        ArgumentNullException.ThrowIfNull(client);
        return await _orderServices.CreateOrderAsync(newOrder, client);
    }

    public async IAsyncEnumerable<Order> GetOrdersAsync(string numberPhone)
    {
        ArgumentNullException.ThrowIfNull(numberPhone);
        await foreach(var item in _orderServices.GetOrdersAsync(numberPhone))
        {
            yield return item;
        } 
    }
    public async Task<Order> GetOrderAsync(long orderId)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        return await _orderServices.GetOrderAsync(orderId);
    }
    
    public async Task UpdateAsync(long orderId, Order orderForUpdate)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        ArgumentNullException.ThrowIfNull(orderForUpdate);
        await _orderServices.UpdateAsync(orderId, orderForUpdate);
    }
}