using Dealty.Shared.Data;

namespace Dealty.Shared.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
    }

    public interface ICountryRepositoryAsync : IRepositoryAsync<Country>
    {
    }
}