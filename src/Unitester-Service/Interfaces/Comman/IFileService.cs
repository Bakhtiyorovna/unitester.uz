using Microsoft.AspNetCore.Http;
namespace Unitester_Service.Interfaces.Comman;

public interface IFileService
{
    // returns sub path of this image
    public Task<string> UploadImageAsync(IFormFile image);

    public Task<bool> DeleteImageAsync(string subpath);

    // returns sub path of this avatar
    public Task<string> UploadAvatarAsync(IFormFile avatar);

    public Task<bool> DeleteAvatarAsync(string subpath);
}
