
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Application.Data;


namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureService
            (
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            //This Method is dependent on Mediator So register befor this, Using clean arc you shoudl do it on: AddApplicationService 
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();

            services.AddDbContext<ApplicatinDbContext>((sp,o) =>
            {
                o.AddInterceptors(sp.GetService<ISaveChangesInterceptor>());

                o.UseSqlServer(connectionString);
             });
            services.AddScoped<IApplicationDbContext, ApplicatinDbContext>();
            return services;
        }
    }
}
