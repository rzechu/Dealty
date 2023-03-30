namespace Dealty.Shared.Data
{
    public class Promotion
    {
        public int PromotionID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CountryID { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<PromotionPhoto> PromotionPhotos { get; set; }
        public virtual ICollection<PromotionRating> PromotionRatings { get; set; }
    }
}