using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Unicam.Paradigmi.GestoreMusicale.Domain.Entities;
using Unicam.Paradigmi.GestoreMusicale.Infrastructure.Persistence.Contexts;

var context = new GestoreMusicaleContext();

LazyLoading();

void LazyLoading()
{
    var album = context.Albums
                .Single(w => w.IdAlbum == 2);

    Console.WriteLine($"Artista : {album.Artista.NomeArtista}");
}
void EagerLoading()
{
    var album = context.Albums
                .Include(i => i.Artista)
                .Include(i => i.Generi)
                .Single(w => w.IdAlbum == 2);

    Console.WriteLine($"Artista : {album.Artista?.NomeArtista}");
    Console.WriteLine($"Generi : {album.Generi.Count()}");

}
void ExplicitLoading()
{
    var album = context.Albums
        .Single(w => w.IdAlbum == 2);

    Console.WriteLine($"Artista : {album.Artista?.NomeArtista}");
    Console.WriteLine($"Generi : {album.Generi.Count()}");

    var entry = context.Entry(album);
    entry.Reference(r => r.Artista).Load();

    Console.WriteLine($"Artista dopo caricamento : {album.Artista?.NomeArtista}");

    entry.Collection(c => c.Generi).Load();

    Console.WriteLine($"Generi dopo caricamento : {album.Generi.Count()}");
}
void Tracciamento()
{
    var albums = context.Albums
        .AsNoTracking()
        .ToList();

    var album = new Album();
    album.IdAlbum = 2;
    album.Prezzo = 5;

    context.Entry(album).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    context.SaveChanges();

}


void ReadAndUpdate()
{
    var album = context.Albums
    .Where(w => w.IdAlbum == 2)
    .Single();

    album.Prezzo = (decimal)20.4;

    context.SaveChanges();
}

void Update()
{
    var album = new Album();
    album.IdAlbum = 2;
    album.Prezzo = (Decimal)18.2;

    var entry = context.Entry(album);
    //entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    entry.Property(p => p.Prezzo).IsModified = true;
    context.SaveChanges();
}
void Delete()
{
    var album = new Album();
    album.IdAlbum = 5;

    var entry = context.Entry(album);
    entry.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
    context.SaveChanges();
}

void Insert()
{
    var artista = new Artista();
    artista.NomeArtista = "Metallica";
    //context.Artisti.Add(artista);
    //context.SaveChanges();

    var album = new Album();
    album.Nome = "Black Album";
    album.DataUscita = new DateTime(1997, 1, 1);
    album.Descrizione = "Black Album description";
    album.Prezzo = (decimal)8.2;
    album.Isbn = "456123789";
    //album.IdArtista = artista.IdArtista;
    album.Artista = artista;


    var stateArtista = context.Entry(artista).State;
    var stateAlbum = context.Entry(album).State;
    context.Albums.Add(album);

    context.SaveChanges();
}

Console.ReadLine();