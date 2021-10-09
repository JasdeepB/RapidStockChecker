using Microsoft.EntityFrameworkCore;
using RSC.Models;

namespace RapidStockChecker.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Discord> Discord { get; set; }
        public virtual DbSet<RSC.Models.Type> Types { get; set; }
        public virtual DbSet<RestockHistory> RestockHistory { get; set; }
    }
}
