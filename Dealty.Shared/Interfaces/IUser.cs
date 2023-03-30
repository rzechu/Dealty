using Dealty.Shared.Data;

namespace Dealty.Shared.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
    }

    public interface IUserRepositoryAsync : IRepositoryAsync<User>
    {
    }
}