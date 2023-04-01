using Dealty.WebApi.Data;

namespace Dealty.WebApi.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
    }

    public interface IUserRepositoryAsync : IRepositoryAsync<User>
    {
    }
}