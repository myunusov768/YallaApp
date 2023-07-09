
namespace DataAccess;

public class FileStorageFuctory<T>
        where T : DbEntity
{
    public static IFileStorage<T> GetCsvFileStorage(string filePath) => new CsvFileStorage<T>(filePath);

    public static IFileStorage<T> GetXmlFileStorage(string filePath) => new XmlFileStorage<T>(filePath);

}