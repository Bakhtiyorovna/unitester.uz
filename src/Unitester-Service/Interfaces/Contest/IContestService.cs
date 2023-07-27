using Unitester_Domain.Entities.Contests;
using Unitester_DataAccess.Utils;
using Unitester_Service.Dtos.Contets;

namespace Unitester_Service.Interfaces.Contest;

public interface IContestService
{
    public Task<bool> CreateAsync(ContestCreateDto dto);

    public Task<bool> DeleteAsync(long userId);

    public Task<long> CountAsync();

    public Task<bool> UpdateAsync(long contestId, ContestUpdatedDto dto);

    public Task<IList<Unitester_Domain.Entities.Contests.Contest>> GetAllAsync(PaginationParams @params);

    public Task<Unitester_Domain.Entities.Contests.Contest> GetByIdAsync(long userId);

    public Task<bool> RegesterContestAsync(RegisterContestDto dto);
}
