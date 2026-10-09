using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unicam.Paradigmi.FirstConsole
{
    public class EfCoreService
    {
        public void Exec()
        {
            /*var optionsBuilder = new DbContextOptionsBuilder<GestoreMusicaleContext>();
            optionsBuilder.UseSqlServer("Server=LOCALHOST;Database=GestoreMusicale;Trusted_Connection=True;Trust Server Certificate=true");
            var context = new GestoreMusicaleContext(optionsBuilder.Options);

            var albums = from a in context.Albums
                         where a.Prezzo > 8
                         select a;

            foreach (var album in albums)
            {
                Console.WriteLine(album.NomeCompleto);
            }
            Console.ReadLine();*/
        }
    }
}
