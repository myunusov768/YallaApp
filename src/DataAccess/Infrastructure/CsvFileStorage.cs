using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
namespace DataAccess;

public sealed class CsvFileStorage<T> : IFileStorage<T>
            where T : DbEntity
{
    public readonly string _filePath ;
    public CsvFileStorage(string filePath)
    {
        _filePath = filePath;
        if(File.Exists(_filePath))
        {
            var saveTask = SaveAsync(new List<T>());
            saveTask.Start();
            saveTask.Wait();
        }
    }
    public IAsyncEnumerable<T> ReadAsync()
    {
        using var reader = new StreamReader(_filePath);
        var config = new CsvConfiguration(CultureInfo.CurrentCulture);
        using var csv = new CsvReader(reader, config);
        return csv.GetRecordsAsync<T>();
    }
    
    public async Task SaveAsync(T entity)
    {
        await using var writer = new StreamWriter(_filePath);
        var config = new CsvConfiguration(CultureInfo.CurrentCulture);
        await using var csv = new CsvWriter(writer, config);
        csv.WriteRecord(entity);
    }

    public async Task SaveAsync(IEnumerable<T> entities)
    {
        await using StreamWriter writer = new StreamWriter(_filePath);
        var config = new CsvConfiguration(CultureInfo.CurrentCulture);
        await using CsvWriter csv = new CsvWriter(writer, config);
        await csv.WriteRecordsAsync(entities);
    }
}
