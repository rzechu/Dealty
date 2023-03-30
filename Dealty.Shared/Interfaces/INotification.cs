using Dealty.Shared.Data;

namespace Dealty.Shared.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
    }

    public interface INotificationRepositoryAsync : IRepositoryAsync<Notification>
    {
    }
}