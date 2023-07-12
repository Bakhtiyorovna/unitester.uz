using Unitester_Domain.Constans;
namespace Unitester_Service.Comman.Helpers;

public class TimeHelper
{
    public static DateTime GetDateTime()
    {
        var dtTime = DateTime.UtcNow;
        dtTime.AddHours(TimeConstans.UTC);
        return dtTime;
    }
}
