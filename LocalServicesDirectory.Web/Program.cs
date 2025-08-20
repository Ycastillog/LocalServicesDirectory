using LocalServicesDirectory.Web;
using LocalServicesDirectory.Web.Components;
using LocalServicesDirectory.Web.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddHttpClient<ServiceApiClient>(client =>
{
    
    var baseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7075";
    client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();



