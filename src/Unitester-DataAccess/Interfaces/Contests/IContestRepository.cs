using Unitester_DataAccess.Comman;
using Unitester_DataAccess.ViewModels.Contests;
using Unitester_Domain.Entities.Contests;
namespace Unitester_DataAccess.Interfaces.Contests;
public interface IContestRepository : IRepository<Contest, ContetsViewModel>,
    IGetAll<Contest>
{
    public Task<int> RegisterPupilAsync(ContestStudent contest);
    public Task<long?> CountStudentAsync(long contestId);

    public Task<bool> UpdateStudentNumberAsync(long number, long contestId);
}
