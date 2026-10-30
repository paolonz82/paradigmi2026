using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Diagnostics;
using Unicam.Paradigmi.GestoreMusicale.Domain.Entities;
using Unicam.Paradigmi.GestoreMusicale.Infrastructure.Persistence.Contexts;

var context = new GestoreMusicaleContext();


GetNomeAlbumAsync(2);
string GetNomeAlbum(int id)
{
    return context.Albums.Where(w => w.IdAlbum == id)
        .Select(s => s.Nome).First();
}
async Task<string> GetNomeAlbumAsync(int id)
{
    
    return await context
            .Albums
            .Where(w => w.IdAlbum == id)
            .Select(s => s.Nome)
            .FirstAsync();
}
/*
List<long> list = new List<long>();
for (long i = 1; i <= 1000000000; i++)
{
    list.Add(i);
}
await AvvioOperazioneSingoloThread(list);
await AvvioOperazioneMultiThread(list);
*/
async Task AvvioOperazioneSingoloThread(List<long> list ) {

    var stopWatch = new Stopwatch();
    stopWatch.Start();
    var result = await Task<long>.Run(() =>
    {
        long result = 0;
        foreach (var item in list)
        {
            result += item * item;
        }
        return result;
    });
    Console.WriteLine($"Risultato singolo thread: {result} - Elapsed time : {stopWatch.Elapsed.TotalSeconds}");
}

async Task AvvioOperazioneMultiThread(List<long> list)
{
    var list1 = list.Where(x => x % 2 == 0).ToList();
    var list2 = list.Where(x => x % 2 == 1).ToList();
    var task1 = new Task<long>(() =>
    {
        long result = 0;
        foreach (var item in list1)
        {
            result += item * item;
        }
        return result;
    });
    var task2 = new Task< long>(() =>
    {
        long result = 0;
        foreach (var item in list2)
        {
            result += item * item;
        }
        return result;
    });
    var stopWatch = new Stopwatch();
    stopWatch.Start();
    task1.Start();
    task2.Start();
    var results = await Task.WhenAll(task1, task2);
  
    Console.WriteLine($"Risultato multi thread: {results.Sum()} - Elapsed time : {stopWatch.Elapsed.TotalSeconds}");
}

/*
 var task = await Task.Run<string>(() =>
{
    throw new Exception("Test Exception");
    System.Threading.Thread.Sleep(10000);
    Console.WriteLine("Task secondario completato");
    return "Output Task secondario";
});
*/

//Console.WriteLine($"Task Secondario : {task.Result}");
Console.WriteLine("Task primario in esecuzione");
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