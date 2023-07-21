
namespace Unitester_Domain.Exceptions.Tests;

public class TestNotFoundException :NotFoundException
{
    public TestNotFoundException()
    {
        this.TitleMessage = "Test topilmadi!";
    }
}
