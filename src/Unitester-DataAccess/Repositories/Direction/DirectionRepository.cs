using Unitester_Domain.Entities.Directions;
using Dapper;
using Unitester_DataAccess.Interfaces.Directions;
using Unitester_DataAccess.Utils;
using Unitester_DataAccess.Comman;

namespace Unitester_DataAccess.Repositories.Direction;

public class DirectionRepository : BaseRepository, IDirectionRepository
{
    public async Task<List<Unitester_Domain.Entities.Directions.Direction>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM directions order by id desc " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<Unitester_Domain.Entities.Directions.Direction>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<Unitester_Domain.Entities.Directions.Direction>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
