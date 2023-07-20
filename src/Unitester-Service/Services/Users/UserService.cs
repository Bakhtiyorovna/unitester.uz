using Unitester_DataAccess.Interfaces.Users;
using Unitester_DataAccess.Utils;
using Unitester_Domain.Entities.Users;
using Unitester_Domain.Exceptions.Files;
using Unitester_Domain.Exceptions.Users;
using Unitester_Service.Comman.Helpers;
using Unitester_Service.Dtos.Users;
using Unitester_Service.Interfaces.Comman;
using Unitester_Service.Interfaces.Users;
namespace Unitester_Service.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IFileService _fileService;
    public UserService(IUserRepository userRepository,
        IFileService fileService)
    {
        this._repository = userRepository;
        this._fileService = fileService;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreateAsync(UserCreatedDto dto)
    {
        string imagepath = await _fileService.UploadImageAsync(dto.Image);
        User user = new User()
        {
            FirstName = dto.FirsName,
            LastName = dto.LastName,
            UserName = dto.UserName,
            ImagePath = imagepath,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Rol = dto.Rol,
            Region = dto.Region,
            Description = dto.Description,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };
        var result = await _repository.CreateAsync(user);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user is null) throw new UserNotFoundException();

        var result = await _fileService.DeleteImageAsync(user.ImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _repository.DeleteAsync(userId);
        return dbResult > 0;
    }

    public async Task<IList<User>> GetAllAsync(PaginationParams @params)
    {
        var users = await _repository.GetAllAsync(@params);
        return users;
    }

    public async Task<User> GetByIdAsync(long categoryId)
    {
        var user = await _repository.GetByIdAsync(categoryId);
        if (user is null) throw new UserNotFoundException();
        else return user;
    }

    public async Task<bool> UpdateAsync(long userId, UserUpdateDto dto)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user is null) throw new UserNotFoundException();

        // parse new items to user
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.Rol = dto.Role;
        user.Region = dto.Region;
        user.UserName = dto.UserName;
        user.PhoneNumber = dto.PhoneNumber;
        user.Description = dto.Description;

        if (dto.Image is not null)
        {
            // delete old image
            var deleteResult = await _fileService.DeleteImageAsync(user.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            // upload new image
            string newImagePath = await _fileService.UploadImageAsync(dto.Image);

            // parse new path to user
            user.ImagePath = newImagePath;
        }
        // else user old image have to save

        user.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(userId, user);
        return dbResult > 0;
    }

}
