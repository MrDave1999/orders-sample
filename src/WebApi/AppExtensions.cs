using Application.Features.Customers;

namespace WebApi;

public static class AppExtensions
{
    public static IServiceCollection RegisterUseCases(this IServiceCollection services)
    {
        services.Scan(scan => scan
        // Search the types from the specified assemblies.
            .FromAssemblies(typeof(CreateCustomerUseCase).Assembly)
              // Registers the concrete classes as a service, for example: 'CreateCustomerUseCase'.
              .AddClasses(classes => classes.Where(type => type.Name.EndsWith("UseCase")))
                .AsSelf()
                .WithTransientLifetime());

        return services;
    }
}
