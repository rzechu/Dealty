using Dealty.Shared.Data;
using Dealty.Shared.Filters;
using Dealty.WebApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dealty.WebApi.Data
{
    public class PromotionRepository : IPromotionRepository, IPromotionRepositoryAsync
    {
        private readonly DealtyDBContext _dbContext;

        public PromotionRepository(DealtyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Promotion> GetAll()
        {
            return _dbContext.Promotions.ToList();
        }

        public Promotion GetById(int id)
        {
            return _dbContext.Promotions.Find(id);
        }

        public void Add(Promotion entity)
        {
            _dbContext.Promotions.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Promotion entity)
        {
            _dbContext.Promotions.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Promotion entity)
        {
            _dbContext.Promotions.Remove(entity);
            _dbContext.SaveChanges();
        }

        #region Async
        public async Task<IEnumerable<Promotion>> GetAllAsync()
        {
            return await _dbContext.Promotions.ToListAsync();
        }

        public async Task<Promotion> GetByIdAsync(int id)
        {
            return await _dbContext.Promotions.FindAsync(id);
        }

        public async Task<Promotion> AddAsync(Promotion entity)
        {
            await _dbContext.Promotions.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Promotions.FindAsync(entity.PromotionID);
        }

        public async Task<Promotion> UpdateAsync(Promotion entity)
        {
            //_dbContext.Promotions.Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Promotions.FindAsync(entity.PromotionID);
        }

        public async Task<(bool, string)> DeleteAsync(Promotion entity)
        {
            //_dbContext.Promotions.Remove(entity);
            //_dbContext.SaveChanges();

            try
            {
                var record = await _dbContext.Promotions.FindAsync(entity.PromotionID);

                if (record == null)
                {
                    return (false, $"{nameof(Promotion)} could not be found");
                }

                _dbContext.Promotions.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return (true, $"{nameof(Promotion)} got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        public async Task<(IEnumerable<Promotion>, int)> GetAllPaginatedAsync(PaginationFilter paginationFilter)
        {
            try
            {
                int totalCount = await _dbContext.Promotions.CountAsync();
                var dbRecords = await _dbContext.Promotions.Skip(paginationFilter.Offset).Take(paginationFilter.PageSize).ToListAsync();
                return (dbRecords, totalCount);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

    }
}