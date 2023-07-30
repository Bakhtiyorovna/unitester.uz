
using Unitester_DataAccess.Utils;
using Unitester_Domain.Entities.Directions;

namespace Unitester_Service.Interfaces.Directions;

public interface IDirectionService
{
    public Task<List<Direction>> GetAllAsync(PaginationParams @Params);
}
