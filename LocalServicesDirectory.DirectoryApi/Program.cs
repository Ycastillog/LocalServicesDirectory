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
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Local Services Directory API",
        Version = "v1",
        Description = "API para la gestión del Directorio de Servicios Locales"
    });
});


const string DevCors = "_devCors";
builder.Services.AddCors(opts =>
{
    opts.AddPolicy(DevCors, p => p
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Local Services Directory API v1");
    c.RoutePrefix = "swagger"; 
});


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LocalServicesContext>();
    context.Database.EnsureCreated(); 
}


app.UseHttpsRedirection();


app.UseCors(DevCors);

app.UseAuthorization();
app.MapControllers();

app.Run();
