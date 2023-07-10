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
        var result = await _connection.ExecuteAsync(SqlClientQuery.InsertQuery,new DbClient(){ Id = id });
        return result > 0 ? true : false;
    }

    private bool _desposing = false;
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
    private async ValueTask DisposeAsync(bool desposing)
    {
        if(!_desposing)
        {
            if(desposing)
            {
                await _connection.DisposeAsync();
            }
        }
        _desposing = true;
        await ValueTask.CompletedTask;
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
        return await _connection.QueryAsync<DbClient>(SqlClientQuery.SelectByPhoneNumbereQuery,new DbClient{ NumberPhone = numberPhone} );
    }

    public async Task<IEnumerable<DbClient>> GetByPhoneNumberLike(string numberPhone)
    {
        ArgumentNullException.ThrowIfNull(numberPhone);
        await ConnectionAsync();
        return await _connection.QueryAsync<DbClient>(SqlClientQuery.SelectByNumbereLikeQuery(numberPhone));
    }

    public async Task<bool> UpdateAsync(DbClient entitie)
    {
        ArgumentNullException.ThrowIfNull(entitie);
        await ConnectionAsync();
        await _connection.ExecuteAsync(SqlClientQuery.UpdateQuery,entitie);
        return true;
    }
}
