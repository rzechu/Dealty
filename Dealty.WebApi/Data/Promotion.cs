using System.ComponentModel.DataAnnotations.Schema;

namespace Dealty.WebApi.Data
{
    public class Promotion
    {
        public int PromotionID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        [ForeignKey(nameof(User))]
        public  int InsertedBy { get; set; }
        [ForeignKey(nameof(User))]
        public int UpdatedBy { get; set; }
        [ForeignKey(nameof(Country))]
        public int CountryID { get; set; }
        public virtual ICollection<PromotionPhoto>? PromotionPhotos { get; set; }
        public virtual ICollection<PromotionRating>? PromotionRatings { get; set; }
    }
}