
namespace Unitester_Service.Comman.Helpers;

public class MediaHelper
{
    public static string MakeImageName(string filename)
    {
        FileInfo fileInfo = new FileInfo(filename);
        string extension = fileInfo.Extension;
        string name = "IMG_" + Guid.NewGuid() + extension;
        return name;
    }
    public static string[] GetImageExtensions()
    {
        return new string[]
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".heic"
        };
    }
}
