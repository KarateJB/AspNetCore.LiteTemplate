using NLog;
using application;
using infrastructure;
using webapi.Extensions;
using webapi.Filters;
using webapi;

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
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddWebApi();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
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
