using Dealty.Shared.Data;

namespace Dealty.Shared.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }

    public interface ICategoryRepositoryAsync : IRepositoryAsync<Category>
    {
    }
}