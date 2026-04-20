using application.Interfaces;
using infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shared.Configurations;

namespace infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration);
        services.AddScoped<IMemberRepository, MemberRepository>();

        return services;
    }
}
