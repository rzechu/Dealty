using Dealty.WebApi.Data;

namespace Dealty.WebApi.Interfaces
{
    public interface IPromotionRatingRepository : IRepository<PromotionRating>
    {
    }

    public interface IPromotionRatingRepositoryAsync : IRepositoryAsync<PromotionRating>
    {
    }
}