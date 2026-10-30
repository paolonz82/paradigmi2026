using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using Unicam.Paradigmi.GestoreMusicale.Application.Abstractions;
using Unicam.Paradigmi.GestoreMusicale.Application.Services;
using Unicam.Paradigmi.GestoreMusicale.Infrastructure.Persistence.Contexts;
using Unicam.Paradigmi.GestoreMusicale.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<GestoreMusicaleContext>();
builder.Services.AddScoped<IUserService, InMemoryUserService>();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{ 
    opt.Cookie.Name = "my_cookie_auth";
    opt.LoginPath = "/Account/Login";
    opt.ExpireTimeSpan = TimeSpan.FromHours(1);
    opt.SlidingExpiration = true;
}).AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, opt =>
{
    opt.Authority = "https://login.microsoftonline.com/b974097e-5347-4135-8e56-9a78283a13f1/v2.0";
    opt.ClientId = "8f09de98-e3b8-4687-87c1-b52123f03b25";
    opt.ClientSecret = "ayy8Q~i6bA4plVIWcXbOGbov4jQjeTCtpq88xdz_";
    opt.SaveTokens = true;
    opt.CallbackPath = "/signin-oidc";
    opt.SignedOutCallbackPath = "/signed-out-oidc";
    opt.ResponseType = "code";
    opt.ResponseMode = "form_post";
    opt.Scope.Clear();
    opt.Scope.Add("openid");
    opt.Scope.Add("profile");
    opt.Scope.Add("email");
    opt.Scope.Add("offline_access");

    opt.GetClaimsFromUserInfoEndpoint = true;
    opt.TokenValidationParameters.NameClaimType = "name";

    opt.Events.OnTokenValidated = async (context) =>
    {
        var principal = context.Principal;
        var identity = principal.Identity as ClaimsIdentity;
        //identity.AddClaim(new Claim("role", "test"));
    };
});

builder.Services.AddAuthorization(opt =>
{
    /*var policyBuilder = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder();
    var policy = policyBuilder.RequireAuthenticatedUser().Build();
    opt.FallbackPolicy = policy;*/
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
/*
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Termino la pipeline");
});
*/

app.UseMiddleware<CustomLoggerMiddleware>();
app.Use(async (context, next) =>
{
    if (context.Items.ContainsKey("MyCustomItem"))
    {

    }
    //Logging
    if (context.Request.Path.ToString().Contains("test"))
    {
        await context.Response.WriteAsync("Inizio middleware");
        return;
    }
    await next.Invoke(context);
});

app.Map("/api", api =>
{
    api.Run(async (context) =>
    {
        await context.Response.WriteAsync("Siamo nel ramo dell'api");
    });
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();



app.Run();
