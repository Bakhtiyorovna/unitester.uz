using Npgsql;
namespace Unitester_DataAccess.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;
    public BaseRepository()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connection = new NpgsqlConnection("Host=localhost; Port=5432; Database=UniTester-db; User Id=postgres; Password=kraleca7577;");
    }
}
