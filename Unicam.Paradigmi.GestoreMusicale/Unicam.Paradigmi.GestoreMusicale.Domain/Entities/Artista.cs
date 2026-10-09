using System;
using System.Collections.Generic;
using System.Text;

namespace Unicam.Paradigmi.GestoreMusicale.Domain.Entities
{
    public class Artista
    {
        public int IdArtista { get; set; }
        public string NomeArtista { get; set; } = string.Empty;
        public virtual ICollection<Album> Albums { get; set; } = new  HashSet<Album>(); 
    }
}
