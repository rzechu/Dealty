namespace Dealty.Shared.Data
{
    public class PromotionRating
    {
        public int PromotionRatingID { get; set; }
        public int PromotionID { get; set; }
        public int UserID { get; set; }
        public int Vote { get; set; }
        public virtual Promotion Promotion { get; set; }
    }
}
