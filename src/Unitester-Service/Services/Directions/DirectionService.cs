
using Unitester_DataAccess.Interfaces.Directions;
using Unitester_DataAccess.Utils;
using Unitester_Domain.Entities.Directions;
using Unitester_Service.Interfaces.Directions;

namespace Unitester_Service.Services.Directions;

public class DirectionService : IDirectionService
{
    public readonly IDirectionRepository _repository;
    public DirectionService(IDirectionRepository repository)
    {
        this._repository = repository;
    }
    public async Task<List<Direction>> GetAllAsync(PaginationParams @params)
    {
        var contests = await _repository.GetAllAsync(@params);
        return contests;
    }
}
