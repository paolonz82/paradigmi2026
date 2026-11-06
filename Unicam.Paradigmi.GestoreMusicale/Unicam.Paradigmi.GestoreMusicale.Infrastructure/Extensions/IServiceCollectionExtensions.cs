using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Unicam.Paradigmi.GestoreMusicale.Infrastructure.Persistence.Contexts;
using Unicam.Paradigmi.GestoreMusicale.Infrastructure.Persistence.Seeding;

namespace Unicam.Paradigmi.GestoreMusicale.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GestoreMusicaleContext>(opt =>
            {

                //optionsBuilder.UseLazyLoadingProxies();
                opt.UseSeeding((context, data) =>
                {
                    var myContext = (GestoreMusicaleContext)context;
                    var seeder = new GestoreMusicaleSeeding(myContext);
                    seeder.Seed();
                });
                opt.UseSqlServer(configuration.GetConnectionString("GestoreMusicaleContext")
                    , b => b.MigrationsAssembly("Unicam.Paradigmi.GestoreMusicale.Infrastructure"));
            });
            return services;
        }
    }
}
