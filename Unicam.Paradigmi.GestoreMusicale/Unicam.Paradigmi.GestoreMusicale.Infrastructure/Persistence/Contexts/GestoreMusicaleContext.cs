using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using Unicam.Paradigmi.GestoreMusicale.Domain.Entities;
using Unicam.Paradigmi.GestoreMusicale.Infrastructure.Persistence.Configurations;
using Unicam.Paradigmi.GestoreMusicale.Infrastructure.Persistence.Seeding;

namespace Unicam.Paradigmi.GestoreMusicale.Infrastructure.Persistence.Contexts
{
    public class GestoreMusicaleContext : DbContext
    {
        public GestoreMusicaleContext()
        {
            
        }
        public GestoreMusicaleContext(DbContextOptions<GestoreMusicaleContext> options) : base(options)
        {
            
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artista> Artisti { get; set; }
        public DbSet<Genere> Generi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSeeding((context, data) =>
            {
                var myContext = (GestoreMusicaleContext)context;
                var seeder = new GestoreMusicaleSeeding(myContext);
                seeder.Seed();
            });
            optionsBuilder.UseSqlServer("Server=LOCALHOST;Database=GestoreMusicale;Trusted_Connection=True;Trust Server Certificate=true"
                ,b=> b.MigrationsAssembly("Unicam.Paradigmi.GestoreMusicale.Infrastructure"));
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Posso applicare le configurazioni una alla volta
            /*
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistaConfiguration());
            modelBuilder.ApplyConfiguration(new GenereConfiguration());
            */
            //Con questa istruzione applico tutte le configurazione che si trovano all'interno dell'assembly del Contesto
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestoreMusicaleContext).Assembly);
        }
    }
}
