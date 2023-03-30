using Dealty.Shared.Data;

namespace Dealty.Shared.Interfaces
{
    public interface IPromotionPhotoRepository : IRepository<PromotionPhoto>
    {
    }

    public interface IPromotionPhotoRepositoryAsync : IRepositoryAsync<PromotionPhoto>
    {
    }
}