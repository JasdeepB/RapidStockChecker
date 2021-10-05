using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using RSC.Models;

namespace RapidStockChecker.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
            Database.Migrate();
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Discord> Discord { get; set; }
        public virtual DbSet<RSC.Models.Type> Types { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<RSC.Models.Models.Type>()
                //.HasOne(a => a.Discord)
                //.WithOne(b => b.Type)
                //.HasForeignKey<Discord>(b => b.TypeRef);

            modelBuilder.Entity<Discord>()
                .HasOne(a => a.Type)
                .WithOne(b => b.Discord)
                .HasForeignKey<RSC.Models.Type>(b => b.DiscordRef);
        }
    }
}
