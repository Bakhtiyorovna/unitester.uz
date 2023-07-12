
namespace Unitester_DataAccess.Interfaces;

public interface IRepository<TEtity, TViewModel>
{
    public Task<int> CreateAsync(TEtity entity);

    public Task<int> UpdateAsync(long id, TEtity entity);

    public Task<int> DeleteAsync(long id);

    public Task<long> CountAsync();

    public Task<TEtity> GetByIdAsync(long id);
}
