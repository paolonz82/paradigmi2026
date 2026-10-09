using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Unicam.Paradigmi.FirstConsole;

public partial class GestoreMusicaleContext : DbContext
{
    public GestoreMusicaleContext()
    {
    }

    public GestoreMusicaleContext(DbContextOptions<GestoreMusicaleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<AlbumsGeneri> AlbumsGeneris { get; set; }

    public virtual DbSet<Artisti> Artistis { get; set; }

    public virtual DbSet<Generi> Generis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LOCALHOST;Database=GestoreMusicale;Trusted_Connection=True;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.IdAlbum);

            entity.Property(e => e.DataUscita).HasColumnType("datetime");
            entity.Property(e => e.DescrizioneAlbum).IsUnicode(false);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NomeAlbum)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Prezzo).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdArtistaNavigation).WithMany(p => p.Albums)
                .HasForeignKey(d => d.IdArtista)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Albums_Artisti");
        });

        modelBuilder.Entity<AlbumsGeneri>(entity =>
        {
            entity.HasKey(e => new { e.IdAlbum, e.IdGenere });

            entity.ToTable("AlbumsGeneri");

            entity.HasOne(d => d.IdAlbumNavigation).WithMany(p => p.AlbumsGeneris)
                .HasForeignKey(d => d.IdAlbum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumsGeneri_Albums");

            entity.HasOne(d => d.IdAlbum1).WithMany(p => p.AlbumsGeneris)
                .HasForeignKey(d => d.IdAlbum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumsGeneri_Generi");
        });

        modelBuilder.Entity<Artisti>(entity =>
        {
            entity.HasKey(e => e.IdArtista);

            entity.ToTable("Artisti");

            entity.Property(e => e.NomeArtista)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Generi>(entity =>
        {
            entity.HasKey(e => e.IdGenere);

            entity.ToTable("Generi");

            entity.Property(e => e.NomeGenere)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
