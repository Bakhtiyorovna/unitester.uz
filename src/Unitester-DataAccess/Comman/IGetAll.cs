using Unitester_DataAccess.Utils;

namespace Unitester_DataAccess.Comman;

public interface IGetAll<TViewModel>
{
    public Task<List<TViewModel>> GetAllAsync(PaginationParams @params);
}
