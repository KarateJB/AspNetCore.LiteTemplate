using Microsoft.FeatureManagement;
using webapi.Filters;

namespace webapi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddFeatureManagement();
        services.AddScoped<HttpRequestLogFilter>();

        return services;
    }
}
