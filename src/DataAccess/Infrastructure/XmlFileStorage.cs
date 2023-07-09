using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace DataAccess;


public sealed class XmlFileStorage<T> : IFileStorage<T>
                where T : DbEntity
{
    private readonly string _filePath ;

    public XmlFileStorage(string filePath)
    {
        _filePath = filePath;
        if(!File.Exists(_filePath))
        {
            var saveTask = SaveAsync(new List<T>());
            saveTask.Start();
            saveTask.Wait();
        }
            
    }
    
    public async IAsyncEnumerable<T> ReadAsync()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T[]));
        using StreamReader fileStream = new StreamReader(_filePath);
        T[]? data = serializer.Deserialize(fileStream) as T[];
        foreach(var item in data)
        {
            yield return item;
        }
    }

    public async Task SaveAsync(T entity)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        await using StreamWriter fileStream = new StreamWriter(_filePath);
        serializer.Serialize(fileStream, entity);
        fileStream.Close();
    }

    public async Task SaveAsync(IEnumerable<T> entities)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
        await using StreamWriter fileStream = new StreamWriter(_filePath);
        serializer.Serialize(fileStream, entities);
        fileStream.Close();
    }
}