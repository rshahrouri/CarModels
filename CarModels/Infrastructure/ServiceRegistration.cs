using CarModels.Services.Interfaces;
using System.Reflection;

namespace CarModels.Infrastructure;

/// <summary>
/// Provides extension methods for configuring services in the DI container.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Adds services to the DI container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The same instance of the <see cref="IServiceCollection"/> to allow method chaining.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer()
            .AddSwaggerGen();

        services.AddHttpClient("CarModelsClient", client =>
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("Settings:GetModelsForMakeIdYearApi:BaseAddress"));
        });

        services.AddBusinessServices();

        return services;
    }

    /// <summary>
    /// Adds business services to the DI container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The same instance of the <see cref="IServiceCollection"/> to allow method chaining.</returns>
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        // Get all types in the current assembly that implement IService
        var serviceTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsClass && type.IsAbstract == false && type.GetInterfaces().Any(i => i == typeof(IService)));

        // Register each service type
        foreach (var serviceType in serviceTypes)
        {
            var interfaceType = serviceType.GetInterfaces()
                .Except(new List<Type>() { typeof(IService) })
                .FirstOrDefault();

            if (interfaceType is not null)
            {
                services.AddScoped(interfaceType, serviceType);
            }
        }

        return services;
    }
}
