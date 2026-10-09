using System;
using System.Collections.Generic;
using System.Text;

namespace Unicam.Paradigmi.GestoreMusicale.Domain.Entities
{
    public class Genere
    {
        public int IdGenere { get; set; }
        public string NomeGenere { get; set; } = string.Empty;
        public virtual ICollection<Album> Albums { get; set; } = new HashSet<Album>();
    }
}
