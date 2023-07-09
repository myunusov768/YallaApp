using DataAccess;
namespace BusinessLogic;


public static class DBOrderExtension
{
    public static DbOrder OrderToDbOrder( this Order newOrder)
    {
        ArgumentNullException.ThrowIfNull(newOrder);

        return new DbOrder()
            { 
                DateOrder = newOrder.DateOrder, 
                Comment = newOrder.Comment, 
                Price = newOrder.Price
            };
    }
    
    public static Order DbOrderToOrder( this DbOrder newOrder)
    {
        ArgumentNullException.ThrowIfNull(newOrder);
        return new Order() 
        {
            DateOrder = newOrder.DateOrder, 
            Comment = newOrder.Comment, 
            Price = newOrder.Price
        }; 
    }
    public static IEnumerable<Order> DbOrderToOrdersCollection( this IEnumerable<DbOrder> listDbOrders)
    {
        ArgumentNullException.ThrowIfNull(listDbOrders);
        var list = new List<Order>();
        foreach(var ent in listDbOrders)
        {
           var ord = new Order() 
            { 
                DateOrder = ent.DateOrder, 
                Comment = ent.Comment, 
                Price = ent.Price
            };
            yield return ord;
        }
    }
}