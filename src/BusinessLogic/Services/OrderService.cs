using DataAccess;
namespace BusinessLogic;

public sealed class OrderServaice : IOrderServices
{
    IOrderRepository _orderRepository;
    IClientRepository _clientRepository;
    public OrderServaice(IOrderRepository orderRepository, IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
        _orderRepository = orderRepository;
    }
    public async Task<long> CreateOrderAsync(Order order, Client client)
    {
        ArgumentNullException.ThrowIfNull(order);
        ArgumentNullException.ThrowIfNull(client);
        var newDbOrder =  order.OrderToDbOrder();
        var getClient = await _clientRepository.GetByPhoneNumberAsync(client.NumberPhone);
        if(getClient is null)
        {
            var newDbClient =  client.DbClient();
            var newClientId = await _clientRepository.CreateAsync(newDbClient);
            newDbOrder.ClientId = newClientId; 
            return await _orderRepository.CreateAsync(newDbOrder);
        }
        newDbOrder.ClientId = getClient.Id;
        return await _orderRepository.CreateAsync(newDbOrder);
    }

    public async Task<Order> GetOrderAsync(string description)
    {
        return (await _orderRepository.GetByCommentSinAsync(description)).DbOrderToOrder();
    }
    public async Task<Order> GetOrderAsync(long clientId, long orderId)
    {
        return (await _orderRepository.GetAsync(orderId)).DbOrderToOrder();
    }

    public async IAsyncEnumerable<Order> GetOrderAsync()
    {
        var orders =  _orderRepository.GetAllAsync();
        await foreach(var item in orders)
        {
            yield return  item.DbOrderToOrder();
        }

    }

    public async Task<Order> GetOrderAsync(long orderId)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        return (await _orderRepository.GetAsync(orderId)).DbOrderToOrder();
    }
    
    public async IAsyncEnumerable<Order> GetOrdersAsync(long clientId)
    {
        ArgumentNullException.ThrowIfNull(clientId);
        var orders = _orderRepository.GetOrdersAsync(clientId);
        await foreach(var item in orders)
        {
            yield return item.DbOrderToOrder();
        }
    }

    public async IAsyncEnumerable<Order> GetOrdersAsync(int take, int skip)
    {
        ArgumentNullException.ThrowIfNull(take);
        ArgumentNullException.ThrowIfNull(skip);
        var orders = _orderRepository.GetOrdersAsync(take , skip);
        await foreach(var item in orders)
        {
            yield return item.DbOrderToOrder();
        }
    }

    public async IAsyncEnumerable<Order> GetOrdersAsync(string phoneNumber)
    {
        ArgumentNullException.ThrowIfNull(phoneNumber);
        var getClientId = await _clientRepository.GetByPhoneNumberAsync(phoneNumber);
        ArgumentNullException.ThrowIfNull(getClientId);
        var orders = _orderRepository.GetOrdersAsync(getClientId.Id);
        await foreach(var item in orders)
        {
            yield return item.DbOrderToOrder();
        }
    }

    public  Task UpdateAsync(long orderId, Order orderForUpdate)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        ArgumentNullException.ThrowIfNull(orderForUpdate);
        return _orderRepository.UpdateAsync(orderId, orderForUpdate.OrderToDbOrder());
    }

    public async Task DeleteAsinc(long orderId)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        await _orderRepository.DeleteAsync(orderId);
    }

    public async IAsyncEnumerable<Order> GetOrderAsync(DateTime fromDate, DateTime toDate)
    {
        ArgumentNullException.ThrowIfNull(fromDate);
        ArgumentNullException.ThrowIfNull(toDate);
        var orders =  _orderRepository.GetOrderAsync(fromDate, toDate);
        await foreach(var item in orders)
        {
            yield return  item.DbOrderToOrder();
        }
    }
}

