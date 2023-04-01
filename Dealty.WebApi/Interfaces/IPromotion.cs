using Dealty.WebApi.Data;

namespace Dealty.WebApi.Interfaces
{
    public interface IPromotionRepository : IRepository<Promotion>
    {
    }

    public interface IPromotionRepositoryAsync : IRepositoryAsync<Promotion>
    {
    }
}