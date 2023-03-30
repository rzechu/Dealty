namespace Dealty.Shared.Data
{
    public class UserAlert
    {
        public int UserAlertId { get; set; }
        public int UserID { get; set; }
        public string AlertText { get; set; }
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
        public bool IsPoland { get; set; }
        public bool IsEU { get; set; }
        public virtual User User { get; set; }
    }
}
