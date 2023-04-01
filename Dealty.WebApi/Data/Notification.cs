namespace Dealty.WebApi.Data
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public int PromotionID { get; set; }
        public string? NotificationText { get; set; }
    }
}
