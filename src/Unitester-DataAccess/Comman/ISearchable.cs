using Unitester_DataAccess.Utils;
namespace Unitester_DataAccess.Comman;
public interface ISearchable<TModel>
{
    public Task<(int ItemsCount, IList<TModel>)> SearchAsync(string search, PaginationParams @params);
}
