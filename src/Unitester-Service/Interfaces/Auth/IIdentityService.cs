using Unitester_Domain.Enums;

namespace Unitester_Service.Interfaces.Auth;

public interface IIdentityService
{
    public long UserId { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string PhoneNumber { get; }

    public UserRole? IdentityRole { get; }
}
