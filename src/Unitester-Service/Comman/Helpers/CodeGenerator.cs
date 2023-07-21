
namespace Unitester_Service.Comman.Helpers;

public class CodeGenerator
{
    public static int GenerateRandomNumber()
    {
        Random random = new Random();
        return random.Next(1000, 9999);
    }
}
