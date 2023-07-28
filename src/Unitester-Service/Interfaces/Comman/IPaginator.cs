

using Unitester_DataAccess.Utils;

namespace Unitester_Service.Interfaces.Comman;

public interface IPaginator
{
    public void Paginate(long itemCount, PaginationParams @params);
}
