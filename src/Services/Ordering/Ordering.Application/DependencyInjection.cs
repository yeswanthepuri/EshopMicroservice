using BuildingBlocks.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;
using System.Reflection;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {

                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            return services;
        }
    }
}
