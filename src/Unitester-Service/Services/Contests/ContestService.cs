using Unitester_DataAccess.Interfaces.Contests;
using Unitester_DataAccess.Utils;
using Unitester_Domain.Entities.Contests;
using Unitester_Domain.Enums;
using Unitester_Domain.Exceptions.Contests;
using Unitester_Service.Comman.Helpers;
using Unitester_Service.Dtos.Contets;
using Unitester_Service.Interfaces.Auth;
using Unitester_Service.Interfaces.Contest;

namespace Unitester_Service.Services.Contests;

public class ContestService : IContestService
{
    private readonly IContestRepository _repository;
    private readonly IIdentityService _identity;
    public int ContestTime = 5;
    public ContestService(IContestRepository userRepository,
        IIdentityService identity)
    {
        this._repository = userRepository;
        this._identity=identity;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreateAsync(ContestCreateDto dto)
    {
        Contest contest = new Contest()
        {
            StartedAt = dto.StartedAt,
            EndAt = dto.StartedAt.AddHours(5),
            Status = ContestStatus.Nostarted,
            StudentNumber=0,
            Description=dto.Description,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };
        var result = await _repository.CreateAsync(contest);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long contestId)
    {
        var user = await _repository.GetByIdAsync(contestId);
        if (user is null) throw new ContestNotFoundException();

        var dbResult = await _repository.DeleteAsync(contestId);
        return dbResult > 0;
    }

    public async Task<IList<Contest>> GetAllAsync(PaginationParams @params)
    {
        var contests = await _repository.GetAllAsync(@params);
        return contests;
    }

    public async Task<Contest> GetByIdAsync(long categoryId)
    {
        var user = await _repository.GetByIdAsync(categoryId);
        if (user is null) throw new ContestNotFoundException();
        else return user;
    }

    public async Task<bool> RegesterContestAsync(RegisterContestDto dto)
    {
        ContestStudent contest = new ContestStudent()
        {
            ContestId = dto.contestId,
            StudentId = _identity.UserId,
            BasicDirectionId=dto.basicDerictionId,
            SecondDirectionId=dto.secondDerictionId
        };
        long StudentNumber =Convert.ToInt64(await _repository.CountStudentAsync(dto.contestId));
        bool resultupdate = await _repository.UpdateStudentNumberAsync(StudentNumber + 1, dto.contestId);

        var result = await _repository.RegisterPupilAsync(contest);
        return result > 0;
    }

    public async Task<bool> UpdateAsync(long contestId, ContestUpdatedDto dto)
    {
        var contest = await _repository.GetByIdAsync(contestId);
        if (contest is null) throw new ContestNotFoundException();

        contest.StartedAt = dto.StartedAt;
        contest.EndAt = dto.StartedAt.AddHours(ContestTime);
        contest.Description = dto.Description;

        contest.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(contestId, contest);
        return dbResult > 0;
    }
}
