using Npgsql;
using Dapper;
using System.Data;

namespace DataAccess;

public sealed class DbClientRepository : IDbRepository<DbClient>,IDbClientRepository
{
    private readonly string _connectionString;
    private NpgsqlConnection _connection;

    public DbClientRepository(string connectionString)
    {
        _connectionString = connectionString;
        _connection = new NpgsqlConnection(_connectionString);
    }

    public Task ConnectionAsync()
    {
        ArgumentNullException.ThrowIfNull(_connectionString);
        return _connection.State is not ConnectionState.Open ? _connection.OpenAsync() : Task.CompletedTask;
    }

    public async Task<bool> CreateAsync(DbClient entity)
    {
        
        ArgumentNullException.ThrowIfNull(entity);
        await ConnectionAsync();
        var result = await _connection.ExecuteAsync(SqlClientQuery.InsertQuery,entity);
        
        return result > 0 ? true : false;
    }

    public async Task<bool> CreatePackageAsync(IList<DbClient> orders)
    {
        ArgumentNullException.ThrowIfNull(orders);
        await ConnectionAsync();
        var result = await _connection.ExecuteAsync(SqlClientQuery.InsertQuery,orders);
        
        return result > 0 ? true : false;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        ArgumentNullException.ThrowIfNull(id);
        await ConnectionAsync();
        var result = await _connection.ExecuteAsync(SqlClientQuery.InsertQuery,id);
        return result > 0 ? true : false;
    }

    public async void Dispose()
    {
        if(_connection.State is not ConnectionState.Open)
            return;
        await _connection.CloseAsync();
    }

    public async Task<IEnumerable<DbClient>> GetAllAsync()
    {
        await ConnectionAsync();
        return await  _connection.QueryAsync<DbClient>(SqlClientQuery.SelectQuery);
    }

    public async Task<DbClient> GetAsync(long id)
    {
        ArgumentNullException.ThrowIfNull(id);
        await ConnectionAsync();
        return await _connection.QuerySingleAsync<DbClient>(SqlClientQuery.SelectQueryByClientId, id); 
    }

    public async Task<IEnumerable<DbClient>> GetByPhoneNumber(string numberPhone)
    {
        ArgumentNullException.ThrowIfNull(numberPhone);
        await ConnectionAsync();
        return await _connection.QueryAsync<DbClient>(SqlClientQuery.SelectByPhoneNumbereQuery,numberPhone);
    }

    public async Task<IEnumerable<DbClient>> GetByPhoneNumberLike(string numberPhone)
    {
        ArgumentNullException.ThrowIfNull(numberPhone);
        await ConnectionAsync();
        return await _connection.QueryAsync<DbClient>(SqlClientQuery.SelectByNumbereLikeQuery(numberPhone),numberPhone);
    }

    public async Task<bool> UpdateAsync(DbClient entitie)
    {
        ArgumentNullException.ThrowIfNull(entitie);
        await ConnectionAsync();
        await _connection.ExecuteAsync(SqlClientQuery.UpdateQuery,entitie);
        return true;
    }
}
