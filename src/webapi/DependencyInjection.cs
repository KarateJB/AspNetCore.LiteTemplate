using application.Interfaces;
using application.Mapping;
using application.Services;
using MediatR;
using webapi.Filters;

namespace webapi;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<HttpRequestLogFilter>();
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(MemberApiProfile).Assembly);
        services.AddScoped<IMemberService, MemberService>();

        return services;
    }
}
