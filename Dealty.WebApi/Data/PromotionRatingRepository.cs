﻿using Dealty.Shared.Data;
using Dealty.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dealty.WebApi.Data
{
    public class PromotionRatingRepository : IPromotionRatingRepository
    {
        private readonly DealtyDBContext _dbContext;

        public PromotionRatingRepository(DealtyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<PromotionRating> GetAll()
        {
            return _dbContext.PromotionRatings.ToList();
        }

        public PromotionRating GetById(int id)
        {
            return _dbContext.PromotionRatings.Find(id);
        }

        public void Add(PromotionRating entity)
        {
            _dbContext.PromotionRatings.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(PromotionRating entity)
        {
            _dbContext.PromotionRatings.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(PromotionRating entity)
        {
            _dbContext.PromotionRatings.Remove(entity);
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