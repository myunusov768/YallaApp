using DataAccess;
using System.Threading.Tasks;
namespace BusinessLogic;

public static class DBClientExtension
{
    public static DbClient DbClient(this Client newClient)
    {
        ArgumentNullException.ThrowIfNull(newClient);
        return new DbClient()
        { 
            Name = newClient.Name, 
            LastName = newClient.LastName, 
            NumberPhone = newClient.NumberPhone, 
            Address = newClient.Address 
        };
    }

    public static Client DbClientToClient(this DbClient newClient)
    {
        ArgumentNullException.ThrowIfNull(newClient);
        return new Client()
        { 
            Name = newClient.Name, 
            LastName = newClient.LastName, 
            NumberPhone = newClient.NumberPhone, 
            Address = newClient.Address 
        };
    }
    public static  IEnumerable<Client> DbClientToClientsCollection( this IEnumerable<DbClient> listDbClient)
    {
        ArgumentNullException.ThrowIfNull(listDbClient);
        var list = new List<Client>();
        foreach(var ent in listDbClient)
        {
           var client = new Client() 
            {
                Name = ent.Name,
                LastName = ent.LastName,
                NumberPhone = ent.NumberPhone,
                Address = ent.Address,
                Email = ent.Email
            };
            list.Add(client);
        }
        return list;
    }
}