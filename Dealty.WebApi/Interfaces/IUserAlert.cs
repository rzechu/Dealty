using Dealty.WebApi.Data;

namespace Dealty.WebApi.Interfaces
{
    public interface IUserAlertRepository : IRepository<UserAlert>
    {
    }

    public interface IUserAlertRepositoryAsync : IRepositoryAsync<UserAlert>
    {
    }
}