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

    builder.Services.AddControllers(opts =>
    {
        opts.Filters.AddService<HttpRequestLogFilter>();
    }).AddNewtonsoftJson(opts =>
    {
        opts.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
        opts.UseCamelCasing(true); // Use camelCase for JSON properties
        // opts.UseMemberCasing(); // Use the original casing of the C# properties
    });
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
