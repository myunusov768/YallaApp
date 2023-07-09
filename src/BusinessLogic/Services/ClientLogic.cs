using DataAccess;
namespace BusinessLogic;

public sealed class ClientLigic
{
    private IOrderServices _orderServices; 
    public ClientLigic(IOrderServices orderServices)
    {
        _orderServices = orderServices;
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
    public async Task DeleteAsync(long orderId)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        await _orderServices.DeleteAsinc(orderId);
    }
    public Task UpdateAsync(long orderId, Order orderForUpdate)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        ArgumentNullException.ThrowIfNull(orderForUpdate);
        return  _orderServices.UpdateAsync(orderId, orderForUpdate);
    }
}
