using Dealty.WebApi.Data;

namespace Dealty.WebApi.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }

    public interface ICategoryRepositoryAsync : IRepositoryAsync<Category>
    {
    }
}