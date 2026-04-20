using infrastructure;
using NLog;
using webapi;
using webapi.Extensions;
using webapi.Filters;

var logger = LogManager.Setup()
    .LoadConfigurationFromFile("NLog.config", optional: true)
    .GetCurrentClassLogger();

try
{
    logger.Debug("Starting program...");
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.AddCustomConfiguration(args);
    builder.AddNLogLogging();

    builder.Services.AddControllers(options => options.Filters.AddService<HttpRequestLogFilter>());
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        builder.Configuration.AddUserSecrets<Program>(optional: true);
    }

    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Application stopped because of an exception.");
    throw;
}
finally
{
    LogManager.Shutdown();
}
