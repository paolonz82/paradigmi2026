using System;
using System.Collections.Generic;

namespace Unicam.Paradigmi.FirstConsole;

public partial class Artisti
{
    public int IdArtista { get; set; }

    public string NomeArtista { get; set; } = null!;

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
