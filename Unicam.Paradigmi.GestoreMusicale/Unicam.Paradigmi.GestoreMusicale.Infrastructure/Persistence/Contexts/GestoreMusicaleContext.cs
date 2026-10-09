using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Unicam.Paradigmi.GestoreMusicale.Domain.Entities;

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
            optionsBuilder.UseSqlServer("Server=LOCALHOST;Database=GestoreMusicale;Trusted_Connection=True;Trust Server Certificate=true"
                ,b=> b.MigrationsAssembly("Unicam.Paradigmi.GestoreMusicale.Infrastructure"));
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Album>().HasKey(x => x.IdAlbum);
            modelBuilder.Entity<Album>().ToTable("Albums");
            modelBuilder.Entity<Album>().Property(p => p.Nome)
                .HasColumnName("NomeAlbum")
                .HasColumnType("varchar")
                .HasMaxLength(50);
            modelBuilder.Entity<Album>().Property(p => p.Descrizione)
                .HasColumnName("DescrizioneAlbum")
                .HasColumnType("varchar");
            modelBuilder.Entity<Album>()
                .HasOne(x => x.Artista)
                .WithMany(x => x.Albums)
                .HasForeignKey(x => x.IdArtista);
            modelBuilder.Entity<Album>()
                .HasMany(x => x.Generi)
                .WithMany(x => x.Albums)
                .UsingEntity<AlbumGenere>(
                    "AlbumGenere",
                    j => j.HasOne<Genere>().WithMany().HasForeignKey(x=>x.IdGenere),
                    j => j.HasOne<Album>().WithMany().HasForeignKey(x=>x.IdAlbum));

            modelBuilder.Entity<Artista>().HasKey(x => x.IdArtista);
            modelBuilder.Entity<Artista>().ToTable("Artisti");
            modelBuilder.Entity<Genere>().HasKey(x => x.IdGenere);
            modelBuilder.Entity<Genere>().ToTable("Generi");
        }
    }
}
