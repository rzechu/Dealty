using Dealty.Shared.Data;
using Dealty.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dealty.WebApi.Data
{
    public class CategoryRepository : ICategoryRepository, ICategoryRepositoryAsync
    {
        private readonly DealtyDBContext _dbContext;

        public CategoryRepository(DealtyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _dbContext.Categories.Find(id);
        }

        public void Add(Category entity)
        {
            _dbContext.Categories.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Category entity)
        {
            _dbContext.Categories.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Category entity)
        {
            _dbContext.Categories.Remove(entity);
            _dbContext.SaveChanges();
        }

        #region Async
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<Category> AddAsync(Category entity)
        {
            await _dbContext.Categories.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Categories.FindAsync(entity.CategoryID);
        }

        public async Task<Category> UpdateAsync(Category entity)
        {
            //_dbContext.Categories.Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified; 
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Categories.FindAsync(entity.CategoryID);
        }

        public async Task<(bool, string)> DeleteAsync(Category entity)
        {
            //_dbContext.Categories.Remove(entity);
            //_dbContext.SaveChanges();

            try
            {
                var record = await _dbContext.Categories.FindAsync(entity.CategoryID);

                if (record == null)
                {
                    return (false, $"{nameof(Category)} could not be found");
                }

                _dbContext.Categories.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return (true, $"{nameof(Category)} got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }
        #endregion
    }
}