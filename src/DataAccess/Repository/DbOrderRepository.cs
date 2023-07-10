using Npgsql;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess;

public sealed class DbOrderRepository : IDbRepository<DbOrder>, IDbOrderRepository
{
    public readonly string _connectionString;
    private NpgsqlConnection _npgsqlConnection;
    public DbOrderRepository(string stringConnection)
    {
        _connectionString = stringConnection;
        _npgsqlConnection = new NpgsqlConnection(_connectionString);
    }

    public Task ConnectionAsync()
    {
        ArgumentNullException.ThrowIfNull(_connectionString);
        return _npgsqlConnection.State is not ConnectionState.Open ?  _npgsqlConnection.OpenAsync() : Task.CompletedTask; 
    }

    public async Task<bool> CreateAsync(DbOrder entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await ConnectionAsync();
        var transaction = await _npgsqlConnection.BeginTransactionAsync();
        var result = await _npgsqlConnection.ExecuteAsync(SqlOrderQuery.InsertQuery, entity, transaction);
        await transaction.CommitAsync();
        return result > 0 ? true : false;

    }

    public async Task<bool> CreatePackageAsync(IList<DbOrder> orders)
    {
        ArgumentNullException.ThrowIfNull(orders);
        await ConnectionAsync();
        var transaction = await _npgsqlConnection.BeginTransactionAsync();
        var result = await _npgsqlConnection.ExecuteAsync(SqlOrderQuery.InsertQuery, orders, transaction);
        await transaction.CommitAsync();
        return result > 0 ? true : false;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        ArgumentNullException.ThrowIfNull(id);
        await ConnectionAsync();
        var transaction = await _npgsqlConnection.BeginTransactionAsync();
        var result = await _npgsqlConnection.ExecuteAsync(SqlOrderQuery.DeleteQuery, id,transaction);
        await transaction.CommitAsync();
        return result > 0 ? true : false;
    }

    public async Task<IEnumerable<DbOrder>> GetAllAsync()
    {
        await ConnectionAsync();
        var result = await _npgsqlConnection.QueryAsync<DbOrder>(SqlOrderQuery.SelectQuery);
        return result;
    }

    public async Task<DbOrder> GetAsync(long id)
    {
        await ConnectionAsync();
        var result = await _npgsqlConnection.QuerySingleAsync<DbOrder>(SqlOrderQuery.SelectByOrderIdQuery, id);
        return result;
    }

    public async Task<IEnumerable<DbOrder>> GetByPhoneNumberLike(string numberPhone)
    {
        ArgumentNullException.ThrowIfNull(numberPhone);
        await ConnectionAsync();
        return await _npgsqlConnection.QueryAsync<DbOrder>(SqlOrderQuery.SelectPhoneNumber(numberPhone));
    }

    public async Task<bool> UpdateAsync(DbOrder entitie)
    {
        await ConnectionAsync();
        var transaction = await _npgsqlConnection.BeginTransactionAsync();
        var result = await _npgsqlConnection.ExecuteAsync(SqlOrderQuery.UpdateQuery, entitie, transaction);
        await transaction.CommitAsync();
        return result > 0 ? true: false;
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
                await _npgsqlConnection.DisposeAsync();
            }
        }
        _desposing = true;
        await ValueTask.CompletedTask;
    }
}