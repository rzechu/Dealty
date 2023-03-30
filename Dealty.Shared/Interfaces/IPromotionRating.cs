using Dealty.Shared.Data;

namespace Dealty.Shared.Interfaces
{
    public interface IPromotionRatingRepository : IRepository<PromotionRating>
    {
    }

    public interface IPromotionRatingRepositoryAsync : IRepositoryAsync<PromotionRating>
    {
    }
}