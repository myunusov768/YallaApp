using System;
namespace DataAccess;

public static class SqlOrderQuery
{
    public const string InsertQuery = "insert into orders( date, price, comment, client_id)"
    +" values ( @DateOrder, @Price, @Comment, @ClientId );";

    public const string DeleteQuery = "delete from orders where id = @Id";
    public const string SelectQuery = "select * from orders;";
    public const string SelectQueryByClientId = "select * from orders where client_id = @ClientId;";

    public const string UpdateQuery = "update orders set date = @Date, price = @Price,"
            +"comment = @Comment, client_id = @ClientId where id = @id;";
     
    public const string SelectByOrderIdQuery = "select * from orders where id like @Id;";
    public static string SelectPhoneNumber(string phone)
    {
        return "select o.id, o.date, o.comment, o.price, o.client_id from orders o "
        +$"inner join client c on c.id = o.client_id where c.numberPhone like '%{phone}%';";
    }
} 
