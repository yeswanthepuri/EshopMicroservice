using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options)
            :base(options)
        {
        }
        public DbSet<Coupon> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id= 1, ProductName="Iphone10", Amount=10,Description="Get 10% off on iphone"},
                new Coupon { Id= 2, ProductName="samsung15", Amount=15,Description="Get 15% off on s24"}
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
