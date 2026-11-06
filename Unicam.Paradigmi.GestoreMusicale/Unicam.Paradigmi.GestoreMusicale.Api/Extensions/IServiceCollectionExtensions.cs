using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Unicam.Paradigmi.GestoreMusicale.Api.Validators;
using Unicam.Paradigmi.GestoreMusicale.Application.Options;

namespace Unicam.Paradigmi.GestoreMusicale.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
        {
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

            services.Configure<JwtOption>(configuration.GetSection("Jwt"));
            return services;
        }
    }
}
