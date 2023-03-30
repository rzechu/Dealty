using Dealty.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace Dealty.WebApi.Data
{
    public class DealtyDBContext : DbContext
    {
        public DealtyDBContext(DbContextOptions<DealtyDBContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionPhoto> PromotionPhotos { get; set; }
        public DbSet<PromotionRating> PromotionRatings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAlert> UserAlerts { get; set; }
    }
}