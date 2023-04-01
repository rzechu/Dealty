using Dealty.WebApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dealty.WebApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DealtyDBContext _dbContext;

        public UserRepository(DealtyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public User GetById(int id)
        {
            return _dbContext.Users.Find(id);
        }

        public void Add(User entity)
        {
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(User entity)
        {
            _dbContext.Users.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            _dbContext.Users.Remove(entity);
            _dbContext.SaveChanges();
        }

        #region Async
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> AddAsync(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Users.FindAsync(entity.UserID);
        }

        public async Task<User> UpdateAsync(User entity)
        {
            //_dbContext.Users.Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Users.FindAsync(entity.UserID);
        }

        public async Task<(bool, string)> DeleteAsync(User entity)
        {
            //_dbContext.Users.Remove(entity);
            //_dbContext.SaveChanges();

            try
            {
                var record = await _dbContext.Users.FindAsync(entity.UserID);

                if (record == null)
                {
                    return (false, $"{nameof(User)} could not be found");
                }

                _dbContext.Users.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return (true, $"{nameof(User)} got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }
        #endregion
    }
}