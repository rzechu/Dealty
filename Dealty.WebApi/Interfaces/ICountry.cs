using Dealty.WebApi.Data;

namespace Dealty.WebApi.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
    }

    public interface ICountryRepositoryAsync : IRepositoryAsync<Country>
    {
    }
}