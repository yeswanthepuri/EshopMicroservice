using Ordering.Application.Data;
using Ordering.Domain.Models;
using System.Reflection;


namespace Ordering.Infrastructure.Data
{
    public class ApplicatinDbContext : DbContext , IApplicationDbContext
    {
        public ApplicatinDbContext
            (DbContextOptions<ApplicatinDbContext> options) : base(options)
        { }

        public DbSet<Customer> Customers  => Set<Customer>();
        public DbSet<Product> Products  => Set<Product>();
        public DbSet<Order> Orders  => Set<Order>();
        public DbSet<OrderItem> OrderItems  => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //This will get all the assembly 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}


