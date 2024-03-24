using Core.Application.Services;
using Core.Domain.Abstractions;
using Infra.Data.DataContext;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

#nullable disable
namespace Infra.IoC.DataConfig
{
    public static class DataConfig
    {
        public static IServiceCollection AddDataConfig(this IServiceCollection services, IConfiguration configuration)
        {

            services
                   .AddDbContextPool<ApplicationDbContext>(opts => opts
                   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                   .UseSqlServer(configuration
                   .GetConnectionString("SQLConnection"), b => b
                   .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            services
                .TryAddScoped(typeof(IProductRepository), typeof(ProductRepository));

            services
                .TryAddScoped(typeof(IProductService), typeof(ProductService));

            return services;

        }

    }
}
