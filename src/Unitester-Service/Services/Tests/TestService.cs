
using Unitester_DataAccess.Interfaces.Tests;
using Unitester_DataAccess.Utils;
using Unitester_Domain.Entities.Tests;
using Unitester_Domain.Enums;
using Unitester_Domain.Exceptions.Tests;
using Unitester_Service.Comman.Helpers;
using Unitester_Service.Dtos.Tests;
using Unitester_Service.Interfaces.Comman;
using Unitester_Service.Interfaces.Tests;

namespace Unitester_Service.Services.Tests;

public class TestService : ITestService
{
    private readonly ITestRepository _repository;
    private readonly IPaginator _paginator;
    public TestService(ITestRepository testRepository,
        IPaginator paginator)
    {
        this._repository = testRepository;
        this._paginator = paginator;
    }
    public async Task<long> CountAsync()=> await _repository.CountAsync();

    public async Task<bool> CreateAsync(TestCreatedDto dto)
    {
        Test test = new Test()
        {
            test = dto.test,
            DirectionId = dto.DirectionId,
            VariantA=dto.VariantA,
            VariantB=dto.VariantB,
            VariantC=dto.VariantC,
            VariantD=dto.VariantD,
            RightVariant=dto.RightVariant,
            Type=dto.Type,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };
        var result = await _repository.CreateAsync(test);
        return result > 0;
    }

    public Task<int> CreateAsync(Test entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(long testId)
    {
        var test = await _repository.GetByIdAsync(testId);
        if (test is null) throw new TestNotFoundException();

        var dbResult = await _repository.DeleteAsync(testId);
        return dbResult > 0;
    }

    public async Task<IList<Test>> GetAllDirectionAsync(PaginationParams @params, long directionId)
    {
        var tests = await _repository.GetAllDirectionAsync(@params, directionId);
        return tests;
    }

    public async Task<IList<Test>> GetAllTypeAsync(PaginationParams @params, TestType type)
    {
        var tests = await _repository.GetAllTypeAsync(@params, type);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);
        return tests;
    }

    public async Task<Test> GetByIdAsync(long testId)
    {
        var test = await _repository.GetByIdAsync(testId);
        if (test is null) throw new TestNotFoundException();
        else return test;
    }

    public async Task<bool> UpdateAsync(long testId, TestUpdateDto dto)
    {
        var test = await _repository.GetByIdAsync(testId);
        if (test is null) throw new TestNotFoundException();
        long teacherId = 0;

        test.test = dto.test;
        test.VariantA = dto.VariantA;
        test.VariantB = dto.VariantB;
        test.VariantC = dto.VariantC;
        test.VariantD = dto.VariantD;
        test.RightVariant = dto.RightVariant;
        test.Type = dto.Type;
        test.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(testId, test);
        return dbResult > 0;
    }
}
