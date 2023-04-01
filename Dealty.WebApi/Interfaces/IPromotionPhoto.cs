using Dealty.WebApi.Data;

namespace Dealty.WebApi.Interfaces
{
    public interface IPromotionPhotoRepository : IRepository<PromotionPhoto>
    {
    }

    public interface IPromotionPhotoRepositoryAsync : IRepositoryAsync<PromotionPhoto>
    {
    }
}