using Dealty.Shared.Data;
using Dealty.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dealty.WebApi.Data
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DealtyDBContext _dbContext;

        public NotificationRepository(DealtyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Notification> GetAll()
        {
            return _dbContext.Notifications.ToList();
        }

        public Notification GetById(int id)
        {
            return _dbContext.Notifications.Find(id);
        }

        public void Add(Notification entity)
        {
            _dbContext.Notifications.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Notification entity)
        {
            _dbContext.Notifications.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Notification entity)
        {
            _dbContext.Notifications.Remove(entity);
            _dbContext.SaveChanges();
        }

        #region Async
        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _dbContext.Notifications.ToListAsync();
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _dbContext.Notifications.FindAsync(id);
        }

        public async Task<Notification> AddAsync(Notification entity)
        {
            await _dbContext.Notifications.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Notifications.FindAsync(entity.NotificationID);
        }

        public async Task<Notification> UpdateAsync(Notification entity)
        {
            //_dbContext.Notifications.Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Notifications.FindAsync(entity.NotificationID);
        }

        public async Task<(bool, string)> DeleteAsync(Notification entity)
        {
            //_dbContext.Notifications.Remove(entity);
            //_dbContext.SaveChanges();

            try
            {
                var record = await _dbContext.Notifications.FindAsync(entity.NotificationID);

                if (record == null)
                {
                    return (false, $"{nameof(Notification)} could not be found");
                }

                _dbContext.Notifications.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return (true, $"{nameof(Notification)} got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }
        #endregion
    }
}