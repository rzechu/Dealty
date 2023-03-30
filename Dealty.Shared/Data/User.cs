namespace Dealty.Shared.Data
{
    public class User
    {
        public int UserID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<PromotionRating> PromotionRatings { get; set; }
        public virtual ICollection<UserAlert> UserAlerts { get; set; }
    }
}
