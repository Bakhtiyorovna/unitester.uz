using Unitester_DataAccess.Comman;
using Unitester_Domain.Entities.Users;
namespace Unitester_DataAccess.Interfaces.Users;
public interface IUserRepository : IRepository<User, User>,
    IGetAll<User>
{
    public Task<long> IStudentcount(long id);

    public Task<User?> GetByconstructionAsync(string construction);

    public Task<long> CountRoleAsync(string role);
}
