using application.Interfaces;
using application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddScoped<IMemberService, MemberService>();

        return services;
    }
}
