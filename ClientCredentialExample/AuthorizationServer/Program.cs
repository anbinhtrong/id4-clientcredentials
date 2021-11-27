using AuthorizationServer.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
    .AddInMemoryApiScopes(InMemoryConfig.ApiScopes)
    .AddInMemoryApiResources(InMemoryConfig.Apis)
    .AddInMemoryClients(InMemoryConfig.GetClients())
    .AddDeveloperSigningCredential();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
