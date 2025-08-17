using LocalServicesDirectory.Application;
using LocalServicesDirectory.Infrastructure;
using LocalServicesDirectory.Infrastructure.Persistence;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Local Services Directory API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Local Services Directory API v1");
        c.RoutePrefix = string.Empty;
    });
}

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<LocalServicesContext>();
    DbInitializer.Seed(ctx);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
