using Dealty.Shared.Data;

namespace Dealty.Shared.Interfaces
{
    public interface IUserAlertRepository : IRepository<UserAlert>
    {
    }

    public interface IUserAlertRepositoryAsync : IRepositoryAsync<UserAlert>
    {
    }
}