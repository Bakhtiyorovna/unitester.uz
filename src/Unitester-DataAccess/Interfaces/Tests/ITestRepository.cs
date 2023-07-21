using Unitester_DataAccess.Comman;
using Unitester_DataAccess.Utils;
using Unitester_Domain.Entities.Tests;
using Unitester_Domain.Enums;

namespace Unitester_DataAccess.Interfaces.Tests;

public interface ITestRepository:IRepository<Test,Test> 
{
    public Task<List<Test>> GetAllDirectionAsync(PaginationParams @params,long id);
    public Task<List<Test>> GetAllTypeAsync(PaginationParams @params,TestType type);
}
