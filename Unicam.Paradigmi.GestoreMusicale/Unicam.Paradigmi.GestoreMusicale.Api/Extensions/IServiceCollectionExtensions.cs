using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Security.Claims;
using System.Text;
using Unicam.Paradigmi.GestoreMusicale.Api.Validators;
using Unicam.Paradigmi.GestoreMusicale.Application.Options;

namespace Unicam.Paradigmi.GestoreMusicale.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
        {
            IdentityModelEventSource.ShowPII = true;
            services.AddControllers(opt =>
            {
                opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            })
    .ConfigureApiBehaviorOptions(opt =>
    {
        opt.SuppressModelStateInvalidFilter = true;
    });

            services.AddValidatorsFromAssemblyContaining<AggiungiAlbumRequestValidator>();
            services.AddFluentValidationAutoValidation();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();

            services.AddAuthentication(opt=>
            {
                opt.DefaultAuthenticateScheme = "Azure";                
            })
                .AddJwtBearer("Azure", opt =>
                {
                    opt.Authority = "https://login.microsoftonline.com/b974097e-5347-4135-8e56-9a78283a13f1/v2.0/";

                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://sts.windows.net/b974097e-5347-4135-8e56-9a78283a13f1/",
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,
                        ValidAudience = "api://8f09de98-e3b8-4687-87c1-b52123f03b25",
                        NameClaimType = "my_custom_claim"
                    };
                    opt.Events.OnTokenValidated = async (context) =>
                    {
                        var principal = context.Principal as ClaimsPrincipal;
                        var identity= principal.Identity as ClaimsIdentity;
                        identity.AddClaim(new Claim("my_custom_claim","test"));
                    };
                })
                .AddJwtBearer("Custom", opt =>
                {
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://gestoremusicale.unicam.it/idp",
                        ValidateIssuerSigningKey = false,
                        ValidateAudience = true,
                        ValidAudience = "https://gestoremusicale.unicam.it",
                        IssuerSigningKeyResolver = (string token, SecurityToken securityToken, string kid, TokenValidationParameters validationParameters) =>
                        {
                            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MiaChiaveMiaChiaveMiaChiaveMiaChiave"));
                            return new List<SecurityKey>() { securityKey };
                        }
                    };
                    opt.Events.OnTokenValidated = (context) =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        var principal = context.Principal as ClaimsPrincipal;
                        logger.LogInformation("Token Custom validato per l'utente: {Username}", principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                        return Task.CompletedTask;
                    };
                    opt.Events.OnAuthenticationFailed = (context) =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        logger.LogError("Autenticazione Custom fallita: {Exception}", context.Exception?.Message);
                        return Task.CompletedTask;
                    };
                    opt.Events.OnForbidden = (context) =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerEvents>>();
                        logger.LogWarning("Accesso negato per autenticazione Custom");
                        return Task.CompletedTask;
                    };
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("All", pol =>
                {
                    pol.RequireAuthenticatedUser();
                });

                opt.AddPolicy("OnlyValidUsers", pol =>
                {
                    pol.RequireAuthenticatedUser();
                    pol.RequireClaim("appid", "8f09de98-e3b8-4687-87c1-b52123f03b25");
                });
            });

            services.Configure<JwtOption>(configuration.GetSection("Jwt"));
            return services;
        }

   
    }
}
