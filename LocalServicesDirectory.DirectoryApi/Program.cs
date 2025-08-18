using LocalServicesDirectory.Infrastructure;
using LocalServicesDirectory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructureServices(builder.Configuration);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Local Services Directory API",
        Version = "v1",
        Description = "API para el Directorio de Servicios Locales"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<LocalServicesContext>();
    ctx.Database.Migrate();
    DbInitializer.Seed(ctx);
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorApp");

app.MapControllers();

app.Run();


using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<LocalServicesContext>();
    ctx.Database.Migrate();       
    DbInitializer.Seed(ctx);      

app.UseHttpsRedirection();
app.UseCors("AllowBlazorApp");
app.MapControllers();
    app.Run();
    }