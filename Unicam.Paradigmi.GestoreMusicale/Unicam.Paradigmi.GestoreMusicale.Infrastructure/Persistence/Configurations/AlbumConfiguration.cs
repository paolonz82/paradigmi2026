using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using Unicam.Paradigmi.GestoreMusicale.Domain.Entities;

namespace Unicam.Paradigmi.GestoreMusicale.Infrastructure.Persistence.Configurations
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(x => x.IdAlbum);
            builder.ToTable("Albums");
            builder.Property(p => p.Nome)
                .HasColumnName("NomeAlbum")
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(p => p.Descrizione)
                .HasColumnName("DescrizioneAlbum")
                .HasColumnType("varchar")
                .HasMaxLength(-1);
            builder
                .HasOne(x => x.Artista)
                .WithMany(x => x.Albums)
                .HasForeignKey(x => x.IdArtista);
            builder
                .HasMany(x => x.Generi)
                .WithMany(x => x.Albums)
                .UsingEntity<AlbumGenere>(
                    "AlbumGenere",
                    j => j.HasOne<Genere>().WithMany().HasForeignKey(x => x.IdGenere),
                    j => j.HasOne<Album>().WithMany().HasForeignKey(x => x.IdAlbum));

        }
    }
}
