namespace DataAccess;

public static class SqlClientQuery
{
    public const string InsertQuery = "insert into client(name, last_name, numberPhone, email, addres)"
    +" values (@Name, @LastName, @NumberPhone, @Email, @Address);";

    public const string DeleteQuery = "delete from client where id = @Id";
    public const string SelectQuery = "select * from client;";
    public const string SelectQueryByClientId = "select * from client where id = @Id;";

    public const string UpdateQuery = "update client  set  name = '@Name', last_name = '@LastName',"
            +"numberPhone = '@NumberPhone', email = '@Email', addres = '@Address' where id = '@Id';";
    public static string SelectByNumbereLikeQuery(string numberPhone)
    {
        return  $"select * from client where numberphone like '%{numberPhone}%';";
    } 
    public const string SelectByPhoneNumbereQuery = "select * from client where numberphone like @'%NumberPhone%';";
} 
