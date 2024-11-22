using Microsoft.Extensions.DependencyInjection;

namespace FzBz.Domain;

public static class ConfigurationExtension
{
    public static IServiceCollection AddFzBz(this IServiceCollection services)
    {
        services.AddScoped<IFzBzService, FzBzService>();
        return services;
    }
}
