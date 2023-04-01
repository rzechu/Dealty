using Dealty.WebApi.Data;

namespace Dealty.WebApi.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
    }

    public interface INotificationRepositoryAsync : IRepositoryAsync<Notification>
    {
    }
}