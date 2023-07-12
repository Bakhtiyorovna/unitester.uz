using Unitester_Service.Dtos.Users;
using Unitester_DataAccess.Utils;
using Unitester_Domain.Entities.Users;
namespace Unitester_Service.Interfaces.Users;

public interface IUserService
{
    public Task<bool> CreateAsync(UserCreatedDto dto);

    public Task<bool> DeleteAsync(long userId);

    public Task<long> CountAsync();

    public Task<IList<User>> GetAllAsync(PaginationParams @params);

    public Task<User> GetByIdAsync(long userId);

    public Task<bool> UpdateAsync(long userId, UserUpdateDto dto);
}
