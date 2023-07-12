using Unitester_Domain.Entities.Users;
using Unitester_DataAccess.Comman;
namespace Unitester_DataAccess.Interfaces.User
{
    public interface IUserRepository : IRepository<Unitester_Domain.Entities.Users.User, Unitester_Domain.Entities.Users.User>, IGetAll<Unitester_Domain.Entities.Users.User>
    {

    }
}
