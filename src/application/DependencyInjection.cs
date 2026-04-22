using application.Interfaces;
using application.Mapping;
using application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(options =>
        {
            // Change the Handlers lifetime to Scoped (default: Singleton) to inject scoped/transient services.
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });
        services.AddSingleton<MemberMapper>();
        services.AddScoped<IMemberService, MemberService>();

        return services;
    }
}
