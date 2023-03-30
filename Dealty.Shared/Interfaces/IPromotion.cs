using Dealty.Shared.Data;

namespace Dealty.Shared.Interfaces
{
    public interface IPromotionRepository : IRepository<Promotion>
    {
    }

    public interface IPromotionRepositoryAsync : IRepositoryAsync<Promotion>
    {
    }
}