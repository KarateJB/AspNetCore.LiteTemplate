using application;
using infrastructure;
using webapi.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MemberApiProfile).Assembly);

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
