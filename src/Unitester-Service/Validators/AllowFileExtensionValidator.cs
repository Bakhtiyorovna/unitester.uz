using Unitester_Service.Comman.Helpers;

namespace Unitester_Service.Validators;

public class AllowFileExtensionValidator
{
    public static bool Validate(string extension)
    {
        return (MediaHelper.GetImageExtensions()).Contains(extension);
    }

}
