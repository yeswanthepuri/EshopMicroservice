

using BuildingBlocks.Exceptions.Handler;
using Carter;

namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiService(this IServiceCollection services) {
            services.AddCarter();
            services.AddExceptionHandler<CustomExceptionHandler>();
            return services;
        }
    
        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();
            app.UseExceptionHandler(opt => { });
            return app;
        }
    }
}



