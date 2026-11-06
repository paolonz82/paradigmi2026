using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Unicam.Paradigmi.GestoreMusicale.Application.Abstractions;
using Unicam.Paradigmi.GestoreMusicale.Application.Services;

namespace Unicam.Paradigmi.GestoreMusicale.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAlbumService, AlbumService>();
            return services;
        }
    }
}
