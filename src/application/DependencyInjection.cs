using application.Features.Members.Handlers;
using application.Interfaces;
using application.Mapping;
using application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly, typeof(FindMemberQueryHandler).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(MemberApiProfile).Assembly);
        services.AddScoped<IMemberService, MemberService>();

        return services;
    }
}
