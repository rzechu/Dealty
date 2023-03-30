namespace Dealty.Shared.Data
{
    public class PromotionPhoto
    {
        public int PromotionPhotoID { get; set; }
        public string PhotoURL { get; set; }
        public int PromotionID { get; set; }
        public virtual Promotion Promotion { get; set; }
    }
}
