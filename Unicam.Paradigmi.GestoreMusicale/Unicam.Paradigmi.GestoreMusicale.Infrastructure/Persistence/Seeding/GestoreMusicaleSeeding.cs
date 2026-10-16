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
            
        }

        private void InizializzaArtisti()
        {
            if (!_context.Generi.Any())
            {
             
                // Abilita IDENTITY_INSERT
                _context.Database.ExecuteSql($"SET IDENTITY_INSERT Artisti ON");
                var artista = new Artista();
                artista.IdArtista = 1;
                artista.NomeArtista = "The Beatles";
                _context.Artisti.Add(artista);
                var artista2 = new Artista();
                artista2.IdArtista = 1;
                artista2.NomeArtista = "The Beatles";
                _context.Artisti.Add(artista2);
                _context.SaveChanges();

                // Disabilita IDENTITY_INSERT
                _context.Database.ExecuteSql($"SET IDENTITY_INSERT Generi OFF");
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
