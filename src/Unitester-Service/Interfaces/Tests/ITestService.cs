
using Unitester_DataAccess.Utils;
using Unitester_Domain.Entities.Tests;
using Unitester_Domain.Enums;
using Unitester_Service.Dtos.Tests;

namespace Unitester_Service.Interfaces.Tests;

public interface ITestService
{

    public Task<bool> DeleteAsync(long testId);

    public Task<long> CountAsync();

  //  public Task<long> CountAsync(long id);

    public Task<IList<Test>> GetAllDirectionAsync(PaginationParams @params, long directionId);

    public Task<IList<Test>> GetAllTypeAsync(PaginationParams @params, TestType type);

    public Task<Test> GetByIdAsync(long testId);

    public Task<bool> UpdateAsync(long testId, TestUpdateDto dto);

    public Task<bool> CreateAsync(TestCreatedDto entity);

    
}
