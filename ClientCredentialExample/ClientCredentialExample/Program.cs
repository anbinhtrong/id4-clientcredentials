using ClientCredentialExample.Services;
using IdentityModel.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:9000/";
        options.Audience = "ApiResource1";
    });
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IDiscoveryCache>(r =>
{
    var factory = r.GetRequiredService<IHttpClientFactory>();
    return new DiscoveryCache("https://localhost:9000/", () => factory.CreateClient());
});
builder.Services.AddTransient<IAuthServerService, AuthServerService>();
var app = builder.Build();

// Configure the HTTP request pipeline.B
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
