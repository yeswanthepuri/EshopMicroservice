

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extensions
{
    public static class DataBaseExtensions
    {
        public static async Task InitialiseDataBaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicatinDbContext>();


            context.Database.MigrateAsync().GetAwaiter().GetResult();

            await SeedAsync(context);
        }

        private static async Task SeedAsync(ApplicatinDbContext context)
        {
            await SeedCustomerAsync(context);
            await SeedProductAsync(context);
            await SeedOrdersWithItemsAsync(context);
        }


        private static async Task SeedCustomerAsync(ApplicatinDbContext context)
        {
            if (!await context.Customers.AnyAsync())
            {
                await context.Customers.AddRangeAsync(InitialData.Customers);
                await context.SaveChangesAsync();
            }
        }
        private static async Task SeedProductAsync(ApplicatinDbContext context)
        {
            if (!await context.Products.AnyAsync())
            {
                await context.Products.AddRangeAsync(InitialData.Products);
                await context.SaveChangesAsync();
            }
        }
        private static async Task SeedOrdersWithItemsAsync(ApplicatinDbContext context)
        {
            if (!await context.Orders.AnyAsync())
            {
                await context.Orders.AddRangeAsync(InitialData.OrderwithItems);
                await context.SaveChangesAsync();
            }
        }
    }
}
