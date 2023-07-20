using Dapper;
using Unitester_DataAccess.Interfaces.Contests;
using Unitester_DataAccess.Utils;
using Unitester_Domain.Entities.Contests;

namespace Unitester_DataAccess.Repositories.Contests;
public class ContestRepository : BaseRepository, IContestRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from contests";
            var result = await _connection.QuerySingleAsync<long>(query);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> CreateAsync(Contest entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.contests( " +
                "started_at, end_at, status, student_number, description, created_at, updated_at) " +
                "VALUES(StartedAt, EndAt, Status, StudentNumber, Description, CreatedAt, UpdatedAt); ";
            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "DELETE FROM contests WHERE id=@Id";
            var result = await _connection.ExecuteAsync(query, new { Id = id });
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<List<Contest>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM contests order by id desc " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<Contest>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<Contest>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Contest> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM contests where id=@Id";
            var result = await _connection.QuerySingleAsync<Contest>(query, new { Id = id });
            return result;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Contest entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"UPDATE public.contests " +
                $"SET started_at = @StartedAt, end_at = @EndAt, status = @Status, student_number = @StudentNumber, description = @Description, created_at = @CreatedAt, updated_at = @UpdatedAt " +
                $"WHERE id = {id}; ";

            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
