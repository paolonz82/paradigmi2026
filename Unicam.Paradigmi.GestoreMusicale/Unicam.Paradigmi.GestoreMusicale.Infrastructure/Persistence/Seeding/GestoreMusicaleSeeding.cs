using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Unicam.Paradigmi.GestoreMusicale.Domain.Entities;
using Unicam.Paradigmi.GestoreMusicale.Infrastructure.Persistence.Contexts;

namespace Unicam.Paradigmi.GestoreMusicale.Infrastructure.Persistence.Seeding
{
    public class GestoreMusicaleSeeding
    {
        private readonly GestoreMusicaleContext _context;
        public GestoreMusicaleSeeding(GestoreMusicaleContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            InizializzaGeneri();
            InizializzaArtisti();
            InizializzaAlbum();
        }

        private void InizializzaAlbum()
        {
            if (!_context.Albums.Any())
            {
                // Abilita IDENTITY_INSERT
                _context.Database.ExecuteSql($"SET IDENTITY_INSERT Albums ON");

                var album = new List<(string Nome, string Descrizione, DateTime DataUscita, decimal Prezzo, string Isbn, int IdArtista, List<int> IdGeneri)>()
                {
                    ("Abbey Road", "Capolavoro dei Beatles del 1969", new DateTime(1969, 9, 26), 15.99m, "978-0094574", 1, new List<int> { 1 }),
                    ("Sgt. Pepper's Lonely Hearts Club Band", "Album rivoluzionario dei Beatles", new DateTime(1967, 6, 1), 14.99m, "978-0094575", 1, new List<int> { 1 }),
                    ("Blue Hawaii", "Colonna sonora con Elvis", new DateTime(1961, 10, 20), 12.99m, "978-0094576", 2, new List<int> { 2 }),
                    ("Thriller", "L'album più venduto di Michael Jackson", new DateTime(1982, 11, 30), 16.99m, "978-0094577", 3, new List<int> { 2 }),
                    ("Heroes", "Capolavoro di David Bowie", new DateTime(1977, 10, 14), 13.99m, "978-0094578", 4, new List<int> { 1 }),
                    ("Bohemian Rhapsody", "Album iconico dei Queen", new DateTime(1975, 10, 31), 14.99m, "978-0094579", 5, new List<int> { 1, 2 }),
                    ("The Wall", "Doppio album dei Pink Floyd", new DateTime(1979, 11, 30), 17.99m, "978-0094580", 6, new List<int> { 1 }),
                    ("IV", "Il quarto album dei Led Zeppelin", new DateTime(1971, 11, 8), 15.99m, "978-0094581", 7, new List<int> { 1 }),
                    ("Sticky Fingers", "Album dei Rolling Stones", new DateTime(1971, 4, 23), 14.99m, "978-0094582", 8, new List<int> { 1 }),
                    ("The Marshall Mathers LP", "Album di Eminem", new DateTime(2000, 5, 23), 13.99m, "978-0094583", 9, new List<int> { 3 }),
                    ("Recovery", "Secondo album di Eminem", new DateTime(2010, 6, 18), 12.99m, "978-0094584", 9, new List<int> { 3 }),
                    ("When We All Fall Asleep, Where Do We Go?", "Debut di Billie Eilish", new DateTime(2019, 3, 29), 11.99m, "978-0094585", 10, new List<int> { 3, 2 }),
                    ("Jazz", "Album dei Queen", new DateTime(1978, 11, 10), 14.99m, "978-0094586", 5, new List<int> { 1 }),
                    ("The Dark Side of the Moon", "Masterpiece dei Pink Floyd", new DateTime(1973, 3, 1), 16.99m, "978-0094587", 6, new List<int> { 5, 1 }),
                    ("Space Oddity", "Album di David Bowie", new DateTime(1969, 8, 14), 13.99m, "978-0094588", 4, new List<int> { 1 })
                };

                int i = 1;
                foreach (var item in album)
                {
                    var nuovoAlbum = new Album
                    {
                        IdAlbum = i,
                        Nome = item.Nome,
                        Descrizione = item.Descrizione,
                        DataUscita = item.DataUscita,
                        Prezzo = item.Prezzo,
                        Isbn = item.Isbn,
                        IdArtista = item.IdArtista
                    };

                    // Aggiungi i generi
                    foreach (var idGenere in item.IdGeneri)
                    {
                        var genere = _context.Generi.FirstOrDefault(g => g.IdGenere == idGenere);
                        if (genere != null)
                        {
                            nuovoAlbum.Generi.Add(genere);
                        }
                    }

                    _context.Albums.Add(nuovoAlbum);
                    i++;
                }
                _context.SaveChanges();

                // Disabilita IDENTITY_INSERT
                _context.Database.ExecuteSql($"SET IDENTITY_INSERT Albums OFF");
            }
        }

        private void InizializzaArtisti()
        {
            if (!_context.Artisti.Any())
            {
                // Abilita IDENTITY_INSERT
                _context.Database.ExecuteSql($"SET IDENTITY_INSERT Artisti ON");

                var artisti = new List<string>()
                {
                    "The Beatles",
                    "Elvis Presley",
                    "Michael Jackson",
                    "David Bowie",
                    "Queen",
                    "Pink Floyd",
                    "Led Zeppelin",
                    "The Rolling Stones",
                    "Eminem",
                    "Billie Eilish"
                };

                int i = 1;
                foreach (var nomeArtista in artisti)
                {
                    var artista = new Artista();
                    artista.IdArtista = i;
                    artista.NomeArtista = nomeArtista;
                    _context.Artisti.Add(artista);
                    i++;
                }
                _context.SaveChanges();

                // Disabilita IDENTITY_INSERT
                _context.Database.ExecuteSql($"SET IDENTITY_INSERT Artisti OFF");
            }
        }

        private void InizializzaGeneri()
        {
            if (!_context.Generi.Any())
            {
                var list = new List<string>() { "Rock and Roll", "Pop", "Rap", "Techno", "Classica" };

                // Abilita IDENTITY_INSERT
                _context.Database.ExecuteSql($"SET IDENTITY_INSERT Generi ON");
                int i = 1;
                foreach(var genere in list)
                {
                    var item = new Genere();
                    item.IdGenere = i;
                    item.NomeGenere = genere;
                    _context.Generi.Add(item);
                    i++;
                }
                _context.SaveChanges();

                // Disabilita IDENTITY_INSERT
                _context.Database.ExecuteSql($"SET IDENTITY_INSERT Generi OFF");
            }

        }
    }
}
