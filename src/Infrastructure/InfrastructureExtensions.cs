using Application.Shared;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddDatabaseContext(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var cs = configuration["DB_CONNECTION_STRING"];
        services.AddDbContext<DbContext, AppDbContext>(options =>
        {
            options.UseMySql(cs, ServerVersion.AutoDetect(cs), b => b.MigrationsAssembly("WebApi"));
        });
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IOrderStatusRepository, OrderStatusRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
