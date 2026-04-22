using NLog.Web;

namespace webapi.Extensions;

public static class LoggingExtension
{
    public static WebApplicationBuilder AddNLogLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();
        return builder;
    }
}
