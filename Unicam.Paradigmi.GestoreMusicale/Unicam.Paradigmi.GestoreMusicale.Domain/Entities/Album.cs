using System;
using System.Collections.Generic;
using System.Text;

namespace Unicam.Paradigmi.GestoreMusicale.Domain.Entities
{
    public class Album
    {
        public int IdAlbum { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public DateTime DataUscita { get; set; }
        public int Prezzo { get; set; }
        public string Isbn { get; set; } = string.Empty;
        public int IdArtista { get; set; }
        public virtual Artista Artista { get; set; } = null!;
        public virtual ICollection<Genere> Generi { get; set; } = new HashSet<Genere>();
    }
}
