using Dealty.Shared.Data;
using Dealty.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dealty.WebApi.Data
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DealtyDBContext _dbContext;

        public CountryRepository(DealtyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Country> GetAll()
        {
            return _dbContext.Countries.ToList();
        }

        public Country GetById(int id)
        {
            return _dbContext.Countries.Find(id);
        }

        public void Add(Country entity)
        {
            _dbContext.Countries.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Country entity)
        {
            _dbContext.Countries.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Country entity)
        {
            _dbContext.Countries.Remove(entity);
            _dbContext.SaveChanges();
        }

        #region Async
        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _dbContext.Countries.ToListAsync();
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await _dbContext.Countries.FindAsync(id);
        }

        public async Task<Country> AddAsync(Country entity)
        {
            await _dbContext.Countries.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Countries.FindAsync(entity.CountryID);
        }

        public async Task<Country> UpdateAsync(Country entity)
        {
            //_dbContext.Countries.Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Countries.FindAsync(entity.CountryID);
        }

        public async Task<(bool, string)> DeleteAsync(Country entity)
        {
            //_dbContext.Countries.Remove(entity);
            //_dbContext.SaveChanges();

            try
            {
                var record = await _dbContext.Countries.FindAsync(entity.CountryID);

                if (record == null)
                {
                    return (false, $"{nameof(Country)} could not be found");
                }

                _dbContext.Countries.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return (true, $"{nameof(Country)} got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }
        #endregion
    }
}